using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class Classroom
{
    public int Id { get; set; }

    public string ClassroomName { get; set; }

    public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();
}
