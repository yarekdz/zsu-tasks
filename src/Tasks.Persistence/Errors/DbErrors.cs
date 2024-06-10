using Tasks.Domain.Shared;

namespace Tasks.Persistence.Errors
{
    public static class DbErrors
    {
        public static Error SingleDeleteDeniedMessage(string name, string? id) => new("DbErrors.SingleDeleteDeniedMessage",
            $"{name} {id} is already in use. It cannot be deleted unless all dependencies are removed.");

        public static Error MultipleDeleteDeniedMessageInSingular(string name, string? id) => new ("DbErrors.MultipleDeleteDeniedMessageInSingular",
            $"{name} {id} is already in use. It cannot be deleted unless all dependencies are removed. Please unselect it from list");

        public static Error MultipleDeleteDeniedMessageInPlural(string name, string? id) => new ("DbErrors.MultipleDeleteDeniedMessageInPlural",
            $"{name} {id} are already in use. They cannot be deleted unless all dependencies are removed. Please unselect them from list");
    }
}
