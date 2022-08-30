using GraphQL_CRUD.Data;
using GraphQL_CRUD.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Threading.Tasks;

namespace GraphQL_CRUD.GraphQL
{
    public class Mutation
    {
        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<Cake> SaveCakeAsync([ScopedService] AppDbContext context, CakesInputs model)
        //{
        //    var newCake = new Cake
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Price = model.Price
        //    };
        //    context.Cakes.Add(newCake);
        //    await context.SaveChangesAsync();
        //    return newCake;
        //}

        public async Task<Cake> SaveCakeAsync([Service] AppDbContext context, CakesInputs model)
        {
            var newCake = new Cake
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
            context.Cakes.Add(newCake);
            await context.SaveChangesAsync();
            return newCake;
        }


        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<Cake> UpdateCakeAsync([ScopedService] AppDbContext context, Cake updateCake)
        //{
        //    context.Cakes.Update(updateCake);
        //    await context.SaveChangesAsync();
        //    return updateCake;
        //}


        public async Task<Cake> UpdateCakeAsync([Service] AppDbContext context, Cake updateCake)
        {
            context.Cakes.Update(updateCake);
            await context.SaveChangesAsync();
            return updateCake;
        }


        //[UseDbContext(typeof(AppDbContext))]
        //public async Task<string> DeleteCakeAsync([ScopedService] AppDbContext context, int id)
        //{
        //    var cakeToDelete = await context.Cakes.FindAsync(id);

        //    if (cakeToDelete == null)
        //    {
        //        return "Invalid Operation";
        //    }

        //    context.Cakes.Remove(cakeToDelete);

        //    await context.SaveChangesAsync();
            
        //    return "Record Deleted!";
        //}


        public async Task<string> DeleteCakeAsync([Service] AppDbContext context, int id)
        {
            var cakeToDelete = await context.Cakes.FindAsync(id);

            if (cakeToDelete == null)
            {
                return "Not found";
            }

            context.Cakes.Remove(cakeToDelete);

            await context.SaveChangesAsync();

            return "Record Deleted!";
        }
    }
}