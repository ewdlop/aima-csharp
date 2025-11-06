using System.Collections.Generic;
using aima.core.agent;
using Action = aima.core.agent.Action;

namespace aima.core.search.framework.problem
{
    /**
     * Returns one for every action.
     * 
     * @author Ravi Mohan
     */
    public class DefaultStepCostFunction : StepCostFunction
    {


        public double c(System.Object stateFrom, Action action, System.Object stateTo)
        {
            return 1;
        }
    }
}