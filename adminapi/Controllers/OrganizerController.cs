using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace adminapi.Controllers
{
    public class OrganizerController : ApiController
    {
        DatabaseEntities1 databas = new DatabaseEntities1();

        // GET: api/organizer (För att hämta alla arrangörer)
        public IQueryable<Arrangörer> Get()
        {
            return databas.Arrangörer;
        }

        // GET: api/organizer/3 (Hämtar den arrangör med det angivna id:et"
        public Arrangörer Get(int id)
        {
            var findUser = databas.Arrangörer.Find(id);
            return findUser;
        }

        // POST: api/Organizer (En post request till denna ända lägger till användare)
        public string Post([FromBody] Arrangörer arrangör)
        {
            if (arrangör.Firstname == null || arrangör.Lastname == null || arrangör.Role == null)
                return null;
            if (arrangör.Role != "Arrangör")
                arrangör.Role = "Arrangör";

            databas.Arrangörer.Add(arrangör);
            databas.SaveChanges();

            return "Successfully added new organizer!";
        }


    }
}
