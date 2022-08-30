using GraphQL_CRUD.Data;
using GraphQL_CRUD.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQL_CRUD.GraphQL
{
    public class Query
    {
        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<List<Cake>> AllCakesAsync([ScopedService] AppDbContext context)
        //{
        //    return await context.Cakes.ToListAsync();
        //}

        [UsePaging]
        public async Task<List<Cake>> AllCakesAsync([Service] AppDbContext context)
        {
            return await context.Cakes.ToListAsync();
        }
    }
}