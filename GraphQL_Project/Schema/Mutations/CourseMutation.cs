using GraphQL_Project.DTOs;
using GraphQL_Project.Middlewares.UseUser;
using GraphQL_Project.Schema.Subscriptions;
using GraphQL_Project.Services.Courses;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Threading.Tasks;

namespace GraphQL_Project.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class CourseMutation
    {
        private readonly CoursesRepository _coursesRepository;

        public CourseMutation(CoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        [Authorize]
        [UseUser]
        public async Task<CourseResult> CreateCourse(CourseInputType courseInput,
                                                        [Service] ITopicEventSender topicEventSender,
                                                        [User] User user)
        {
            string userId = user.Id;
            //string email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL);
            //string username = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME);
            //string verified = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED);

            CourseDTO courseDTO = new CourseDTO()
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                CreatorId = userId,
                InstructorId = courseInput.InstructorId
            };

            courseDTO = await _coursesRepository.Create(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), course);

            return course;
        }

        [Authorize]
        [UseUser]
        public async Task<CourseResult> UpdateCourse(Guid id,
                                                        CourseInputType courseInput,
                                                        [Service] ITopicEventSender topicEventSender,
                                                        [User] User user)
        {
            CourseDTO courseDTO = await _coursesRepository.GetById(id);

            if (courseDTO == null)
            {
                throw new GraphQLException(new Error("Course not found.", "COURSE_NOT_FOUND"));
            }

            string userId = user.Id;

            if (courseDTO.CreatorId != userId)
            {
                throw new GraphQLException(new Error("You do not have permission to update this course.", "INVALID_PERMISSION"));
            }

            courseDTO.Name = courseInput.Name;
            courseDTO.Subject = courseInput.Subject;
            courseDTO.InstructorId = courseInput.InstructorId;

            courseDTO = await _coursesRepository.Update(courseDTO);

            CourseResult course = new CourseResult()
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.CourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        [Authorize(Policy = "IsAdmin")]
        public async Task<bool> DeleteCourse(Guid id)
        {
            try
            {
                return await _coursesRepository.Delete(id);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
