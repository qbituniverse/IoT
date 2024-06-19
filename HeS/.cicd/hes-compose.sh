### Compose all Containers ###
# Amd 64
docker compose version
docker compose -f HeS/.cicd/compose/docker-compose.DockerHub-amd64.yaml up
docker compose -f HeS/.cicd/compose/docker-compose.DockerHub-amd64.yaml down

# Arm 64
sudo docker compose version
sudo docker compose -f docker-compose.DockerHub-arm64.yaml up
sudo docker compose -f docker-compose.DockerHub-arm64.yaml down
