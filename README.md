# spike of a system with a dotnet bus and protobufs



https://kafka.apache.org/quickstart

$ docker pull apache/kafka:3.8.0
$ docker run -p 9092:9092 apache/kafka:3.8.0


./bin/kafka-topics.sh --create --topic quickstart-events --bootstrap-server localhost:9092
./bin/kafka-console-producer.sh --topic quickstart-events --bootstrap-server localhost:9092
./bin/kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092
./bin/kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092


docker compose -f docker-compose.yml up --build



shell into an image:
docker exec -it <image> bash

on image:
./opt/kafka/bin/
