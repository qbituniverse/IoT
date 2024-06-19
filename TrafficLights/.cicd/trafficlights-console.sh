### Debug on Pi ###
# Setup Folders
sudo mkdir -p /home/hes/code/TrafficLights/TrafficLights.Console
sudo chmod 777 /home/hes/code/TrafficLights/TrafficLights.Console
sudo chmod 777 /home/hes/code/TrafficLights

# Compile and Copy
# Use local Visual Studio Publish Profile

# Run
cd /home/hes/code/TrafficLights/TrafficLights.Console
export ASPNETCORE_ENVIRONMENT=Test
#dotnet TrafficLights.Console.dll --no-launch-profile
dotnet TrafficLights.Console.dll --launch-profile "Console-Test"

# Clean-up
sudo rm -rf /home/hes/code/TrafficLights/TrafficLights.Console


### Build Docker Images ###
docker buildx ls
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/amd64,linux/arm64 -t qbituniverse/iot-trafficlights-console:latest -f TrafficLights/.cicd/docker/Dockerfile-iot-trafficlights-console .
docker buildx rm -f iot-trafficlights-buildx


### Run Single Conrainer ###
sudo docker network create iot-trafficlights
sudo docker run --privileged -it --rm --name iot-trafficlights-console --network iot-trafficlights qbituniverse/iot-trafficlights-console:latest
sudo docker rm -fv iot-trafficlights-console
sudo docker rmi -f qbituniverse/iot-trafficlights-console:latest
