using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using aima.core.search.framework.qsearch;
using Action = aima.core.agent.Action;

namespace aima.core.search.uninformed
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): page 85.
     * <br>
     * Depth-first search always expands the deepest node in the current frontier
     * of the search tree. The search proceeds immediately to the deepest level of
     * the search tree, where the nodes have no successors. As those nodes are expanded,
     * they are dropped from the frontier, so then the search "backs up" to the next
     * deepest node that still has unexplored successors.
     * <br>
     * Depth-first search is implemented using a LIFO queue (stack).
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Ruediger Lunde
     */
    public class DepthFirstSearch : Search
    {
        private readonly QueueSearch implementation;

        public DepthFirstSearch() : this(new GraphSearch())
        {
        }

        public DepthFirstSearch(QueueSearch impl)
        {
            this.implementation = impl;
        }

        public List<Action> search(Problem problem)
        {
            // Use a stack (LIFO) for depth-first search
            return implementation.search(problem, new StackAdapter());
        }

        public Metrics getMetrics()
        {
            return implementation.getMetrics();
        }

        /**
         * Adapter to make Stack work with the Queue<Node> interface
         * expected by QueueSearch.
         */
        private class StackAdapter : Queue<Node>
        {
            private readonly Stack<Node> stack = new Stack<Node>();

            public new void Enqueue(Node node)
            {
                stack.Push(node);
            }

            public new Node Dequeue()
            {
                return stack.Pop();
            }

            public new Node Peek()
            {
                return stack.Peek();
            }

            public new int Count => stack.Count;
        }
    }
}
