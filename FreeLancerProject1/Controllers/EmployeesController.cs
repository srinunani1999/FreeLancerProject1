using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using FreeLancerProject1.Data;
using FreeLancerProject1.Models;
using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace FreeLancerProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly FreeLancerProject1Context _context;
        private readonly IConfiguration configuration;

        public EmployeesController(FreeLancerProject1Context context,IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        [HttpGet]
        [Route("dynamiclist")]
        protected List<Dictionary<string, object>> DynamicList(IQueryable list, string[] columns)
        {

            var list1 = new List<Dictionary<string, object>>();

            foreach (var row in list)
            {
                var dict = new Dictionary<string, object>();

                foreach (var col in columns)
                {
                    if (col != null)
                    {
                        PropertyInfo prop = typeof(Employee).GetProperty(col);
                        var disaplyName = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                        string columnName = disaplyName == null ? col : disaplyName.Name;

                        object value = prop.GetValue(row);
                        dict[columnName] = value;
                    }
                }

                list1.Add(dict);
            }

            return list1;


        }

        // GET: api/Employees
        [HttpGet]
        public  ActionResult<dynamic> GetEmployee()
        {
            
            
            SqlConnection connection = new SqlConnection( "Server=(localdb)\\mssqllocaldb;Database=FreeLancerProject1Context-766e554d-08a6-4876-9118-d374f49fd759;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand command = new SqlCommand("Test", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            List<List<object>> list = new List<List<object>>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            object p;

            List<object> list1 = new List<object>();
            List<object> col = new List<object>();

            for (int i = 0; i < sqlDataReader.FieldCount; i++)
            {
                col.Add(sqlDataReader.GetName(i));

            }
            Dictionary<string, object> names = new Dictionary<string, object>();
            List<Dictionary<string,object>> nameswithvalues = new List<Dictionary<string, object>>();

            

            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {
                    list1 = new List<object>();
                    p = new object();

                    names = new Dictionary<string, object>();

                    

                 

                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {

                        names.Add(sqlDataReader.GetName(i), sqlDataReader.GetValue(i));

                       p= sqlDataReader.GetValue(i);

                        list1.Add(sqlDataReader.GetValue(i));

                    }

                    list.Add(list1);
                    nameswithvalues.Add(names);
                }
            }
            else
            {
                return NotFound();
            }

            var convert = JsonConvert.SerializeObject(nameswithvalues);



    




            return nameswithvalues;
        }

        [HttpGet]
        [Route("emp")]
        public ActionResult<IEnumerable<object>> GetEmployeeAsync()
        {

            //var x= _context.Database.GetDbConnection().ConnectionString;
            // SqlConnection connection = new SqlConnection(x);

            //var con = configuration.GetConnectionString("FreeLancerProject1Context");
           //SqlConnection connection = new SqlConnection(con);


            SqlConnection connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=FreeLancerProject1Context-766e554d-08a6-4876-9118-d374f49fd759;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand command = new SqlCommand("Test", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

         

            SqlDataReader sqlDataReader = command.ExecuteReader();
         
            Dictionary<string, object> names = new Dictionary<string, object>();
            List<object> nameswithvalues = new List<object>();



            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {
                    

                    names = new Dictionary<string, object>();





                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {

                        names.Add(sqlDataReader.GetName(i), sqlDataReader.GetValue(i));

                      

                    }

                    nameswithvalues.Add(names);
                }
            }
            else
            {
                return NotFound();
            }
        
             //string json = JsonConvert.SerializeObject(nameswithvalues,Formatting.Indented);

         

            return nameswithvalues;

        }


        // dynamic k;

        //var x= _context. Query<dynamic>().FromSqlRaw("Test").ToList();


        // k = _context.Query<dynamic>().FromSqlRaw("Test");



        //    var employee = _context.Employee.FromSqlRaw("Test").ToList();


        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetEmployee(int id)
        {
            //var employee =  await _context.Employee.FromSqlRaw<Employee>("selectemployeebyid {0}", id).ToListAsync();

            SqlConnection connection = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=FreeLancerProject1Context-766e554d-08a6-4876-9118-d374f49fd759;Trusted_Connection=True;MultipleActiveResultSets=true");
            SqlCommand command = new SqlCommand("selectemployeebyid", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@FirstName",null);

            connection.Open();



            SqlDataReader sqlDataReader = await command.ExecuteReaderAsync();

            Dictionary<string, object> names = new Dictionary<string, object>();
            List<object> nameswithvalues = new List<object>();



            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {


                    names = new Dictionary<string, object>();





                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {

                        names.Add(sqlDataReader.GetName(i), sqlDataReader.GetValue(i));



                    }

                    nameswithvalues.Add(names);
                }
            }
            else
            {
                return NotFound();
            }

            //string json = JsonConvert.SerializeObject(nameswithvalues,Formatting.Indented);



            return nameswithvalues;

          
        }

        //PUT: api/Employees/5
         //To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
           
        {

           
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }
    }
}
