### Debug Net ###
# setup folders
sudo mkdir -p /home/hes/code/TrafficLights/TrafficLights.Api
sudo chmod 777 /home/hes/code/TrafficLights/TrafficLights.Api

# run
cd /home/hes/code/TrafficLights/TrafficLights.Api
dotnet TrafficLights.Api.dll

# test
curl -X POST "http://localhost:5000/api/traffic/start"
curl -X POST "http://localhost:5000/api/traffic/stop"
curl -X POST "http://localhost:5000/api/traffic/standby"
curl -X POST "http://localhost:5000/api/traffic/shut"
curl -X POST "http://localhost:5000/api/traffic/test?blinkTime=1000&pinNumber=23"

# clear
sudo rm -rf /home/hes/code/TrafficLights/TrafficLights.Api


### Build Docker ###
# build
docker buildx create --use --bootstrap --name iot-trafficlights-buildx
docker buildx build --push --platform linux/arm64 -t qbituniverse/iot-trafficlights-api:latest -f TrafficLights/.cicd/docker/Dockerfile-iot-trafficlights-api .
docker buildx rm -f iot-trafficlights-buildx

# run
sudo docker run --privileged -it --rm --name iot-trafficlights-api -p 8010:80 qbituniverse/iot-trafficlights-api:latest
sudo docker rm -fv iot-trafficlights-api
sudo docker rmi -f qbituniverse/iot-trafficlights-api:latest
