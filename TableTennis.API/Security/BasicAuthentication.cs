using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using TableTennis.API.Helper;
using TableTennis.Domain.DTOs;
using TableTennis.Domain.Repository.Concrete;

namespace TableTennis.API.Security
{
    public class BasicAuthentication
    {
        public static void Authenticate(HttpRequestMessage request, out TeamsOfUser teamsOfUser)
        {
            var authorization = request.Headers.Authorization;

            if (authorization == null)
                throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Authorization header missing");

            if (authorization.Scheme != "Basic")
                throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Basic authorization header missing");

            if (string.IsNullOrEmpty(authorization.Parameter.Trim()))
                throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Missing credentials");

            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Parameter));
            var username = credentials.Split(':')[0];
            var password = credentials.Split(':')[1];

            teamsOfUser = Authentication.Login(username, password);

            if (teamsOfUser != null)
                teamsOfUser.Token = JwtManager.GenerateToken(username);
            else
                throw new CustomHttpException(System.Net.HttpStatusCode.Unauthorized, "Wrong username or password");
        }
    }
}