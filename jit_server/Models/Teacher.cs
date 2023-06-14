using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class Teacher
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int ContactNumber { get; set; }

    public string Email { get; set; }

    public virtual ICollection<TeacherClass> TeacherClasses { get; set; } = new List<TeacherClass>();

    public virtual ICollection<TeacherSub> TeacherSubs { get; set; } = new List<TeacherSub>();
}
