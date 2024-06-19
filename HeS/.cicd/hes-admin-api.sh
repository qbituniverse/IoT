### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/hes/code/HeS/HeS.Admin.Api
sudo chmod 777 /home/hes/code/HeS/HeS.Admin.Api
sudo chmod 777 /home/hes/code/HeS

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/hes/code/HeS/HeS.Admin.Api
export ASPNETCORE_ENVIRONMENT=Test
#dotnet HeS.Admin.Api.dll --no-launch-profile
dotnet HeS.Admin.Api.dll --launch-profile "Api-Test"

# Test
curl -X GET "http://localhost:5000/api/gpio"
curl -X POST "http://localhost:5000/api/gpio/open?pinNumber=23"
curl -X POST "http://localhost:5000/api/gpio/close?pinNumber=23"
curl -X POST "http://localhost:5000/api/gpio/close-all"

# Clean-up
sudo rm -rf /home/hes/code/HeS/HeS.Admin.Api


### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-hes-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-hes-admin-api:latest -f HeS/.cicd/docker/Dockerfile-iot-hes-admin-api .
docker buildx rm -f iot-hes-buildx


### Run Single Conrainer ###
sudo docker network create iot-hes
sudo docker run --privileged -d --restart=always --name iot-hes-admin-api -p 8000:80 qbituniverse/iot-hes-admin-api:latest
sudo docker rm -fv iot-hes-admin-api
sudo docker rmi -f qbituniverse/iot-hes-admin-api:latest
