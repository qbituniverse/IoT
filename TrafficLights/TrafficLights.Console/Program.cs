using TrafficLights.Models;

Console.WriteLine("Started Traffic");
var traffic = new Traffic();
var trafficOn = true;

while (true)
{
    if (trafficOn) traffic.Start();
    else traffic.Stop();
    trafficOn = !trafficOn;
}