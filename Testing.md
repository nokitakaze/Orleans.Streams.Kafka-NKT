# How to test source code

Create `docker-compose.yml`
```yaml
version: "3.3"

services:
  kafka:
    image: apache/kafka:4.1.0
    ports:
      - "0.0.0.0:39000:39000"         # Broker: outer port
      - "0.0.0.0:39100:39100"         # Controller: outer port
    volumes:
      - ./kafka-data:/var/lib/kafka/data
    environment:
      KAFKA_PROCESS_ROLES: broker,controller
      KAFKA_NODE_ID: 1

      KAFKA_LISTENERS: PLAINTEXT://0.0.0.0:39000,CONTROLLER://0.0.0.0:39100
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://example.com:39000
      KAFKA_LISTENER_SECURITY_PROTOCOL_MAP: PLAINTEXT:PLAINTEXT,CONTROLLER:PLAINTEXT
      KAFKA_CONTROLLER_LISTENER_NAMES: CONTROLLER
      
      # Quorum of 1 node is required for a single-node cluster.
      KAFKA_CONTROLLER_QUORUM_VOTERS: 1@example.com:39100

      # Dev-настройки для одноузлового стенда
      # Dev-setting for single-node stage
      KAFKA_INTER_BROKER_LISTENER_NAME: PLAINTEXT
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
      KAFKA_TRANSACTION_STATE_LOG_REPLICATION_FACTOR: 1
      KAFKA_TRANSACTION_STATE_LOG_MIN_ISR: 1
      KAFKA_GROUP_INITIAL_REBALANCE_DELAY_MS: "0"

      # Data directory
      KAFKA_LOG_DIRS: /var/lib/kafka/data
```

Change `example.com` to your host IP. Execute this script:

```bash
#!/bin/bash

mkdir -p kafka-data
chown -R 1000:1000 ./kafka-data
```

Change `./Orleans.Streams.Kafka.E2E/Tests/TestBase.cs` Brokers to your host IP:
```CSharp
public static List<string> Brokers = new List<string>
{
    "example.com:39000"
};
```

Start docker-compose.
```bash
docker-compose down && docker-compose up -d
```

And run tests.

For some reason, the initial tests fail when run against an empty database. On the second run, your tests will pass normally.
