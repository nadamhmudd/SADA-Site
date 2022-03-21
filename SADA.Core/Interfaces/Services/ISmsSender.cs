namespace SADA.Core.Interfaces;
public interface ISmsSender
{
    Task SendSMSAsync(string phoneNumber, string message);
}
