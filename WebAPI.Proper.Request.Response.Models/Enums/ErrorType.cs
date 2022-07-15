namespace WebAPI.Proper.Request.Response.Models.Enums
{
    public enum ErrorType
    {
        NotFound = 404,
        Invalid = -99,
        UnAuthorized = 401,
        Forbidden = 403,
        NullException = -1,
        ServerError = 500,
    }
}
