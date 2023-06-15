using jit_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace jit_server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubjectController : Controller
    {
        private LmsJitContext db = new LmsJitContext();


        [HttpGet]
        public async Task<String> getAllSubjects()
        {
            try
            {
                List<Subject> subjects = await db.Subjects.ToListAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = subjects });
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
                Subject sbjct = await db.Subjects.SingleAsync(S => S.Id == id);
                if (sbjct != null)
                {
                    success = true;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        success = success,
                        data = new
                        {
                            Id = sbjct.Id,
                            SubjectName = sbjct.SubjectName

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
        public async Task<String> create(SubjectModel sbjct)
        {
            try
            {

                Subject newStd = new Subject()
                {
                    SubjectName = sbjct.SubjectName

                };
                db.Subjects.Add(newStd);
                await db.SaveChangesAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newStd });

            }
            catch (Exception ex) { return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message }); }
        }

        [HttpPut]
        public async Task<String> update(StudentModel sbjct)
        {
            Subject existingSubject = await db.Subjects.FindAsync(sbjct.Id);

            if (existingSubject != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    Subject newStd = new Subject()
                    {
                        SubjectName = sbjct.SubjectName

                    };
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newStd });
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
            Subject existingSubject = await db.Subjects.FindAsync(id);

            if (existingSubject != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    db.Subjects.Remove(existingSubject);
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

    public class SubjectModel
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

    }
}