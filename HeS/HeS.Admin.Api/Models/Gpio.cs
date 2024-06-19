using System.Device.Gpio;

namespace HeS.Admin.Api.Models
{
    public class Gpio : IGpio
    {
        private readonly GpioController _gpio = new();

        public void OpenPin(int pinNumber)
        {
            _gpio.OpenPin(pinNumber, PinMode.Output);
            _gpio.Write(pinNumber, PinValue.High);
        }

        public void ClosePin(int pinNumber)
        {
            _gpio.OpenPin(pinNumber, PinMode.Output);
            _gpio.Write(pinNumber, PinValue.Low);
            _gpio.ClosePin(pinNumber);
        }

        public void CloseAllPins()
        {
            for (var i = 0; i <= 27; i++)
            {
                _gpio.OpenPin(i, PinMode.Output);
                _gpio.Write(i, PinValue.Low);
                _gpio.ClosePin(i);
            }
        }
    }
}