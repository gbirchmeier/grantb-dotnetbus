# BasicSystem

The code needs a ton of cleanup, it's really rough.

I learned a bunch of what-not-to-do things while figuring this out.

But... it uses a bus and protos and a DB, and that's what I wanted to spike.


## Setup stuff

### Launch docker containers

`docker compose -f docker-compose.yml up --build`

* Launches kafka and creates appropriate topics
* Launches mssql and creates DB **(but not tables)**

**To shell into a container**

`docker exec -it <container> bash`

### Create tables

* `cd BasicSystem/DbTool`
* `dotnet run create`

**Or drop tables:**

* `dotnet run drop` - you'll get a prompt to confirm before it happens.

## Run the system

In a shell in `DatabaseRecorder`: `dotnet run`

In a shell in `OrderConsole`: `dotnet run <your-name-or-something>`

* Use `buy <anystring>` or `sell <anystring>` in the OrderConsole to send a TradeRequest message to the bus.
* The DatabaseRecorder terminal will show it is received, and write it to the TradeRequest table.

(There's a TradeStatusUpdate message defined also, but nothing uses it.)



## references

**Kafka API docs**

https://docs.confluent.io/platform/current/clients/confluent-kafka-dotnet/_site/api/Confluent.Kafka.html




