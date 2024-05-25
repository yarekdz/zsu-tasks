using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain
{
    public class ComplexTask : Task
    {
        public IReadOnlyList<Task> BreakDownTasks { get; private set; }

        public CompletionRate CompletionRate { get; private set; }

        public IReadOnlyList<Person> Assignees { get; private set; }
    }
}
