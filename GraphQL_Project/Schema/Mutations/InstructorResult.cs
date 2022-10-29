﻿using System;

namespace GraphQL_Project.Schema.Mutations
{
    public class InstructorResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Salary { get; set; }
    }
}