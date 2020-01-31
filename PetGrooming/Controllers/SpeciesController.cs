using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetGrooming.Data;
using PetGrooming.Models;
using System.Diagnostics;

namespace PetGrooming.Controllers
{
    public class SpeciesController : Controller
    {

        private PetGroomingContext db = new PetGroomingContext();

        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        // List of Species
        public ActionResult List()
        {
            List<Species> species = db.Species.SqlQuery("Select * from species").ToList();
            return View(species);
        }

        // Show Species
        public ActionResult Show(int id)
        {
            string query = "select * from species where speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();
            return View(selectedspecies);

        }

        // [HttpPost] Add
        [HttpPost]
        public ActionResult Add(string SpeciesName)
        {
            //Step1: gather user input data for pet species
            //debugging line
            Debug.WriteLine("I am gathering species name of " + SpeciesName);

            //Step2: create query
            string query = "insert into species (Name) values (@SpeciesName)";
            SqlParameter sqlparams = new SqlParameter("@SpeciesName", SpeciesName); //accept one parameter to add in database

            //each piece of information is a key and value pair
            //@SpeciesName is a key and SpeciesName is a value
            sqlparams = new SqlParameter("@SpeciesName", SpeciesName);

            //Step3: run query
            //this line will run the query after accepting the parameters that we define above 
            db.Database.ExecuteSqlCommand(query, sqlparams);

            //Step4: go back to list of species
            //it will return the new list of species
            return RedirectToAction("List");
        }


        //Add species to Add.cshtml  
        public ActionResult Add()
        {
            //it will first push the data and then add to 
            //map url: /species/add
            //goto Views -> Species ->Add.cshtml

            return View();
        }


        // (optional) delete  

        // [HttpPost] Delete
        public ActionResult Delete(int id)
        {
            string query = "delete from species where speciesid = @id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");

        }
        // Update
        public ActionResult Update(int id)
        {
            string query = "select * from species where speciesid=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);

            Species selectedspecies = db.Species.SqlQuery(query, sqlparam).FirstOrDefault();

            return View(selectedspecies);
        }

        // [HttpPost] Update
        [HttpPost]
        public ActionResult Update(int id, string SpeciesName)
        {
            string query = "update species set Name=@SpeciesName where speciesid=@id";
            SqlParameter[] sqlparams = new SqlParameter[2]; //two items
            sqlparams[0] = new SqlParameter("@SpeciesName", SpeciesName);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
    }
}
