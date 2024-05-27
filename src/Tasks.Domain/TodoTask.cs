using Tasks.Domain.States;
using Tasks.Domain.Tasks;

namespace Tasks.Domain
{
    public class TodoTask
    {
        //Tasks could be standalone, like “Buy Milk” or part of a much larger project.

        public TaskId Id { get; private init; }
        public DateTime CreatedDate { get; private set; }


        public TaskMainInfo MainInfo { get; private set; } = new TaskMainInfo();

        public TaskAssignees Assignees { get; private set; } = new TaskAssignees();

        public TaskEstimation Estimation { get; private set; } = new TaskEstimation();

        public TaskDependency Dependency { get; private set; } = new TaskDependency();

        public TaskDateTimeStats DateTimeStats { get; private set; }


        public IReadOnlyList<Comment> Comments { get; private set; }

        public IReadOnlyList<Person> Followers { get; private set; }

        public IReadOnlyList<AttachedFile> AttachedFiles { get; private set; }

        public IReadOnlyList<Tag> Tags { get; private set; }


        public TodoTaskStatus Status { get; private set; } = TodoTaskStatus.ConceptInactive;

        private ITaskState _state;

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


        public void SetState(ITaskState state)
        {
            _state = state;
        }

        public void SetStatus(TodoTaskStatus status)
        {
            Status = status;
        }

        public static TodoTask Create(TaskMainInfo mainInfo)
        {
            var newTask = new TodoTask
            {
                Id = new TaskId(Guid.NewGuid()),
                DateTimeStats = new TaskDateTimeStats
                {
                    CreatedDate = DateTime.UtcNow
                },
            };

            var conceptInactiveState = new ConceptInactiveState();

            newTask.SetState(conceptInactiveState);
            conceptInactiveState.Create(newTask, mainInfo);

            return newTask;
        }


        public void Assign(TaskAssignees assignees)
        {
            _state.Assign(this, assignees);
        }

        public void Estimate(TaskEstimation estimation)
        {
            _state.Estimate(this, estimation);
        }

        public void AddDependencies(TaskDependency dependency)
        {
            _state.AddDependencies(this, dependency);
        }

        public void StartWork()
        {
            _state.StartWork(this);
        }

        public void CompleteWork()
        {
            _state.CompleteWork(this);
        }

        public void Verify()
        {
            _state.Verify(this);
        }

        public void Approve()
        {
            _state.Approve(this);
        }

        public void Release()
        {
            _state.Release(this);
        }

        public void Terminate()
        {
            _state.Terminate(this);
        }


        public void SetMainInfo(TaskMainInfo mainInfo)
        {
            MainInfo = mainInfo;
        }

        public void SetAssignees(TaskAssignees assignees)
        {
            Assignees = assignees;
        }

        public void SetEstimation(TaskEstimation estimation)
        {
            Estimation = estimation;
        }
    }
}