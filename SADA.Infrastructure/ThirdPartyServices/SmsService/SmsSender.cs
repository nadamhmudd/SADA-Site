using Microsoft.Extensions.Options;
using SADA.Core.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SADA.Infrastructure.ThirdPartyServices;
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
