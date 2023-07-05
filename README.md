# Proyecto Challenge

Proyecto de pruebas de habilidades

## Implementaciones

.NET 7 \
Entity Framework Core \
Serilog \
Dependecy Injection \
Repository Pattern \
Unit of Work \
CQRS \
Elasticsearc \
Docker \
Apache Kafka

## Descripción

Este proyecto esta desarrollado para correr sobre docker con .NET 7, para levantar el proyecto hay que instalar [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Paso 1

Para levantar el sistema API REST son

- Abrir el proyecto Visual Studio 2022 Community y restaurar los paquetes nuget
- Pararse sobre el proyecto docker-compose y ponerlo como default
- Y por ultimo presionar F5 

### Paso 3 
Para levantar los 2 proyectos de Consola de KafkaProducer y Kafka Consumer son

Si ya realizo los pasos anteriores esto quiere decir que dejo corriendo los servicios de docker, esto quiere decir que puede iniciar los proyectos de Kafka, caso contrario realice los pasos anteriores primero

- Presionar boton derecho en el proyecto Challenge.KafkaConsumer y buscar la opción DEPURAR y despues presionar en INICIAR NUEVA INSTANCIA
- Presionar boton derecho en el proyecto Challenge.KafkaProducer y buscar la opción DEPURAR y despues presionar en INICIAR NUEVA INSTANCIA
