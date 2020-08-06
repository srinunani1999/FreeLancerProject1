using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace freelanceproject1Client.Controllers
{
   [AllowAnonymous]
    public class EmployeeController : Controller
    {
        // GET: EmployeeController
       
        public async Task<ActionResult> IndexAsync()
        {
            List<Dictionary<string, object>> employees = new List<Dictionary<string, object>>();             

            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49440");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         
            var response = await client.GetAsync("/api/employees/emp");

            if (response.IsSuccessStatusCode)
               {
                    var jsoncontent = await response.Content.ReadAsStringAsync();
                     employees = JsonConvert.DeserializeObject<List<Dictionary<string,object>>>(jsoncontent);
                //string path = @"C:\Users\Asus\source\repos\FreeLancerProject1\freelanceproject1Client\Views\Employee\data.json";
                ////export data to json file. 
                //using (TextWriter tw = new StreamWriter(path))
                //{
                //    tw.WriteLine(jsoncontent);
                //};


            }
            
           // var json = JsonConvert.SerializeObject(employees);
            List<string> fieldnames = new List<string>();
            foreach (var item in employees[0])
            {
                fieldnames.Add(item.Key);

            }
            ViewBag.employees = employees;
          

            ViewBag.fields = fieldnames;
           

            // DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsoncontent);

            // ViewBag.json = jsoncontent;
            //var js = Json(employees);

            //return View(employees);
            return View();
        }

        // GET: EmployeeController/Details/5
        public async Task< ActionResult> Details(int id)
        {
            List<Dictionary<string, object>> employees = new List<Dictionary<string, object>>();


            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49440");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("/api/employees/emp");

            // if (response.IsSuccessStatusCode)
            //   {
            var jsoncontent = await response.Content.ReadAsStringAsync();

            var k = JsonConvert.DeserializeObject(jsoncontent);


            employees = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsoncontent);

            DataTable dataTable = JsonConvert.DeserializeObject<DataTable>(jsoncontent);

            //  }


            var json = JsonConvert.SerializeObject(employees);




            List<string> fieldnames = new List<string>();
            foreach (var item in employees[0])
            {
                fieldnames.Add(item.Key);

            }
            ViewBag.employees = employees;


            ViewBag.fields = fieldnames;
            var js = Json(employees);
            ViewBag.json = js;

           
            return View();
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
