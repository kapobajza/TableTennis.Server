using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TableTennis.API.Security;
using TableTennis.API.ViewModels;
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

        [Route("GetUsersTeams/{userId}/{skip}/{take}")]
        [HttpGet]
        public IHttpActionResult GetUsersTeams(int userId, int skip, int take)
        {
            return Ok(_userTeamRepo.GetTeamsOfUsers(userId, skip, take));
        }

        [Route("GetMyTeams/{userId}")]
        [HttpGet]
        public IHttpActionResult GetMyTeams(int userId)
        {
            return Ok(_userTeamRepo.GetUserTeams(userId));
        }

        [Route("Add")]
        [HttpPost]
        public IHttpActionResult AddTeam([FromBody]AddTeamVM model)
        {
            var date = DateTime.Now;
            var team = new Domain.Models.Team()
            {
                Name = model.TeamName,
                DateCreated = date
            };

            var ut = new Domain.Models.UserTeam()
            {
                UserId = model.UserId,
                DateJoined = date,
                IsLeader = true,
                TeamId = team.TeamId
            };

            _userTeamRepo.AddTeamAndUserTeam(team, ut);
            _userTeamRepo.SaveChanges();

            return Ok(team);
        }
    }
}
