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
    public class RegisterMaps : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<int, Priority>()
                .ConstructUsing(s => Priority.Create(s));

            config.NewConfig<CreateTaskRequest, CreateTaskCommand>()
                .Map(dest => dest.Category, src => TaskCategory.Create(src.Category))
                //.Map(dest => dest.Priority, src => Priority.Create(src.Priority))
                .Map(dest => dest.OwnerId, src => new PersonId(src.OwnerId))
                .Map(dest => dest.AssigneeId, src => new PersonId(src.AssigneeId));
                //.ConstructUsing(src => new CreateTaskCommand(
                //    src.Title,
                //    src.Description,
                //    TaskCategory.Create(src.Category),
                //    Priority.Create(src.Priority),
                //    new PersonId(src.OwnerId),
                //    new PersonId(src.AssigneeId)
                //));



                config.NewConfig<UpdateTaskRequest, UpdateTaskCommand>()
                    //.Map(dest => dest.Priority, src => Priority.Create(src.Priority))
                    .Map(dest => dest.AssigneeId, src => new PersonId(src.AssigneeId))
                .ConstructUsing(src => new UpdateTaskCommand(
                    src.Title,
                    src.Description,
                    Priority.Create(src.Priority),
                    new PersonId(src.AssigneeId)
                ));

            //config.NewConfig<CreateTaskRequest, CreateTaskCommand>()
            //    .Map(dest => dest.Category, src => TaskCategory.Create(src.Category))
            //    .Map(dest => dest.Priority, src => Priority.Create(src.Priority))
            //    .Map(dest => dest.OwnerId, src => new PersonId(src.OwnerId))
            //    .Map(dest => dest.AssigneeId, src => new PersonId(src.AssigneeId));
        }
    }
}