using System.ComponentModel;

namespace Itn.Utilities.Enums
{
    public enum HttpActionServiceResultType
    {
        [Description("0")]
        None = 0,
        [Description("200")]
        Ok,
        [Description("201")]
        Created,
        [Description("404")]
        NotFound,
        [Description("401")]
        Unauthorized,
        [Description("400")]
        BadRequest,
        [Description("403")]
        Forbidden,
        [Description("422")]
        UnprocessableEntity,
        [Description("500")]
        InternalServiceError,
        [Description("500")]
        Error
    }
}
