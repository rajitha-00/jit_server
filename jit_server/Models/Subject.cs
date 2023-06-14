using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class Subject
{
    public int Id { get; set; }

    public string SubjectName { get; set; }

    public virtual ICollection<ClassSubject> ClassSubjects { get; set; } = new List<ClassSubject>();

    public virtual ICollection<TeacherSub> TeacherSubs { get; set; } = new List<TeacherSub>();
}
