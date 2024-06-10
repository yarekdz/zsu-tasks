namespace Tasks.Persistence.Attributes
{
    /// <summary>
    /// This attribute defines a property which value should be displayed to user to understand what item is message about
    /// (instead of guid) 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class UserFriendlyIdAttribute : Attribute
    {
    }
}
