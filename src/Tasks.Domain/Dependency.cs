using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Tasks;

namespace Tasks.Domain
{
    public class Dependency
    {
        public TodoTask DependOnTodoTask { get; private set; }
        public string OtherDependency { get; private set; }
    }
}
