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

        PasgoVisitor[] samples = new PasgoVisitor[]
        {
            new PasgoVisitor{Id = 123, FullName = "Simson A", PhoneNumber = "0123456789", Gender = true},
            new PasgoVisitor{Id = 111, FullName = "Simson B", PhoneNumber = "0147852369", Gender = true},
            new PasgoVisitor{Id = 121, FullName = "Simson C", PhoneNumber = "0159753123", Gender = true}
        };
        /*
        public string Get()
        {
            //Success
            return "Test API";
        }
        */
        
        //Hoạt động
        public IEnumerable<SimpleRestaurant_Result> Get()
        {
            List<SimpleRestaurant_Result> result = db.SimpleRestaurant(null, null, null, null).ToList();
            return result;
        }

        //Hoạt động
        public IEnumerable<SimpleRestaurant_Result> Get(string name)
        {
            //PasgoRestaurant result = db.FullInfoRestaurant(PasgoRID).ToList().ElementAt(0);
            //List<string> result = new List<string> { "abc", "def" };
            List<SimpleRestaurant_Result> result = db.SimpleRestaurant(null, null, null, name).ToList();
            return result;
            //return Ok(result);
        }

        //Call API từ js hoặc jQ
    

    }
}
