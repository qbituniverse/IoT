### MySql ###
## Network
docker network create iot-trafficlights

## Database
$MYSQL_PWD=""
$MYSQL_DIR=""
$MYSQL_DIR="/home/hes/data/TrafficLights/MySql"

docker run --name iot-trafficlights-mysql --network iot-trafficlights -e MYSQL_ROOT_PASSWORD=${MYSQL_PWD} -e MYSQL_DATABASE=TrafficLights -v ${MYSQL_DIR}:/var/lib/mysql -d -p 3306:3306 mysql:latest
sudo docker run --name iot-trafficlights-mysql --network iot-trafficlights -e MYSQL_ROOT_PASSWORD=${MYSQL_PWD} -e MYSQL_DATABASE=TrafficLights -v ${MYSQL_DIR}:/var/lib/mysql -d -p 3306:3306 mysql:latest
docker logs iot-trafficlights-mysql

## Clear
docker rm -v -f iot-trafficlights-mysql
docker network remove iot-trafficlights


### Mongo DB ###
## Network
docker network create iot-trafficlights

## Database
$MONGODB_DIR=""
$MONGODB_DIR="/home/hes/data/TrafficLights/MongoDb"

docker run --name iot-trafficlights-mongodb --network iot-trafficlights -v ${MONGODB_DIR}:/data/db -d -p 27017:27017 mongo:latest
sudo docker run --name iot-trafficlights-mongodb --network iot-trafficlights -v ${MONGODB_DIR}:/data/db -d -p 27017:27017 mongo:latest
docker logs iot-trafficlights-mongodb

## Mongo Express
docker run --name iot-trafficlights-mongoexpress --network iot-trafficlights -e ME_CONFIG_MONGODB_SERVER=iot-trafficlights-mongodb -d -p 8030:8081 mongo-express:latest
docker logs iot-trafficlights-mongoexpress

### admin:pass
http://localhost:8030

## Clean-up
docker rm -v -f iot-trafficlights-mongodb
docker rm -v -f iot-trafficlights-mongoexpress
docker network remove mongo-iot-trafficlights
