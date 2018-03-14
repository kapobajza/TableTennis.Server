using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TableTennis.Domain.DataContext;
using TableTennis.Domain.DTOs;
using TableTennis.Domain.Models;
using TableTennis.Domain.Security;
using System.Data.Entity;

namespace TableTennis.Domain.Repository.Concrete
{
    public class Authentication
    {
        public static TeamsOfUser Login(string username, string password)
        {
            using (var ctx = new EFContext())
            {
                var userTeams = (from u in ctx.Users
                                 join ut in ctx.UserTeams.Include(x => x.User).Include(x => x.Team) on u.UserId equals ut.UserId into gj
                                 from x in gj.DefaultIfEmpty()
                                 select new
                                 {
                                     x.Team,
                                     User = u
                                 }).Where(x => x.User.UserName == username).ToList();

                if (userTeams.Count <= 0)
                    return null;

                var model = new TeamsOfUser()
                {
                    Teams = new List<Team>(),
                    User = new User()
                };

                for (int i = 0; i < userTeams.Count; i++)
                {
                    var item = userTeams.ElementAt(i);

                    if(i == 0)
                    {
                        model.User = item.User;
                    }

                    if(item.Team != null)
                        model.Teams.Add(item.Team);
                }

                if (model.User.PasswordHash == HashingPasswords.GenerateHash(password, model.User.PasswordSalt))
                    return model;

                return null;
            }
        }
    }
}
