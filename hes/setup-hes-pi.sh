echo ""
echo "##################"
echo "START"
echo "##################"
echo ""

echo ""
echo "##################"
echo "Docker"
echo "##################"
echo ""

# clear
for pkg in docker.io docker-doc docker-compose podman-docker containerd runc; do sudo apt-get remove $pkg; done

# install
sudo apt-get update -qq
sudo apt-get install -y ca-certificates curl gnupg
sudo install -m 0755 -d /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/debian/gpg | sudo gpg --yes --dearmor -o /etc/apt/keyrings/docker.gpg
sudo chmod a+r /etc/apt/keyrings/docker.gpg
echo \
  "deb [arch="$(dpkg --print-architecture)" signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/debian \
  "$(. /etc/os-release && echo "$VERSION_CODENAME")" stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update -qq

# packages
sudo apt-get install -y docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin


echo ""
echo "##################"
echo "Powershell"
echo "##################"
echo ""

# clear
sudo rm -rf /tmp/powershell.tar.gz
sudo rm -rf /usr/bin/pwsh /opt/microsoft/powershell

# download latest
LATEST_RELEASE=$(sudo curl -L -s -H 'Accept: application/json' https://github.com/PowerShell/PowerShell/releases/latest)
LATEST_VERSION_V=$(sudo echo $LATEST_RELEASE | sudo sed -e 's/.*"tag_name":"\([^"]*\)".*/\1/')
LATEST_VERSION=$(sudo echo $LATEST_VERSION_V | sudo sed 's/^.//')
ARTIFACT_URL="https://github.com/PowerShell/PowerShell/releases/download/$LATEST_VERSION_V/powershell-$LATEST_VERSION-linux-arm64.tar.gz"
sudo curl -L -o /tmp/powershell.tar.gz $ARTIFACT_URL

# install
sudo mkdir -p /opt/microsoft/powershell/7
sudo tar zxf /tmp/powershell.tar.gz -C /opt/microsoft/powershell/7
sudo chmod +x /opt/microsoft/powershell/7/pwsh
sudo ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh


echo ""
echo "##################"
echo "Net Core"
echo "##################"
echo ""

# clear
sudo rm -rf /usr/bin/dotnet /usr/bin/vsdbg /opt/microsoft/.dotnet /opt/microsoft/vsdbg

# install framework
sudo curl -sSL https://dot.net/v1/dotnet-install.sh | sudo bash /dev/stdin --install-dir /opt/microsoft/.dotnet --channel 6.0
sudo ln -s /opt/microsoft/.dotnet/dotnet /usr/bin/dotnet

# install debugger
sudo curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l /opt/microsoft/vsdbg
sudo ln -s /opt/microsoft/vsdbg/vsdbg /usr/bin/vsdbg


echo ""
echo "##################"
echo "qbitcli"
echo "##################"
echo ""

# clear
sudo rm -rf /usr/bin/qbitcli /opt/qu/.qbitcli

# install
sudo mkdir -p /opt/qu
sudo docker create --name qbitcli qbituniverse/qbitcli
sudo docker cp qbitcli:/qbitcli/. /opt/qu/.qbitcli/
sudo docker rm -fv qbitcli
sudo docker rmi -f qbituniverse/qbitcli:latest
sudo chmod +x /opt/qu/.qbitcli
sudo ln -s /opt/qu/.qbitcli /usr/bin/qbitcli


echo ""
echo "##################"
echo "END"
echo "##################"
echo ""
