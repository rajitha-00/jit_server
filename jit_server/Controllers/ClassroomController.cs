using jit_server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace jit_server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassroomController : Controller
    {
        private LmsJitContext db = new LmsJitContext();


        [HttpGet]
        public async Task<String> getAllClassrooms()
        {
            try
            {
                List<Classroom> classrooms = await db.Classrooms.ToListAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = classrooms });
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message });
            }
        }

        [HttpGet]
        public async Task<String> getClassrromById(int? id)
        {
            bool success = false;
            try
            {
                Classroom clarm = await db.Classrooms.SingleAsync(S => S.Id == id);
                if (clarm != null)
                {
                    success = true;
                    return Newtonsoft.Json.JsonConvert.SerializeObject(new
                    {
                        success = success,
                        data = new
                        {
                            Id = clarm.Id,
                            ClassroomName = clarm.ClassroomName

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
        public async Task<String> create(ClassroomModel clarm)
        {
            try
            {

                Classroom newStd = new Classroom()
                {
                    ClassroomName = clarm.ClassroomName

                };
                db.Classrooms.Add(newStd);
                await db.SaveChangesAsync();
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = true, data = newStd });

            }
            catch (Exception ex) { return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = ex.Message }); }
        }

        [HttpPut]
        public async Task<String> update(ClassroomModel clarm)
        {
            Classroom existingClassroom = await db.Classrooms.FindAsync(clarm.Id);

            if (existingClassroom != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    Classroom newStd = new Classroom()
                    {
                        ClassroomName = clarm.ClassroomName

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
            Classroom existingClassroom = await db.Classrooms.FindAsync(id);

            if (existingClassroom != null)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, data = "Not Found" });
            }
            else
            {
                try
                {
                    db.Classrooms.Remove(existingClassroom);
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



    public class ClassroomModel
    {
        public int Id { get; set; }

        public string ClassroomName { get; set; }

    }
}
