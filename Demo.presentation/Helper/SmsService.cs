using Demo.presentation.Settings;
using Demo.presentation.Utilities;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.presentation.Helper
{
    public class SmsService(IOptions<SmsSettings> _options) : ISmsService
    {
        public MessageResource SendSms(SmsMessage smsMessage)
        {
            //1=> Create Sms
            //2=>open Connection With server
            //3=> Send Sms
            //4=> DisConnect Connection

            //Init => Open Connection
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);
            //Create Sms
            var message = MessageResource.Create(
                body: smsMessage.Body,
                from: new Twilio.Types.PhoneNumber(_options.Value.Twiliophonenumber),
                to: smsMessage.PhoneNumber 
            );

            //Send Sms
            return message;
        }
    }
}
