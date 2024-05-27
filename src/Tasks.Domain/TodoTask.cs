using Tasks.Domain.States;
using Tasks.Domain.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain
{
    public class TodoTask
    {
        public TaskId Id { get; }

        public TaskMainInfo MainInfo { get; private set; } = new();

        public TaskAssignees Assignees { get; private set; } = new();

        public TaskEstimation Estimation { get; private set; } = new();

        public TaskDateTimeStats DateTimeStats { get; private init; } = new();

        public TaskFlags Flags { get; private set; } = new();

        #region ver.2

        //public TaskDependency Dependency { get; private set; } = new();
        //public IReadOnlyList<Comment> Comments { get; private set; }
        //public IReadOnlyList<Person> Followers { get; private set; }
        //public IReadOnlyList<AttachedFile> AttachedFiles { get; private set; }
        //public IReadOnlyList<Tag> Tags { get; private set; }
        //todo: time tracking
        //todo: notifications
        //todo: Search
        //todo: task management tool

        #endregion

        public TodoTaskStatus Status { get; private set; }
        public void SetStatus(TodoTaskStatus status)
        {
            Status = status;
        }

        private ITaskState _state;
        public void SetState(ITaskState state)
        {
            _state = state;
        }

        private TodoTask(Guid id)
        {
            Id = new TaskId(id);

            _state = new ConceptInactiveState();
            Status = TodoTaskStatus.ConceptInactive;
        }

        public static TodoTask Create(TaskMainInfo mainInfo)
        {
            var newTask = new TodoTask(Guid.NewGuid())
            {
                DateTimeStats = new TaskDateTimeStats
                {
                    CreatedDate = DateTime.UtcNow,
                    CompletionRate = new CompletionRate(0)
                },
            };

            var conceptInactiveState = new ConceptInactiveState();

            newTask.SetState(conceptInactiveState);
            conceptInactiveState.Create(newTask, mainInfo);

            return newTask;
        }

        #region Task Actions

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

            Estimation.WorkDuration = new Duration(Estimation.StartDateTime, Estimation.StartDateTime);
            Flags.IsStarted = true;
        }

        public void CompleteWork()
        {
            _state.CompleteWork(this);

            if (Estimation.WorkDuration != null)
            {
                Estimation.WorkDuration = new Duration(Estimation.WorkDuration.Start, Estimation.DueDateTime);
                Flags.IsCompleted = true;
            }
            UpdateCompletionRate();
        }

        public void Verify()
        {
            _state.Verify(this);

            Flags.IsVerified = true;
        }

        public void Approve()
        {
            _state.Approve(this);

            Flags.IsApproved = true;
        }

        public void Release()
        {
            _state.Release(this);

            Flags.IsReleased = true;
        }

        public void Terminate()
        {
            _state.Terminate(this);

            Flags.IsTerminated = true;
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

        public void UpdateCompletionRate()
        {
            if (Estimation.WorkDuration != null)
            {
                DateTimeStats.CompletionRate = CompletionRate.Calculate(Estimation.WorkDuration.Start, Estimation.WorkDuration.End, DateTime.Now);
            }
        }

        #endregion

    }
}