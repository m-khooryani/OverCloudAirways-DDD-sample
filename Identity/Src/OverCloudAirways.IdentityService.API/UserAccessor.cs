using System.IdentityModel.Tokens.Jwt;
using Microsoft.Azure.Functions.Worker;
using Newtonsoft.Json;
using OverCloudAirways.BuildingBlocks.Domain.Abstractions;

namespace OverCloudAirways.IdentityService.API;

internal class UserAccessor : IUserAccessor
{
    private static AsyncLocal<FunctionContextRedirect> _currentContext = new AsyncLocal<FunctionContextRedirect>();
    private Guid? _userId;
    private string? _tcpConnectionId;
    private string? _fullName;

    public virtual FunctionContext FunctionContext
    {
        get
        {
            return _currentContext.Value?.HeldContext;
        }
        set
        {
            var holder = _currentContext.Value;
            if (holder != null)
            {
                holder.HeldContext = null;
            }

            if (value != null)
            {
                _currentContext.Value = new FunctionContextRedirect { HeldContext = value };
            }
        }
    }

    private JwtSecurityToken DecodeJwtToken(string token)
    {
        return new JwtSecurityTokenHandler().ReadJwtToken(token);
    }

    private string TryGetTokenFromHeaders()
    {
        var authHeaderValue = GetFromHeaders("authorization");

        if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Scheme is not Bearer
            return "";
        }

        return authHeaderValue.Substring("Bearer ".Length).Trim();
    }

    private string GetFromHeaders(string headerName)
    {
        var context = FunctionContext;
        if (!context.BindingContext.BindingData.TryGetValue("Headers", out var headersObj))
        {
            return "";
        }

        if (headersObj is not string headersStr)
        {
            return "";
        }

        // Deserialize headers from JSON
        var headers = JsonConvert.DeserializeObject<Dictionary<string, string>>(headersStr);
        var normalizedKeyHeaders = headers.ToDictionary(h => h.Key.ToLowerInvariant(), h => h.Value);
        if (!normalizedKeyHeaders.TryGetValue(headerName.ToLower(), out var authHeaderValue))
        {
            return "";
        }
        return authHeaderValue;
    }

    public Guid UserId
    {
        get
        {
            if (_userId.HasValue)
            {
                return _userId.Value;
            }
            var jwtToken = TryGetTokenFromHeaders();
            var token = DecodeJwtToken(jwtToken);
            return Guid.Parse(token.Claims.First(c => c.Type == "oid").Value);
        }
        set
        {
            if (_userId is not null)
            {
                throw new InvalidOperationException();
            }
            _userId = value;
        }
    }

    public string? TcpConnectionId
    {
        get
        {
            if (_tcpConnectionId is not null)
            {
                return _tcpConnectionId;
            }
            return GetFromHeaders("TcpConnectionId");
        }
        set
        {
            if (_tcpConnectionId is not null)
            {
                throw new InvalidOperationException();
            }
            _tcpConnectionId = value;
        }
    }

    public string FullName
    {
        get
        {
            if (_fullName is not null)
            {
                return _fullName;
            }
            var jwtToken = TryGetTokenFromHeaders();
            var token = DecodeJwtToken(jwtToken);
            var givenName = token.Claims.First(c => c.Type == "given_name").Value;
            var familyName = token.Claims.First(c => c.Type == "family_name").Value;
            return $"{givenName} {familyName}";
        }
        set
        {
            if (_fullName is not null)
            {
                throw new InvalidOperationException();
            }
            _fullName = value;
        }
    }

    private class FunctionContextRedirect
    {
        public FunctionContext HeldContext;
    }
}