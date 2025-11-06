using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using Action = aima.core.agent.Action;

namespace aima.core.search.uninformed
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.17, page 88.
     * <br>
     * Depth-limited search is a variant of depth-first search that avoids its pitfalls
     * by imposing a cutoff on the maximum depth of the path. The depth is measured
     * from the initial state (root node) as the number of actions required to reach
     * the node.
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Ruediger Lunde
     */
    public class DepthLimitedSearch : Search
    {
        public const int INFINITE_LIMIT = int.MaxValue;
        private readonly int limit;
        private readonly NodeExpander nodeExpander;
        private readonly Metrics metrics;

        public DepthLimitedSearch(int limit)
        {
            this.limit = limit;
            this.nodeExpander = new NodeExpander();
            this.metrics = new Metrics();
        }

        public List<Action> search(Problem problem)
        {
            metrics.set("nodesExpanded", 0);
            metrics.set("pathCost", 0);
            
            // Create the root node
            Node root = nodeExpander.createRootNode(problem.getInitialState());
            
            // Perform recursive depth-limited search
            Node result = recursiveDLS(root, problem, limit);
            
            if (result != null && !result.isCutoffNode())
            {
                metrics.set("pathCost", result.PathCost);
                return SearchUtils.getSequenceOfActions(result);
            }
            
            return SearchUtils.failure();
        }

        public Metrics getMetrics()
        {
            metrics.set("nodesExpanded", nodeExpander.getNumOfExpandCalls());
            return metrics;
        }

        /**
         * Recursive depth-limited search implementation.
         * 
         * @param node the current node
         * @param problem the problem to solve
         * @param limit the depth limit
         * @return the goal node if found, a cutoff indicator node if cutoff occurred,
         *         or null if no solution was found within the limit
         */
        private Node recursiveDLS(Node node, Problem problem, int limit)
        {
            // Check if the current node is a goal state
            if (SearchUtils.isGoalState(problem, node))
            {
                return node;
            }
            else if (limit == 0)
            {
                // Cutoff - depth limit reached
                return createCutoffNode();
            }
            else
            {
                bool cutoffOccurred = false;
                
                // Expand the current node
                foreach (Node child in nodeExpander.expand(node, problem))
                {
                    Node result = recursiveDLS(child, problem, limit - 1);
                    
                    if (result != null && result.isCutoffNode())
                    {
                        cutoffOccurred = true;
                    }
                    else if (result != null)
                    {
                        return result;
                    }
                }
                
                if (cutoffOccurred)
                {
                    return createCutoffNode();
                }
                else
                {
                    return null;
                }
            }
        }

        /**
         * Creates a special node to indicate that a cutoff occurred.
         */
        private Node createCutoffNode()
        {
            return new Node(CutOffIndicatorAction.CUT_OFF);
        }
    }

    /**
     * Extension methods for Node to check if it's a cutoff indicator.
     */
    public static class NodeExtensions
    {
        public static bool isCutoffNode(this Node node)
        {
            return node.Action is CutOffIndicatorAction;
        }
    }
}
