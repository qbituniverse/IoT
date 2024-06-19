### Compose all Containers ###
# Amd 64
$MYSQL_PWD=""
$MYSQL_DIR=""
docker compose version
docker compose -f TrafficLights/.cicd/compose/docker-compose.DockerHub-amd64.yaml up
docker compose -f TrafficLights/.cicd/compose/docker-compose.DockerHub-amd64.yaml down

# Arm 64
$MYSQL_PWD=""
$MYSQL_DIR=""
sudo docker compose version
sudo docker compose -f docker-compose.DockerHub-arm64.yaml up
sudo docker compose -f docker-compose.DockerHub-arm64.yaml down
