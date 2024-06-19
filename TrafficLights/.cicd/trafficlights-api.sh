### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/hes/code/TrafficLights/TrafficLights.Api
sudo chmod 777 /home/hes/code/TrafficLights/TrafficLights.Api
sudo chmod 777 /home/hes/code/TrafficLights

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/hes/code/TrafficLights/TrafficLights.Api
export ASPNETCORE_ENVIRONMENT=Test
#dotnet TrafficLights.Api.dll --no-launch-profile
dotnet TrafficLights.Api.dll --launch-profile "Api-Test"

# Clean-up
sudo rm -rf /home/hes/code/TrafficLights/TrafficLights.Api


### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-trafficlights-api:latest -f TrafficLights/.cicd/docker/Dockerfile-iot-trafficlights-api .
docker buildx rm -f iot-trafficlights-buildx


### Run Single Conrainer ###
sudo docker network create iot-trafficlights
sudo docker run -it --rm --name iot-trafficlights-api --network iot-trafficlights -p 8010:80 qbituniverse/iot-trafficlights-api:latest
sudo docker rm -fv iot-trafficlights-api
sudo docker rmi -f qbituniverse/iot-trafficlights-api:dev
