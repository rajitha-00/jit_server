using jit_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jit_server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentController : Controller
    {
        private LmsJitContext db = new LmsJitContext();


        [HttpGet]
        public async Task<String> getAllStudents()
        {
            try
            {
                List<Student> students = await db.Students.ToListAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = students });
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
            }
        }

        [HttpGet]
        public async Task<String> getStudentById(int? id)
        {
            bool success = false;
            try
            {
                Student std = await db.Students.SingleAsync(S => S.Id == id);
                if (std != null)
                {
                    success = true;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        success = success,
                        data = new
                        {
                            Id = std.Id,
                            FirstName = std.FirstName,
                            LastName = std.LastName,
                            ContactPerson = std.ContactPerson,
                            ContactNumber = std.ContactNumber,
                            Email = std.Email,
                            DateOfBirth = std.DateOfBirth,
                            Age = std.Age,
                            ClassId = std.ClassId
                        }
                    });
                }
                else
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
                }
            }
            catch (Exception ex) { return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message }); }
        }

        [HttpPost]
        public async Task<String> create(StudentModel std)
        {
            try
            {

                Student newStd = new Student()
                {
                    FirstName = std.FirstName,
                    LastName = std.LastName,
                    ContactPerson = std.ContactPerson,
                    ContactNumber = std.ContactNumber,
                    Email = std.Email,
                    DateOfBirth = std.DateOfBirth,
                    Age = std.Age,
                    ClassId = std.ClassId
                };
                db.Students.Add(newStd);
                await db.SaveChangesAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newStd });

            }
            catch (Exception ex) { return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message }); }
        }

        [HttpPut]
        public async Task<String> update(StudentModel std) {
            Student existingStudent = await db.Students.FindAsync(std.Id);

            if (existingStudent != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    Student newStd = new Student()
                    {
                        FirstName = std.FirstName,
                        LastName = std.LastName,
                        ContactPerson = std.ContactPerson,
                        ContactNumber = std.ContactNumber,
                        Email = std.Email,
                        DateOfBirth = std.DateOfBirth,
                        Age = std.Age,
                        ClassId = std.ClassId
                    };
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newStd });
                } catch (Exception ex) {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
                }
            }
        }

        [HttpDelete]
        public async Task<String> delete(int? id) {
            Student existingStudent = await db.Students.FindAsync(id);

            if (existingStudent != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                   db.Students.Remove(existingStudent);
                    await db.SaveChangesAsync();
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = "Delete Success" });
                }
                catch (Exception ex)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
                }
            }

        }
    }

    public  class StudentModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactPerson { get; set; }

        public int ContactNumber { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age { get; set; }

        public int ClassId { get; set; }
    }
}
