using Tasks.Domain.Abstractions;
using Tasks.Domain.Events.Tasks;
using Tasks.Domain.Person;
using Tasks.Domain.Shared;
using Tasks.Domain.States;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.Tasks
{
    public class TodoTask : Entity
    {
        public TaskId TaskId { get; }

        public TaskMainInfo MainInfo { get; private set; }

        public TaskEstimation? Estimation { get; private set; }

        public TaskStatistic? Stats { get; private set; }

        public TaskFlags Flags { get; private set; } = new();

        #region v2

        //public TaskDependency Dependency { get; private set; } = new();
        //public IReadOnlyList<Comment> Comments { get; private set; }
        //public IReadOnlyList<Person> Followers { get; private set; }
        //public IReadOnlyList<AttachedFile> AttachedFiles { get; private set; }
        //public IReadOnlyList<Tag> Tags { get; private set; }
        //todo: time tracking
        //todo: notifications
        //todo: Search
        //todo: task management tool
        //todo: Lock / Delete / Terminate
        //todo: Log Work mechanism
        //todo: log any action

        #endregion

        public ITaskState State { get; private set; }
        public TodoTaskStatus Status { get; private set; }

        private TodoTask()
        {

        }

        protected TodoTask(Guid id, TaskMainInfo mainInfo, ITaskState iniTaskState)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            
            TaskId = new TaskId(id);
            MainInfo = mainInfo;

            State = iniTaskState;
            Status = iniTaskState.Status;

            Stats = new TaskStatistic(TaskId)
            {
                CreatedDate = CreatedAt,
                Task = this
            };
        }

        public static TodoTask Create(TaskMainInfo mainInfo, ITaskState iniTaskState)
        {
            var newTask = new TodoTask(Guid.NewGuid(), mainInfo, iniTaskState);

            newTask.SetState(iniTaskState);
            
            var initStateResult = newTask.State.Create(newTask, mainInfo);

            if (!initStateResult.IsSuccess)
            {
                throw new DomainValidationException(initStateResult.Error);
            }

            newTask.MainInfo = mainInfo;

            newTask.Raise(new TaskCreatedDomainEvent(newTask.TaskId.Value));

            return newTask;
        }

        public void SetState(ITaskState state)
        {
            //todo: implement IStateFactory and verify States connection
            State = state;
            Status = state.Status;
        }

        #region Task Actions

        public void Estimate(TaskEstimation estimation)
        {
            var estimateStateResult = State.Estimate(this, estimation);

            if (!estimateStateResult.IsSuccess)
            {
                throw new DomainValidationException(estimateStateResult.Error);
            }

            Estimation = estimation;
        }

        #region v2

        //todo
        //public void AddDependencies(TaskDependency dependency)
        //{
        //    var addDependenciesStateResult = _state.AddDependencies(this, dependency);

        //    if (!addDependenciesStateResult.IsSuccess)
        //    {
        //        throw new DomainValidationException(addDependenciesStateResult.Error);
        //    }
        //}

        #endregion


        public void StartWork()
        {
            var startWorkStateResult = State.StartWork(this);

            if (!startWorkStateResult.IsSuccess)
            {
                throw new DomainValidationException(startWorkStateResult.Error);
            }

            Stats ??= new TaskStatistic(TaskId);
            Stats.StartedDate = DateTime.UtcNow;
            Flags.IsStarted = true;

            Raise(new WorkStartedDomainEvent(TaskId.Value));
        }

        public void CompleteWork()
        {
            var completeWorkStateResult = State.CompleteWork(this);

            if (!completeWorkStateResult.IsSuccess)
            {
                throw new DomainValidationException(completeWorkStateResult.Error);
            }

            Stats ??= new TaskStatistic(TaskId);
            Stats.CompletionDate = DateTime.UtcNow;
            Stats.ActualWorkDuration = Duration.Create(Stats.StartedDate, Stats.CompletionDate);
            Flags.IsCompleted = true;
        }

        public void Verify()
        {
            var verifyStateResult = State.Verify(this);

            if (!verifyStateResult.IsSuccess)
            {
                throw new DomainValidationException(verifyStateResult.Error);
            }

            Stats ??= new TaskStatistic(TaskId);
            Stats.VerifiedDate = DateTime.UtcNow;
            Flags.IsVerified = true;
        }

        public void Approve()
        {
            var approveStateResult = State.Approve(this);

            if (!approveStateResult.IsSuccess)
            {
                throw new DomainValidationException(approveStateResult.Error);
            }
            Stats ??= new TaskStatistic(TaskId);
            Stats.ApprovedDate = DateTime.UtcNow;
            Flags.IsApproved = true;
        }

        public void Release()
        {
            var releaseStateResult = State.Release(this);

            if (!releaseStateResult.IsSuccess)
            {
                throw new DomainValidationException(releaseStateResult.Error);
            }

            Stats ??= new TaskStatistic(TaskId);
            Stats.ReleasedDate = DateTime.UtcNow;
            Flags.IsReleased = true;
        }

        public void Terminate()
        {
            var terminateStateResult = State.Terminate(this);

            if (!terminateStateResult.IsSuccess)
            {
                throw new DomainValidationException(terminateStateResult.Error);
            }

            Flags.IsTerminated = true;
        }

        #endregion

        public override string ToString()
        {
            return
                $"TaskId: {TaskId.Value.ToString()} | Title: {MainInfo.Title} | Created: {Stats?.CreatedDate:yyyy-MM-dd} | Status: {Status} | " +
                $"Estimation: {Estimation?.EstimatedWorkDuration} | " +
               (Stats is { StartedDate: { } } ? $"Stats: WorkStarted: {Stats?.StartedDate.Value:yyyy-MM-dd HH:mm} | ": "") +
               (Stats is { CompletionDate: { } } ? $"WorkCompleted: {Stats?.CompletionDate.Value:yyyy-MM-dd HH:mm} | " : "") +
                $"{Stats?.ActualWorkDuration}";
        }

        public void SetTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                MainInfo.Title = title;
            }
        }

        public void SetDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                MainInfo.Description = description;
            }
        }

        public void SetPriority(Priority priority)
        {
            MainInfo.Priority = priority;
        }

        public void SetAssignee(PersonId assigneeId)
        {
            MainInfo.AssigneeId = assigneeId;
        }
    }
}