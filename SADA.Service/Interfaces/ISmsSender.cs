namespace SADA.Service.Interfaces;
public interface ISmsSender
{
    Task SendSMSAsync(string phoneNumber, string message);
}
