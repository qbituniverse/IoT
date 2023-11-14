namespace TrafficLights.Models
{
    public interface ITraffic
    {
        void Start();
        void Stop();
        void Stanby();
        void Shut();
        void Test(int blinkTime, int pinNumber);
    }
}