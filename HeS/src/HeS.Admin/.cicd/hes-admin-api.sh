### Debug Net ###
# setup folders
sudo mkdir -p /home/hes/code/HeS/HeS.Admin.Api
sudo chmod 777 /home/hes/code/HeS/HeS.Admin.Api

# run
cd /home/hes/code/HeS/HeS.Admin.Api
dotnet HeS.Admin.Api.dll

# test
curl -X GET "http://localhost:5000/api/gpio"
curl -X POST "http://localhost:5000/api/gpio/open?pinNumber=23"
curl -X POST "http://localhost:5000/api/gpio/close?pinNumber=23"
curl -X POST "http://localhost:5000/api/gpio/close-all"

# clear
sudo rm -rf /home/hes/code/HeS/HeS.Admin.Api


### Build Docker ###
# build
docker buildx create --use --bootstrap --name iot-hes-buildx
docker buildx build --push --platform linux/arm64 -t qbituniverse/iot/hes-admin-api:latest -f HeS/src/HeS.Admin/.cicd/docker/Dockerfile-iot-hes-admin-api .
docker buildx rm -f iot-hes-buildx

# run
sudo docker run --privileged --name iot-hes-admin-api -p 8001:80 qbituniverse/iot/hes-admin-api:latest
sudo docker rm -fv iot-hes-admin-api
sudo docker rmi -f qbituniverse/iot/hes-admin-api:latest
