# POC.KakfaConnect - .Net7 - EM DESENVOLVIMENTO

Este projeto é um estudo para se trabalhar com o KafkaConnect. Onde possui:
 - 1 Api de produtor de informações (Votos)
 - 1 Api de consumidor das informações geradas (Votos)
 - 1 Api KafkaConnect
 - 1 Serviço Kafka
 - 1 Serviço Zookeeper
 - 1 Serviço Debezium rodando com KafkaConnect para enxergar alterações no Banco de Dados (necessita do CDC habilitado no banco de dados)
 - 1 Serviço SQL Server 2022

 ## Como executá-lo
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
Abra um terminal, vá até a pasta `Docker`e execute o seguinte comando:

    docker build . -t connect-sqlserver:latest

Este comando criará uma imagem que criará uma imagem do KafkaConnect com um plugin do Debézium para mais tarde, monitorar o banco de dados das alterações efetuada na tabela que configurarmos.

### Execução:
Com os serviços configurados e as imagens criadas localmente, podemos então executar o docker compose. Ainda na pasta `Docker` e com o terminal aberto, execute o comando: 

    docker compose up -d

Este comando fará com que todos os serviços necessários entrem e execução. Este processo demora cerca de 1 a 2 minutos normalmente para estar concluido e todos os serviços comunicando entre si.