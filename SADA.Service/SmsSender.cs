using Microsoft.Extensions.Options;
using SADA.Service.Interfaces;
using SADA.Service.Settings;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SADA.Service;
public class SmsSender : ISmsSender
{
    private readonly TwilioSetting _twilioSetting;

    public SmsSender(IOptions<TwilioSetting> twilioSetting)
    {
        _twilioSetting = twilioSetting.Value;
    }

    public async Task SendSMSAsync(string phoneNumber, string message)
    {
        TwilioClient.Init(_twilioSetting.AccountSID, _twilioSetting.AuthToken);
        try
        {
            var SMS = MessageResource.Create(
                body: message,
                from: new Twilio.Types.PhoneNumber(_twilioSetting.PhoneNumber),
                to  : new Twilio.Types.PhoneNumber(phoneNumber)
                );
        }
        catch (Exception)
        {
        }
    }
}
