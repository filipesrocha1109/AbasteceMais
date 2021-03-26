using System.ComponentModel;

namespace AbasteceMais.Domain.Enums
{
    public enum ErrorCodes
    {
        [Description("Success")]
        Ok = 0,

        [Description("No value(s) found")]
        NotFound = -1,

        [Description("Invalid value")]
        InvalidValue = -2,

        [Description("Invalid user credentials")]
        InvalidUserCredentials = -3,

        [Description("Your session has expired. Please relogin")]
        ExpiredToken = -4,

        [Description("Delete operation unsuccessful")]
        DeleteRequestDenied = -5,

        [Description("Create operation unsuccessful")]
        CreateRequestDenied = -6,

        [Description("Update operation unsuccessful")]
        UpdateRequestDenied = -7,

        [Description("Internal error")]
        InternalError = -9,

        [Description("Invalid password")]
        InvalidPassword = -10,

        [Description("Invalid user")]
        InvalidUser = -11,

        [Description("Different passwords")]
        DifferentPasswords = -12,

        [Description("Invalid Type")]
        InvalidType = -13,

        [Description("user already registered")]
        UserExist = -14,

        [Description("Invalid username or password")]
        InvalidUserPassword = -15,

    }
}
