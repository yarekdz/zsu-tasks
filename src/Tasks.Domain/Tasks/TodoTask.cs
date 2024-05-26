using Tasks.Domain.States;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks
{
    public class TodoTask
    {
        //Tasks could be standalone, like “Buy Milk” or part of a much larger project.

        public TaskId TaskId { get; private init; }

        public string Title { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public Category Category { get; private set; }

        public TaskAssign Assign { get; private set; }

        public TaskEstimation Estimation { get; private set; }

        public TaskSchedule Schedule { get; private set; }


        public IReadOnlyList<Comment> Comments { get; private set; }

        public IReadOnlyList<Person> Followers { get; private set; }

        public IReadOnlyList<AttachedFile> AttachedFiles { get; private set; }

        public IReadOnlyList<Tag> Tags { get; private set; }


        //todo: use State 
        public State State { get; private set; }

        public void SetState(State state)
        {
            //todo: add states validation
            //if (!State.CouldSetNextOrPrev())
            //{
            //    throw exceptions
            //}
            this.State = state;
        }

        public bool IsStarted { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsReleased { get; private set; }
        public bool IsDeleted { get; private set; }
        public bool IsLocked { get; private set; }
        public bool IsTerminated { get; private set; }

        //todo: time tracking
        //todo: notifications
        //todo: Search
        //todo: task management tool

        public static TodoTask Create()
        {
            return new TodoTask
            {
                TaskId = new TaskId(Guid.NewGuid()),
            };
        }
    }
}