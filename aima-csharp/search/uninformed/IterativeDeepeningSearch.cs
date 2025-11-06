using System.Collections.Generic;
using aima.core.agent;
using aima.core.search.framework;
using aima.core.search.framework.problem;
using Action = aima.core.agent.Action;

namespace aima.core.search.uninformed
{
    /**
     * Artificial Intelligence A Modern Approach (3rd Edition): Figure 3.18, page 89.
     * <br>
     * Iterative deepening search (or iterative deepening depth-first search) is a
     * general strategy, often used in combination with depth-first tree search, that
     * finds the best depth limit. It does this by gradually increasing the limit—
     * first 0, then 1, then 2, and so on—until a goal is found.
     * 
     * @author Ravi Mohan
     * @author Ciaran O'Reilly
     * @author Ruediger Lunde
     */
    public class IterativeDeepeningSearch : Search
    {
        private readonly Metrics metrics;
        private int maxDepth = int.MaxValue;

        public IterativeDeepeningSearch()
        {
            this.metrics = new Metrics();
        }

        /**
         * Sets a maximum depth limit for the search.
         * 
         * @param maxDepth the maximum depth to search to
         */
        public void setMaxDepth(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }

        public List<Action> search(Problem problem)
        {
            metrics.set("nodesExpanded", 0);
            metrics.set("pathCost", 0);
            metrics.set("maxDepth", 0);
            
            // Iteratively increase the depth limit
            for (int depth = 0; depth < maxDepth; depth++)
            {
                DepthLimitedSearch dls = new DepthLimitedSearch(depth);
                List<Action> result = dls.search(problem);
                
                // Update metrics
                Metrics dlsMetrics = dls.getMetrics();
                int nodesExpanded = metrics.getInt("nodesExpanded") + dlsMetrics.getInt("nodesExpanded");
                metrics.set("nodesExpanded", nodesExpanded);
                metrics.set("maxDepth", depth);
                
                // Check if we found a solution (not a failure)
                if (!SearchUtils.isFailure(result))
                {
                    // Found a solution
                    metrics.set("pathCost", dlsMetrics.getDouble("pathCost"));
                    return result;
                }
                
                // If the result is a failure (empty list), no solution exists at this depth
                // Continue to next depth
            }
            
            // No solution found within max depth
            return SearchUtils.failure();
        }

        public Metrics getMetrics()
        {
            return metrics;
        }
    }
}
