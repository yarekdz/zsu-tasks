using Tasks.Domain.Shared;

namespace Tasks.Domain.Errors
{
    public static class PersonErrors
    {
        public static class Create
        {
            public static readonly Error InvalidEmail = Error.Validation("CreatePerson.InvalidEmail",
                "Can't create new Person with invalid email");
        }
    }
}
