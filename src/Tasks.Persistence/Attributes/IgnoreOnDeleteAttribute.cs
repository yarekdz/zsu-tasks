namespace Tasks.Persistence.Attributes
{
    /// <summary>
    /// This attribute is used to specify which navigation properties should be ignored in dependency check on delete
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class IgnoreOnDeleteAttribute : Attribute
    {
    }
}
