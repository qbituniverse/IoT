namespace HeS.Admin.Api.Models
{
    public interface IGpio
    {
        void OpenPin(int pinNumber);
        void ClosePin(int pinNumber);
        void CloseAllPins();
    }
}