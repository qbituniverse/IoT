using TrafficLights.Console;
using TrafficLights.Models;

var traffic = new Traffic();
var trafficType = Helper.TrafficType.Start;
var trafficChange = false;

while (true)
{
    if (trafficChange)
    {
        switch (trafficType)
        {
            case Helper.TrafficType.Start:
                trafficChange = false;
                traffic.Start();
                Console.WriteLine($"Traffic Start: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                break;
            case Helper.TrafficType.Stop:
                trafficChange = false;
                traffic.Stop();
                Console.WriteLine($"Traffic Stop: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                break;
            case Helper.TrafficType.Standby:
                trafficChange = false;
                traffic.Shut();
                traffic.Stanby();
                Console.WriteLine($"Traffic Standby: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                break;
        }
    }
    else
    {
        // run Standby during even minutes
        if (DateTime.Now.Minute % 2 == 0)
        {
            if (trafficType == Helper.TrafficType.Standby)
            {
                trafficChange = true;
                continue;
            }

            trafficChange = true;
            trafficType = Helper.TrafficType.Standby;
            Console.WriteLine($"Traffic Change to Standby: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
            continue;
        }

        // run for switch over from Standby to Stop
        if (trafficType == Helper.TrafficType.Standby)
        {
            trafficChange = true;
            trafficType = Helper.TrafficType.Stop;
            Console.WriteLine($"Traffic Change to Stop: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
            continue;
        }

        // run Start-Stop during uneven minutes, toggle every 10 seconds
        if (DateTime.Now.Second % 10 == 0)
        {
            trafficChange = true;
            switch (trafficType)
            {
                case Helper.TrafficType.Start:
                    trafficType = Helper.TrafficType.Stop;
                    Console.WriteLine($"Traffic Change to Stop: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                    break;
                case Helper.TrafficType.Stop:
                    trafficType = Helper.TrafficType.Start;
                    Console.WriteLine($"Traffic Change to Start: {DateTime.Now:dd-MM-yyyy HH:mm:ss}");
                    break;
            }
        }
    }
}