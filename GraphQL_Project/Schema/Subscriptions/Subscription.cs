using GraphQL_Project.Schema.Mutations;
using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Threading.Tasks;

namespace GraphQL_Project.Schema.Subscriptions
{
    public class Subscription
    {
        [Subscribe]
        public CourseResult CourseCreated([EventMessage] CourseResult course) => course;

        [Subscribe]
        public InstructorResult InstructorCreated([EventMessage] InstructorResult instructor) => instructor;

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResult>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";

            return topicEventReceiver.SubscribeAsync<string, CourseResult>(topicName);
        }

        [SubscribeAndResolve]
        public ValueTask<ISourceStream<InstructorResult>> InstructorUpdated(Guid instructorId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{instructorId}_{nameof(Subscription.InstructorUpdated)}";

            return topicEventReceiver.SubscribeAsync<string, InstructorResult>(topicName);
        }
    }
}