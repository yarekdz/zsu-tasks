using Mapster;
using Tasks.Application.Tasks.Estimate;
using Tasks.Application.Tasks.GetTask;
using Tasks.Application.Tasks.Update;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks;
using Tasks.Domain.Tasks.TaskDetails;
using Tasks.Domain.ValueObjects;
using Tasks.Presentation.Tasks.Requests;
using Tasks.Presentation.Tasks.Responses;

namespace Tasks.Api.Mapper
{
    public class RegisterTaskMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<string, TaskCategory>()
                .MapWith(s => TaskCategory.Create(s));
            config.NewConfig<TaskCategory, string>()
                .MapWith(t => t.ToString());

            config.NewConfig<int, Priority>()
                .ConstructUsing(value => Priority.Create(value));
            config.NewConfig<Priority, int>()
                .ConstructUsing(p => p.Value);

            config.NewConfig<int?, Priority>()
                .ConstructUsing(value => Priority.Create(value));
            config.NewConfig<Priority, int?>()
                .ConstructUsing(p => p.Value);

            config.NewConfig<Guid, PersonId>()
                .MapWith(g => new PersonId(g));
            config.NewConfig<PersonId, Guid>()
                .MapWith(p => p.Value);

            config.NewConfig<TaskId, Guid>()
                .MapWith(t => t.Value);
            config.NewConfig<Guid, TaskId>()
                .ConstructUsing(p => new TaskId(p));

            config.NewConfig<EstimateRequest, EstimateCommand>()
                .ConstructUsing(src =>
                    new EstimateCommand(src.EstimatedStartDateTime, src.EstimatedEndDateTime));

            config.NewConfig<EstimateCommandResponse, EstimateResponseView>()
                .ConstructUsing(src =>
                    new EstimateResponseView(
                        src.EstimatedWorkDuration!.Start,
                        src.EstimatedWorkDuration.End,
                        src.EstimatedWorkDuration.ToString()
                    ));

            config.NewConfig<GetTaskResponse, TaskDetailsResponseView>()
                .Map(dest => dest.TaskId, src => src.TaskId.Value)
                .Map(dest => dest.Title, src => src.MainInfo.Title)
                .Map(dest => dest.Description, src => src.MainInfo.Description)
                .Map(dest => dest.Category, src => src.MainInfo.Category.ToString())
                .Map(dest => dest.Priority, src => src.MainInfo.Priority.ToString())
                //.Map(dest => dest.OwnerId, src => src.OwnerId.ToString())
                //.Map(dest => dest.AssigneeId, src => src.AssigneeId.ToString())
                .Map(dest => dest.Estimation, src => src.Estimation.Adapt<TaskDetailsEstimationResponseView>())
                //.Map(dest => dest.Status, src => src.Status.ToString())
                .Map(dest => dest.Stats, src => src.Statistic.Adapt<TaskDetailsStatsResponseView>());

            config.NewConfig<TaskEstimation, TaskDetailsEstimationResponseView>()
                .Map(dest => dest.StartDateTime,
                    src => src.EstimatedStartDateTime.ToString("g"))
                .Map(dest => dest.EndDateTime, src => src.EstimatedEndDateTime.ToString("g"))
                .Map(dest => dest.EstimatedWorkDuration,
                    src => src.EstimatedWorkDuration != null ? src.EstimatedWorkDuration.ToString() : null);

            config.NewConfig<TaskStatistic, TaskDetailsStatsResponseView>()
                .Map(dest => dest.CreatedDate, src => src.CreatedDate.ToString("g"))
                .Map(dest => dest.StartedDate,
                    src => src.StartedDate.HasValue ? src.StartedDate.Value.ToString("g") : null)
                .Map(dest => dest.CompletionDate,
                    src => src.CompletionDate.HasValue ? src.CompletionDate.Value.ToString("g") : null)
                .Map(dest => dest.VerifiedDate,
                    src => src.VerifiedDate.HasValue ? src.VerifiedDate.Value.ToString("g") : null)
                .Map(dest => dest.ApprovedDate,
                    src => src.ApprovedDate.HasValue ? src.ApprovedDate.Value.ToString("g") : null)
                .Map(dest => dest.ReleasedDate,
                    src => src.ReleasedDate.HasValue ? src.ReleasedDate.Value.ToString("g") : null);
        }
    }
}