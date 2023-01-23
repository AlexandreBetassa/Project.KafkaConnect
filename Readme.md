# POC.KakfaConnect - .Net7

Este projeto é um estudo para se trabalhar com o KafkaConnect. Onde possui:
 - 1 Api de produtor de informações (Votos)
 - 1 Api de consumidor das informações geradas (Votos)
 - 1 Api KafkaConnect
 - 1 Serviço Kafka
 - 1 Serviço Zookeeper
 - 1 Serviço Debezium rodando com KafkaConnect para enxergar alterações no Banco de Dados (necessita do CDC habilitado no banco de dados)
 - 1 Serviço SQL Server 2022

 ## Como executá-lo
Primeiro de tudo será necessário possuir a Engine Docker rodando em sua máquina. Para mais detalhes consulte: 

    https://github.com/codeedu/wsl2-docker-quickstart#integrar-docker-com-wsl-2

Efetuado a instalação do serviço de container em sua máquina. Vá até a pasta "Producer" onde está o arquivo Produce.sln e execute o seguinte comando:

    docker build . -t producer-votes:latest

