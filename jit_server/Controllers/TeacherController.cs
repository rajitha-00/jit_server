using jit_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jit_server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherController : Controller
    {
        private LmsJitContext db = new LmsJitContext();


        [HttpGet]
        public async Task<String> getAllTeachers()
        {
            try
            {
                List<Teacher> teachers = await db.Teachers.ToListAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = teachers });
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
            }
        }

        [HttpGet]
        public async Task<String> getTeacherById(int? id)
        {
            bool success = false;
            try
            {
                Teacher tchr = await db.Teachers.SingleAsync(S => S.Id == id);
                if (tchr != null)
                {
                    success = true;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        success = success,
                        data = new
                        {
                            Id = tchr.Id,
                            FirstName = tchr.FirstName,
                            LastName = tchr.LastName,
                            ContactNumber = tchr.ContactNumber,
                            Email = tchr.Email
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
        public async Task<String> create(TeacherModel tchr)
        {
            try
            {

                Teacher newTchr = new Teacher()
                {
                   
                    FirstName = tchr.FirstName,
                    LastName = tchr.LastName,
                    ContactNumber = tchr.ContactNumber,
                    Email = tchr.Email
                };
                db.Teachers.Add(newTchr);
                await db.SaveChangesAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newTchr });

            }
            catch (Exception ex) { return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message }); }
        }
        [HttpPut]
        public async Task<String> update(TeacherModel tchr)
        {
            Teacher existingTeacher = await db.Teachers.FindAsync(tchr.Id);

            if (existingTeacher != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    Teacher newTchr = new Teacher()
                    {

                        FirstName = tchr.FirstName,
                        LastName = tchr.LastName,
                        ContactNumber = tchr.ContactNumber,
                        Email = tchr.Email
                    };
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newTchr });
                }
                catch (Exception ex)
                {
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
                }
            }
        }
        [HttpDelete]
        public async Task<String> delete(int? id)
        {
            Teacher existingTeacher = await db.Teachers.FindAsync(id);

            if (existingTeacher != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    db.Teachers.Remove(existingTeacher);
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

    public class TeacherModel
    {
        public int Id { get; set; }
         
        public string FirstName { get; set; }

        public string LastName { get; set; }


        public int ContactNumber { get; set; }

        public string Email { get; set; }

    }
}
