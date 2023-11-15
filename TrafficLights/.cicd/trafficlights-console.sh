### Debug Net ###
# setup folders
sudo mkdir -p /home/hes/code/TrafficLights/TrafficLights.Console
sudo chmod 777 /home/hes/code/TrafficLights/TrafficLights.Console

# run
cd /home/hes/code/TrafficLights/TrafficLights.Console
dotnet TrafficLights.Console.dll

# clear
sudo rm -rf /home/hes/code/TrafficLights/TrafficLights.Console


### Build Docker ###
# build
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/arm64 -t qbituniverse/iot-trafficlights-console:latest -f TrafficLights/.cicd/docker/Dockerfile-iot-trafficlights-console .
docker buildx rm -f iot-trafficlights-buildx

# run
sudo docker run --privileged -it --rm --name iot-trafficlights-console qbituniverse/iot-trafficlights-console:latest
sudo docker rm -fv iot-trafficlights-console
sudo docker rmi -f qbituniverse/iot-trafficlights-console:latest
