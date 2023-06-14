using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class TeacherClass
{
    public int Id { get; set; }

    public int TeacherId { get; set; }

    public int ClassId { get; set; }

    public virtual Classroom Class { get; set; }

    public virtual Teacher Teacher { get; set; }
}
