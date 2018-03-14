using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TableTennis.API.Security;
using TableTennis.Domain.Repository.Abstract;

namespace TableTennis.API.Controllers
{
    [RoutePrefix("api/Teams")]
    [JwtAuthorization]
    public class TeamsController : ApiController
    {
        private IUserTeamRepo _userTeamRepo;

        public TeamsController(IUserTeamRepo userTeamRepo)
        {
            _userTeamRepo = userTeamRepo;
        }

        [Route("GetUsersTeams/{skip}/{take}")]
        public IHttpActionResult GetUsersTeams(int skip, int take)
        {
            return Ok(_userTeamRepo.GetTeamsOfUsers(skip, take));
        }
    }
}
