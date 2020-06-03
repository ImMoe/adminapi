using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace adminapi.Controllers
{
    public class VisitorController : ApiController
    {
        DatabaseEntities1 databas = new DatabaseEntities1();

        // GET: api/visitor (För att hämta alla arrangörer)
        public IQueryable<Besökare> Get()
        {
            return databas.Besökare;
        }

        // GET: api/visitor/3 (Hämtar den arrangör med det angivna id:et"
        public Besökare Get(int id)
        {
            var findUser = databas.Besökare.Find(id);
            return findUser;
        }

        // POST: api/visitor (En post request till denna ända lägger till användare)
        public int? Post([FromBody] Besökare nybesökare)
        {
            if (nybesökare.Firstname == null || nybesökare.Lastname == null || nybesökare.Role == null)
                return null;
            if (nybesökare.Role != "Besökare")
                nybesökare.Role = "Besökare";

            databas.Besökare.Add(nybesökare);
            databas.SaveChanges();
            return nybesökare.Id;
        }

    }
}
