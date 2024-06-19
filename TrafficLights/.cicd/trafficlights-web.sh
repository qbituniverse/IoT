### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/hes/code/TrafficLights/TrafficLights.Web
sudo chmod 777 /home/hes/code/TrafficLights/TrafficLights.Web
sudo chmod 777 /home/hes/code/TrafficLights

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/hes/code/TrafficLights/TrafficLights.Web
export ASPNETCORE_ENVIRONMENT=Test
#dotnet TrafficLights.Web.dll --no-launch-profile
dotnet TrafficLights.Web.dll --launch-profile "Web-Test"

# Clean-up
sudo rm -rf /home/hes/code/TrafficLights/TrafficLights.Web


### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-trafficlights-web:latest -f TrafficLights/.cicd/docker/Dockerfile-iot-trafficlights-web .
docker buildx rm -f iot-trafficlights-buildx


### Run Single Conrainer ###
sudo docker network create iot-trafficlights
sudo docker run -it --rm --name iot-trafficlights-web --network iot-trafficlights -p 8020:80 qbituniverse/iot-trafficlights-web:latest
sudo docker rm -fv iot-trafficlights-web
sudo docker rmi -f qbituniverse/iot-trafficlights-web:latest
