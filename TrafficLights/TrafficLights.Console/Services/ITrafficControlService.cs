﻿namespace TrafficLights.Console.Services;

public interface ITrafficControlService
{
    Task Start();
    Task Stop();
    Task Standby();
    Task Shut();
    Task Test(int blinkTime, int pinNumber);
}