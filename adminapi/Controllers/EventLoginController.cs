using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace adminapi.Controllers
{
    public class EventLoginController : ApiController
    {
        DatabaseEntities1 databas = new DatabaseEntities1();
        // POST: api/EventLogin
        public Admins_Login Post([FromBody] Admins_Login user)
        {
            // Hämta användaren från databasen
            var result = databas.Admins_Login.SingleOrDefault(u => u.username == user.username);

            // Kolla så att lösenord stämmer med databas
            if (result.password == user.password && user.permission == result.permission)
            {
                if (user.permission != "eventadmin")
                {
                    return null;
                }
                return result;
            }

            return null;

        }


    }
}
