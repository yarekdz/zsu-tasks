using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.StateTransitions;

namespace Tasks.Domain.StatesChains
{
    public static class DefaultStateChain
    {

        public static IStateTransition GenerateChain()
        {
            var createTaskStateTransition = new CreateTaskStateTransition();
            var assignTaskStateTransition = new AssignTaskStateTransition();
            var estimateTaskStateTransition = new EstimateTaskStateTransition();


            createTaskStateTransition.SetNext(assignTaskStateTransition);

            assignTaskStateTransition.SetPrev(createTaskStateTransition);
            assignTaskStateTransition.SetNext(estimateTaskStateTransition);

            //estimateTaskStateTransition.SetNext()

            return createTaskStateTransition;
        }
    }
}
