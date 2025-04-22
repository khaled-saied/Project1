using Demo.presentation.Utilities;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.presentation.Helper
{
    public interface ISmsService
    {
        MessageResource SendSms(SmsMessage smsMessage);
    }
}
