﻿using GraphQL_Project.Schema.Queries;
using HotChocolate.Data.Filters;

namespace GraphQL_Project.Schema.Filters
{
    public class CourseFilterType : FilterInputType<CourseType>
    {
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Students);

            base.Configure(descriptor);
        }
    }
}