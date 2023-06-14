using System;
using System.Collections.Generic;

namespace jit_server.Models;

public partial class Student
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

    public virtual Classroom Class { get; set; }
}
