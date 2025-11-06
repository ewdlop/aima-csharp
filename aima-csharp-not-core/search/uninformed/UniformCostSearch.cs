using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using aima.core.search.framework.qsearch;
using Action = aima.core.agent.Action;

namespace aima.core.search.uninformed
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.14, page 84.
     * <br>
     * Uniform-cost search is a variant of Dijkstra's algorithm.
     * Instead of expanding the shallowest node, uniform-cost search expands
     * the node n with the lowest path cost g(n).
     * <br>
     * This is implemented using a priority queue ordered by path cost.
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Ruediger Lunde
     */
    public class UniformCostSearch : Search
    {
        private readonly QueueSearch implementation;

        public UniformCostSearch() : this(new GraphSearch())
        {
        }

        public UniformCostSearch(QueueSearch impl)
        {
            this.implementation = impl;
        }

        public List<Action> search(Problem problem)
        {
            // Use a PriorityQueue ordered by path cost
            PriorityQueue<Node, double> frontier = new PriorityQueue<Node, double>();
            
            // Convert to a custom implementation that works with QueueSearch
            return implementation.search(problem, new PriorityQueueAdapter(frontier));
        }

        public Metrics getMetrics()
        {
            return implementation.getMetrics();
        }

        /**
         * Adapter to make PriorityQueue work with the Queue<Node> interface
         * expected by QueueSearch while maintaining priority ordering by path cost.
         */
        private class PriorityQueueAdapter : Queue<Node>
        {
            private readonly PriorityQueue<Node, double> priorityQueue;

            public PriorityQueueAdapter(PriorityQueue<Node, double> pq)
            {
                this.priorityQueue = pq;
            }

            public new void Enqueue(Node node)
            {
                priorityQueue.Enqueue(node, node.PathCost);
            }

            public new Node Dequeue()
            {
                return priorityQueue.Dequeue();
            }

            public new Node Peek()
            {
                return priorityQueue.Peek();
            }

            public new int Count => priorityQueue.Count;
        }
    }
}
