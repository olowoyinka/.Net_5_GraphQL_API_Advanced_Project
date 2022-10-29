using GraphQL_Project.DTOs;
using GraphQL_Project.Schema.Subscriptions;
using GraphQL_Project.Services;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Threading.Tasks;

namespace GraphQL_Project.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class InstructorMutation
    {
        [Authorize]
        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<InstructorResult> CreateInstructor(InstructorInputType instructorInputType,
                                                                [ScopedService] SchoolDbContext context,
                                                                [Service] ITopicEventSender topicEventSender) 
        {
            InstructorDTO instructorDTO = new InstructorDTO
            {
               FirstName = instructorInputType.FirstName,
               LastName = instructorInputType.LastName,
                Salary = instructorInputType.Salary,
                Id = Guid.NewGuid()
            };

            context.Add(instructorDTO);
            await context.SaveChangesAsync();

            InstructorResult instructorResult = new InstructorResult()
            {
                Id = instructorDTO.Id,
                Salary = instructorDTO.Salary,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName
            };

            await topicEventSender.SendAsync(nameof(Subscription.InstructorCreated), instructorResult);

            return instructorResult;
        }

        [Authorize]
        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<InstructorResult> UpdateInstructor(Guid id,
                                                                InstructorInputType instructorInputType,
                                                                [ScopedService] SchoolDbContext context,
                                                                [Service] ITopicEventSender topicEventSender)
        {
            InstructorDTO instructorDTO = await context.Instructors.FindAsync(id);

            if(instructorDTO == null)
            {
                throw new GraphQLException(new Error("Instructor not found.", "INSTTRUCTOR_NOT_FOUND"));
            }

            instructorDTO.FirstName = instructorInputType.FirstName;
            instructorDTO.LastName = instructorInputType.LastName;
            instructorDTO.Salary = instructorInputType.Salary;

            context.Update(instructorDTO);
            await context.SaveChangesAsync();

            InstructorResult instructorResult = new InstructorResult()
            {
                Id = instructorDTO.Id,
                Salary = instructorDTO.Salary,
                FirstName = instructorDTO.FirstName,
                LastName = instructorDTO.LastName
            };

            string updateInstructorTopic = $"{instructorResult.Id}_{nameof(Subscription.InstructorUpdated)}";
            await topicEventSender.SendAsync(updateInstructorTopic, instructorResult);

            return instructorResult;
        }

        [Authorize(Policy = "IsAdmin")]
        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<bool> DeleteInstructor(Guid id, [ScopedService] SchoolDbContext context)
        {
            InstructorDTO instructorDTO = new InstructorDTO
            {
                Id = id
            };

            context.Remove(instructorDTO);

            try
            {
                await context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}