using Tasks.Domain.Shared;
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

        public TaskDateTimeStats Stats { get; private init; } = new();

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

        protected TodoTask(Guid id)
        {
            Id = new TaskId(id);

            _state = new ConceptInactiveState();
            Status = TodoTaskStatus.ConceptInactive;
        }

        public static TodoTask Create(TaskMainInfo mainInfo)
        {
            var newTask = new TodoTask(Guid.NewGuid())
            {
                Stats = new TaskDateTimeStats
                {
                    CreatedDate = DateTime.UtcNow,
                },
            };

            var conceptInactiveState = new ConceptInactiveState();

            newTask.SetState(conceptInactiveState);
            
            var initStateResult = conceptInactiveState.Create(newTask, mainInfo);

            if (!initStateResult.IsSuccess)
            {
                throw new DomainValidationException(initStateResult.Error);
            }

            newTask.MainInfo = mainInfo;

            return newTask;
        }

        #region Task Actions

        public void Assign(TaskAssignees assignees)
        {
            var assignStateResult = _state.Assign(this, assignees);

            if (!assignStateResult.IsSuccess)
            {
                throw new DomainValidationException(assignStateResult.Error);
            }

            Assignees = assignees;
        }

        public void Estimate(TaskEstimation estimation)
        {
            var estimateStateResult = _state.Estimate(this, estimation);

            if (!estimateStateResult.IsSuccess)
            {
                throw new DomainValidationException(estimateStateResult.Error);
            }

            Estimation = estimation;
            Estimation.EstimatedWorkDuration =
                new Duration(estimation.EstimatedStartDateTime, estimation.EstimatedEndDateTime);
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
            var startWorkStateResult = _state.StartWork(this);

            if (!startWorkStateResult.IsSuccess)
            {
                throw new DomainValidationException(startWorkStateResult.Error);
            }

            Stats.StartedDate = DateTime.UtcNow;
           
            Flags.IsStarted = true;
        }

        public void CompleteWork()
        {
            var completeWorkStateResult = _state.CompleteWork(this);

            if (!completeWorkStateResult.IsSuccess)
            {
                throw new DomainValidationException(completeWorkStateResult.Error);
            }

            Stats.CompletionDate = DateTime.UtcNow;
            Stats.ActualWorkDuration = new Duration(Stats.StartedDate, Stats.CompletionDate);
            
            Flags.IsCompleted = true;
        }

        public void Verify()
        {
            var verifyStateResult = _state.Verify(this);

            if (!verifyStateResult.IsSuccess)
            {
                throw new DomainValidationException(verifyStateResult.Error);
            }

            Stats.VerifiedDate = DateTime.UtcNow;

            Flags.IsVerified = true;
        }

        public void Approve()
        {
            var approveStateResult = _state.Approve(this);

            if (!approveStateResult.IsSuccess)
            {
                throw new DomainValidationException(approveStateResult.Error);
            }

            Stats.ApprovedDate = DateTime.UtcNow;

            Flags.IsApproved = true;
        }

        public void Release()
        {
            var releaseStateResult = _state.Release(this);

            if (!releaseStateResult.IsSuccess)
            {
                throw new DomainValidationException(releaseStateResult.Error);
            }

            Stats.ReleasedDate = DateTime.UtcNow;

            Flags.IsReleased = true;
        }

        public void Terminate()
        {
            var terminateStateResult = _state.Terminate(this);

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
                $"TaskId: {Id.Value.ToString()} | Title: {MainInfo.Title} | Created: {Stats.CreatedDate:yyyy-MM-dd} | Status: {Status} | " +
                $"Estimation: {Estimation.EstimatedWorkDuration} | " +
               (Stats.StartedDate.HasValue ? $"Stats: WorkStarted: {Stats.StartedDate.Value:yyyy-MM-dd HH:mm} | ": "") +
               (Stats.CompletionDate.HasValue ? $"WorkCompleted: {Stats.CompletionDate.Value:yyyy-MM-dd HH:mm} | " : "") +
                $"{Stats.ActualWorkDuration}";
        }
    }
}