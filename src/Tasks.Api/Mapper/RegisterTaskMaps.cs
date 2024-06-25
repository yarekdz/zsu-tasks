using Mapster;
using Tasks.Application.Tasks.Create;
using Tasks.Application.Tasks.Update;
using Tasks.Domain;
using Tasks.Domain.Person;
using Tasks.Domain.Tasks;
using Tasks.Domain.ValueObjects;
using Tasks.Presentation.Tasks.Requests;

namespace Tasks.Api.Mapper
{
    public class RegisterTaskMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<string, TaskCategory>()
                .MapWith(s => TaskCategory.Create(s));

            config.NewConfig<int, Priority>()
                .ConstructUsing(value => Priority.Create(value));

            config.NewConfig<Guid, PersonId>()
                .MapWith(g => new PersonId(g));

            config.NewConfig<UpdateTaskRequest, UpdateTaskCommand>()
                .ConstructUsing(src => new UpdateTaskCommand(
                    src.Title,
                    src.Description,
                    Priority.Create(src.Priority),
                    new PersonId(src.AssigneeId)
                ));
        }
    }
}