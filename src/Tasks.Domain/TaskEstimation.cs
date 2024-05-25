using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain
{
    public class TaskEstimation
    {
        //task breakdown
        //3 point estimate 

        public DateTime StartDate { get; private set; }

        public DateTime DueDate { get; private set; }
        public DateTime DueTime { get; private set; }

        public Duration Duration { get; private set; }
    }
}
