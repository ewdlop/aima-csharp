using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using aima.core.search.framework.qsearch;
using Action = aima.core.agent.Action;

namespace aima.core.search.uninformed
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.11, page 82.
     * <br>
     * Breadth-first search is a simple strategy in which the root node is expanded
     * first, then all the successors of the root node are expanded next, then their
     * successors, and so on. In general, all the nodes are expanded at a given depth
     * in the search tree before any nodes at the next level are expanded.
     * <br>
     * Breadth-first search is implemented by using a FIFO queue.
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Ruediger Lunde
     */
    public class BreadthFirstSearch : Search
    {
        private readonly QueueSearch implementation;

        public BreadthFirstSearch() : this(new GraphSearchBFS())
        {
        }

        public BreadthFirstSearch(QueueSearch impl)
        {
            this.implementation = impl;
            // BFS is optimal when using early goal check (finding shortest path)
            if (impl is GraphSearchBFS || impl is TreeSearch)
            {
                impl.setEarlyGoalCheck(true);
            }
        }

        public List<Action> search(Problem problem)
        {
            return implementation.search(problem, new Queue<Node>());
        }

        public Metrics getMetrics()
        {
            return implementation.getMetrics();
        }
    }
}
