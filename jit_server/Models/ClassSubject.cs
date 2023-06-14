using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class ClassSubject
{
    public int Id { get; set; }

    public int SubjectId { get; set; }

    public int ClassId { get; set; }

    public virtual Classroom Class { get; set; }

    public virtual Subject Subject { get; set; }
}
