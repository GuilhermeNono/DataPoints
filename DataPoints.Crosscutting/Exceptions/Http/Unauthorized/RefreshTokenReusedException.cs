using DataPoints.Crosscutting.Exceptions.Http.Unauthorized.Abstractions;
using DataPoints.Crosscutting.Messages;

namespace DataPoints.Crosscutting.Exceptions.Http.Unauthorized;

public class RefreshTokenReusedException() : UnauthorizedException(ErrorMessage.Exception.RefreshTokenReused())
{
}
