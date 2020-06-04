using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace adminapi.Controllers
{
    public class VisitorLoginController : ApiController
    {
        DatabaseEntities1 databas = new DatabaseEntities1();
        // POST: api/VisitorLogin
        public IHttpActionResult Post([FromBody] Besökare besökare)
        {
            var visitor = databas.Besökare.SingleOrDefault(v => v.Email == besökare.Email && v.Password == besökare.Password);

            if (visitor == null)
            {
                return NotFound();
            }

            return Ok(visitor);
        }
        
    }
}
