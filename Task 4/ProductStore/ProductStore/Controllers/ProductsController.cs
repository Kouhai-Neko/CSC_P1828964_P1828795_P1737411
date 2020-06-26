using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    [EnableCorsAttribute(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();
        
        [RequireHttps]
        [Route("api/talents")]
        public IEnumerable<Talent> GetAllTalents()
        {
            return repository.GetAll();
        }

        [Route("api/talents/{id:int}")]
        public Talent GetTalent(int id)
        {
            Talent item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        
    }
}
