using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
//using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{

    public class APIuserController : ApiController
    {
        private PasGoEntities2 db = new PasGoEntities2();


        public PasgoRestaurant Get(int PasgoRID)
        {
            var result = db.FullInfoRestaurant(PasgoRID).ToList().ElementAt(0);
            return result;
        }
            
    }
}
