# spike of a system with a dotnet bus and protobufs

## launching docker containers

`docker compose -f docker-compose.yml up --build`

Starts up a new docker container with topic `demo-topic` created.

## projects

* DemoSender - sends a plaintext message to the bus (in topic `demo-topic`)
* DemoConsumer - comsumes plaintext messages from the bus (topic `demo-topic`)

### misc notes

**Can use these docker scripts to send/receive messages.**

(These scripts are in the kafka downloadable, or in `./opt/kafka/bin/` in the docker image.)

./bin/kafka-topics.sh --create --topic quickstart-events --bootstrap-server localhost:9092
./bin/kafka-console-producer.sh --topic quickstart-events --bootstrap-server localhost:9092
./bin/kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092
./bin/kafka-console-consumer.sh --topic quickstart-events --from-beginning --bootstrap-server localhost:9092


**To shell into a container**

docker exec -it <container> bash

**Kafka API docs**

https://docs.confluent.io/platform/current/clients/confluent-kafka-dotnet/_site/api/Confluent.Kafka.html
