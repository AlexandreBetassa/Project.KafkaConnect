# POC.KakfaConnect - .Net 7 
## - EM DESENVOLVIMENTO

Este projeto é um estudo para se trabalhar com o KafkaConnect. Onde possui:
 - 1 Api de produtor de informações (Votos);
 - 1 Api de consumidor das informações geradas (Votos);
 - 1 Api KafkaConnect;
 - 1 Serviço Kafka;
 - 1 Serviço Zookeeper;
 - 1 Serviço Debezium rodando com KafkaConnect para enxergar alterações no Banco de Dados (necessita do CDC habilitado no banco de dados);
 - 1 Serviço SQL Server 2022;
 - Engine Docker;
 - WSL2.

 ## Como executar
 ### Producer
Primeiro de tudo será necessário possuir a Engine Docker rodando em sua máquina. Para mais detalhes consulte: 

    https://github.com/codeedu/wsl2-docker-quickstart#integrar-docker-com-wsl-2

Efetuado a instalação do serviço de container em sua máquina. Abra um terminal, vá até a pasta `Producer` onde está o arquivo `Produce.sln` e execute o seguinte comando:

    docker build . -t producer-votes:latest

Este comando fará com que uma imagem da API produtora de dados seja criada e assim possível de ser executada no quando subirmos o docker compose.

### Consumer
Abra um terminal, vá até a pasta `Consumer` onde está localizado o arquivo `Votes.sln` e execute o seguinte comando:

    docker build . -t consumer-votes:latest

Este comando fará com que uma imagem da API consumidora de dados seja criada e assim possível de ser executada no quando subirmos o docker compose.

### KafkaConnect
Abra um terminal, vá até a pasta `Docker` e execute o seguinte comando:

    docker build . -t connect-sqlserver:latest

Este comando criará uma imagem que criará uma imagem do KafkaConnect com um plugin do Debézium para mais tarde, monitorar o banco de dados das alterações efetuada na tabela que configurarmos.

### docker compose:
Com os serviços configurados e as imagens criadas localmente, podemos então executar o docker compose. Ainda na pasta `Docker` e com o terminal aberto, execute o comando: 

    docker compose up -d

Este comando fará com que todos os serviços necessários entrem e execução. Este processo demora cerca de 1 a 2 minutos normalmente para estar concluido e todos os serviços comunicando entre si.

## Habilitando CDC no Banco de Dados SQL Server:

Como o objetivo aqui é somente demonstrar uma maneira de colocar o projeto em execução, não explicaremos o que seria o CDC, porém vou colocar um link de documentação para consulta, logo abaixo:

    https://learn.microsoft.com/pt-br/sql/relational-databases/track-changes/about-change-data-capture-sql-server?view=sql-server-ver16

Para habilitá-lo executaremos um passo a passo, desde a criação do banco de dados e da tabela:

- Abra o SQL Server Management Studio, e digite as credenciais que estão na imagem abaixo: 

![SqlServerLogin](./img/loginsql.png)

- Abra uma nova consulta e coloque o script abaixo:

```script
CREATE DATABASE ProjectVoteDb;
CREATE TABLE dbo.Votes(
	Id int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Participants int NOT NULL,
	Qtd int NOT NULL)
```
- Com a tabela e o banco de dados criado podemos habilitar o CDC do seguinte modo:

```script
    USE ProjectVoteDb;
    EXEC sys.sp_cdc_enable_db;
```
Esse comando executará um procedimento armazenado no SQL Server que irá habilitar o CDC para nosso banco de dados, porém ainda precisaremos informar em qual tabela iremos efetuar as capturas. Para isso execute o seguinte script no Sql:

```script
    USE ProjectVoteDb;  
    EXEC sys.sp_cdc_enable_table  
    @source_schema = N'dbo',  
    @source_name   = N'Votes',  
    @role_name     = NULL,  
    @supports_net_changes = 1  
```
Esta query irá indicar para o CDC qual tabela capturar as alterações. Após alguns instantes, depois que a consulta for completada, examine se a sua estrutura ficou assim:

![TablesSql](./img/TablesSQL.png)

Com isso encerramos a configuração em nosso banco de dados.

## Plugin Debezium Sql-Server
Para conectarmos o Debezium no nosso banco de dados, necessitamos criar o conector e enviá-lo para o kafka-connect. Para isso inicie um terminal do Ubuntu, navegue até a pasta onde esta armazenado o arquivo `debeziumsql.json` e execute o seguinte comando:

    curl -X POST -H "Content-Type: application/json" --data @debeziumsql.json http://localhost:8083/connectors

Este comando efetua um POST de um arquivo com extensão `.json` com as configurações básicas necessárias para o Debezium se conectar ao nosso banco de dados e exportar todas as configurações para um tópico Kafka chamado de: `cdc.topic.ProjectVoteDb.dbo.Votes` através do KafkaConnect.

## Envio de dados
Abra seu navegador de internet e digite:

    http://localhost:8000/swagger/index.html

Abrirá o swagger para enviarmos dados para o nosso tópico Votes existente no Kafka. Não criamos ele manualmente, pois quando enviamos uma mensagem para o Kafka e o tópico informado não exista, por padrão o serviço Kafka o cria, nesse com valores `default`, configurados no compose que são:
- 1 partição ou `partitions`;
- fator de replicação ou `replication-factor` da nossa mensagem pelo `broker` de 1;
- Nome do tópico ou `tópic` neste caso foi informado no appsettings.json da aplicação com o nome de: `Votes`.

Execute o Post escolhendo um valor na lista do endpoint. Quando clicar em executar ele enviará uma mensagem para o Broker Kafka que armazenará em um tópico. Nossa API de consumo esta com uma configuração para efetuar o consumo das informações enviadas a cada um segundo. Ou seja, a cada 1 segunda nossa API de consumo irá lá no tópico Kafka chamado `Votes` verificará se existem offsets não lidos. Caso houver ela pegará as informações contidas no offset lida naquele momento e a enviará para gravação no banco de dados do SQLServer. Caso haja sucesso na gravação, ele irá efetuar um `commit`naquele offset lido para que no próximo ciclo de consumo ele pegue uma nova informação. 
Ao gravar na nossa tabela `Votes`no banco de dados, por ela estar sendo monitorada pelo CDC `Change-Data-Capture`, essas informações serão replicadas para a tabela `cdc.dbo_Votes_CT`, esses `logs` serão exportados para um outro tópico Kafka com nome `cdc.topic.ProjectVoteDb.dbo.Votes` que poderá ser lida por outra aplicação caso desejar.

#### OBSERVAÇÃO:
Caso tenha os arquivos binários do kafka em sua máquina, poderá criar kafka-console-consumer consumindo de `cdc.topic.ProjectVoteDb.dbo.Votes` somente para verificar a exportação dos dados do CDC para o tópico.

Você também poderá verificar os dados gravados no banco. Basta acessar a API de consumo disponivel na url:

    http://localhost:8001/swagger/index.html

Lá existem dois endpoints , um para buscar todos os dados gravados no banco de dados, como também um endpoint para busca por ID.


### Fluxo das informações 

![Fluxo](./img/fluxo.png)

## Em Desenvolvimento
---- LEITURA DO TÓPICO `cdc.topic.ProjectVoteDb.dbo.Votes`----

## REFERÊNCIAS

- https://learn.microsoft.com/pt-br/sql/relational-databases/track-changes/about-change-data-capture-sql-server?view=sql-server-ver16

- https://www.confluent.io/lp/confluent-kafka/?utm_medium=sem&utm_source=google&utm_campaign=ch.sem_br.brand_tp.prs_tgt.confluent-brand_mt.xct_rgn.latam_lng.eng_dv.all_con.confluent-general&utm_term=confluent&creative=&device=c&placement=&gclid=Cj0KCQiA_bieBhDSARIsADU4zLcocjbsvv360VPtNDuKejHq2Bz5I6IaBfHcdVuPlDb8rySjfv513PwaAkSREALw_wcB

- https://learn.microsoft.com/pt-br/azure/event-hubs/event-hubs-kafka-connect-debezium

- https://debezium.io/

- https://kafka.apache.org/

- https://hub.docker.com/search?q=