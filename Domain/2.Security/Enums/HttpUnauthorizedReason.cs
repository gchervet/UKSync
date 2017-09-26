using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum HttpUnauthorizedReason
    {
        CredentialsChanged = 1,
        TooManyLoginAttempts = 2,
        InvalidCredentials = 3,
        UnauthorizedOperation = 4,
        BlockedUser = 5,
        UserActivationPending = 6,
        PasswordChangePending = 7,
        AnotherDeviceInUse = 8,
        OnlyOneAttempt = 9,
        CredentialsConfirmationExpired = 10,
        UserConfirmationPending = 11,
        UserSendPending = 12
    }
}
