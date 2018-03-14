using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TableTennis.API.Helper;
using TableTennis.API.Security;
using TableTennis.API.ViewModels;
using TableTennis.Domain.DTOs;
using TableTennis.Domain.Models;
using TableTennis.Domain.Repository.Abstract;
using TableTennis.Domain.Security;

namespace TableTennis.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private IUserRepo _userRepo;

        public AccountController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login()
        {
            BasicAuthentication.Authenticate(Request, out TeamsOfUser teamsOfUser);

            if (teamsOfUser != null)
                return Ok(new { teamsOfUser.Token, teamsOfUser.Teams });

            return BadRequest("Token wasn't generated.");
        }

        [Route("Register")]
        [HttpPost]
        [ValidationActionFilter]
        [CustomExceptionFilter]
        public IHttpActionResult Register([FromBody]RegisterUserVM model)
        {
            using(_userRepo)
            {
                var salt = HashingPasswords.GenerateSalt();
                var pw = HashingPasswords.GenerateHash(model.Password, salt);

                _userRepo.Add(new User()
                {
                    UserName = model.UserName,
                    PasswordHash = pw,
                    PasswordSalt = salt
                });
                _userRepo.SaveChanges();

                return Ok("User registered successfully.");
            }
        }
    }
}
