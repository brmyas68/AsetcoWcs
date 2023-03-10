using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.Common.Exceptions
{
    public static class MessageException
    {
        public enum Messages
        {
            Sucess, Error,
            RequestFailt, Null, RequestNull,
            NotAccess,
            ErrorToken, NoFoundToken,
            ErrorNoMobile, ErrorNoDeleteSms, ErrorNullSms, ErrorSendSms,
            ErrorActiveCode, NotFoundActiveCode,
            ErrorOwner, EmptyOwner, NoUpdateOwner,
            NotFoundUser, NoUpdateUser, NullUserAgents, NullUserInvestor, NoUploadUser,
            ErrorWornCar, NullWornCars, NullProduct,
            NoUpdateAgent,
            NotFoundWornMaster, NoUpdateWornMaster, NullWornMaster,
            NullTrackingCode,
            NoMatchSecurityCode,
            NullMessages,
            NullAnnex,
            NullOrder,
            NullFilesProduct,
            CancelPayMentCallBack, ReversePayMent,
        };

        public enum Status
        {
            Status400 = 400,
            Status200 = 200,
        };
    }
}
