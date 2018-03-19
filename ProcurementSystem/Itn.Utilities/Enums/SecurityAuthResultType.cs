namespace Itn.Utilities.Enums
{
    public enum SecurityAuthResultType
    {
        None = 0,
        Authorized,
        RanchNotFound,
        RanchNotActive,
        ExpiryPINNotFound,
        ExpiryPINExpired,
        ExpiryTokenNotFound,
        ExpiryTokenExpired,
        APIClientNotFound,
        APIKeyNotFound,
        APIKeyExpired,
        NoSecurityContactsAvailable,
        InspectionSecurityTokenNotFound,
        MalformedOrMissingSidAuthToken,
        Error
    }
}
