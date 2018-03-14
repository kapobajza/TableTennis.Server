using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using TableTennis.API.Helper;

namespace TableTennis.API.Security
{
    public class JwtAuthorization : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple
        {
            get
            {
                return false;
            }
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (JwtManager.TokenGenerated)
            {
                if (authorization == null || authorization.Scheme != "Bearer")
                    throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Authorization header missing or the authorization scheme isn't 'Bearer'");

                if (string.IsNullOrEmpty(authorization.Parameter))
                    throw new CustomHttpException(System.Net.HttpStatusCode.Unauthorized, "Missing authorization token");

                var token = authorization.Parameter;
                var simplePrinciple = JwtManager.GetPrincipal(token);
                var identity = simplePrinciple?.Identity as ClaimsIdentity;

                var principal = await AuthenticateJwtToken(identity);

                if (principal == null)
                    throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Invalid token");
                else
                    context.Principal = principal;
            }
            else
            {
                throw new CustomHttpException(System.Net.HttpStatusCode.Unauthorized, "Missing authorization token");
            }
        }

        private Task<IPrincipal> AuthenticateJwtToken(ClaimsIdentity identity)
        {
            if (identity == null)
                return Task.FromResult<IPrincipal>(null);

            if (!identity.IsAuthenticated)
                return Task.FromResult<IPrincipal>(null);

            var username = identity.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
                return Task.FromResult<IPrincipal>(null);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));

            IPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Jwt"));

            return Task.FromResult(user);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}