using Tasks.Domain.Shared;

namespace Tasks.Domain.Errors
{
    public static class PersonErrors
    {
        public static class Create
        {
            public static readonly Error InvalidEmail = new Error("CreatePerson.InvalidEmail",
                "Can't create new Person with invalid email");
        }
    }
}
