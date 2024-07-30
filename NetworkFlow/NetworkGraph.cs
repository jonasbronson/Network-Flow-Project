/* NetworkGraph.cs
 * Author: Jonas Bronson
 */

using Ksu.Cis300.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFlow
{
    /// <summary>
    /// The NetworkGraph class.
    /// </summary>
    public class NetworkGraph
    {
        /// <summary>
        /// Network containing all nodes and edges.
        /// </summary>
        private DirectedGraph<string, EdgeData> _network = new();

        /// <summary>
        /// Contains all of the paths that have been found and can be taken.
        /// </summary>
        private Dictionary<string, string> _paths = new();

        /// <summary>
        /// Returns all of the nodes in the network.
        /// </summary>
        public IEnumerable<string> Nodes
        {
            get
            {
                return _network.Nodes;
            }
        }

        public NetworkGraph(string[] edgeInfo)
        {
            string source;
            string destination;
            string capacity;
            foreach (var edge in edgeInfo)
            {
                string[] split = edge.Split(',');
                source = split[0];
                destination = split[1];
                capacity = split[2];

                _network.AddEdge(source, destination, new EdgeData(Convert.ToInt32(capacity)));
                _network.AddEdge(destination, source, new EdgeData(0));
            }
        }

        /// <summary>
        /// Gets the outgoing edges from the node passed through.
        /// </summary>
        /// <param name="node">The node being tested.</param>
        /// <returns>The outgoing edges.</returns>
        public IEnumerable<Edge<string, EdgeData>> GetOutgoingEdges(string node)
        {
            foreach(Edge<string, EdgeData> t in _network.OutgoingEdges(node))
            {
                yield return t;
            }
        }

        /// <summary>
        /// Zeros the edge data on all nodes.
        /// </summary>
        public void ZeroEdgeData()
        {
            foreach (var node1 in Nodes)
            {
                foreach(Edge<string, EdgeData> edge1 in GetOutgoingEdges(node1))
                {
                    edge1.Data.Flow = 0;
                    edge1.Data.ResidualCapacity = edge1.Data.Capacity;
                }
            }
        }

        /// <summary>
        /// Finds the max flow of the network graph.
        /// </summary>
        /// <param name="source">Source node.</param>
        /// <param name="sink">Sink node.</param>
        public void FindMaxFlow(string source, string sink)
        {
            Dictionary<string, string> allPaths = GetPath(source, sink);
            while (allPaths.ContainsKey(sink))
            {
                AddNewFlow(allPaths, FindMinimumFlow(allPaths, source, sink), source, sink);
                allPaths = GetPath(source, sink);
            }
        }

        /// <summary>
        /// Gets a new path from the source to the sink.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="sink">The sink.</param>
        /// <returns>Dictionary that contains new paths from the source to the sink.</returns>
        private Dictionary<string, string> GetPath(string source, string sink)
        {
            Queue<string> nodes = new();
            nodes.Enqueue(source);
            Dictionary<string, string> newPath = new();
            while(!(nodes.Count == 0))
            {
                string node = nodes.Dequeue();
                foreach(Edge<string, EdgeData> neighborEdges in GetOutgoingEdges(node))
                {
                    if(neighborEdges.Data.ResidualCapacity > 0 && !newPath.ContainsKey(neighborEdges.Destination) && neighborEdges.Destination != source)
                    {
                        nodes.Enqueue(neighborEdges.Destination);
                        newPath[neighborEdges.Destination] = node;
                        if (neighborEdges.Destination == sink) return newPath;
                    }
                }
            }
            return newPath;
        }

        /// <summary>
        /// Finds the minimum flow value in the path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="source">The source.</param>
        /// <param name="sink">The sink.</param>
        /// <returns>An int containing the minimum flow.</returns>
        private int FindMinimumFlow(Dictionary<string, string> path, string source, string sink)
        {
            string currentNode = sink;
            int minimumFlow = int.MaxValue;
            while (path.ContainsKey(currentNode) && path[currentNode] != currentNode)
            {
                EdgeData? t = null;
                _network.TryGetEdge(path[currentNode], currentNode, out t);
                // t will get set by the out parameter of TryGetEdge()
                if (t!.ResidualCapacity < minimumFlow) minimumFlow = t.ResidualCapacity;
                currentNode = path[currentNode];
            }
            return minimumFlow;
        }

        /// <summary>
        /// Adds a new flow, updates values, and changes the backflow and forwardflows.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="flow">The flow.</param>
        /// <param name="source">The source.</param>
        /// <param name="sink">The sink.</param>
        private void AddNewFlow(Dictionary<string, string> path, int flow, string source, string sink)
        {
            string currentNode = sink;
            while (path.ContainsKey(currentNode) && path[currentNode] != currentNode)
            {
                EdgeData? forward = null;
                EdgeData? backward = null;
                _network.TryGetEdge(path[currentNode], currentNode, out forward);
                // forward gets set from the out parameter of TryGetEdge()
                forward!.ResidualCapacity -= flow;
                _network.TryGetEdge(currentNode, path[currentNode], out backward);
                // backward gets set from the out parameter of TryGetEdge()
                backward!.ResidualCapacity += flow;
                if (forward!.Capacity > 0) forward.Flow += flow;
                else backward.Flow -= flow;
                currentNode = path[currentNode];
            }
        }

        /// <summary>
        /// Gets the flow from all of the edges in the network.
        /// </summary>
        /// <param name="node">The starting node.</param>
        /// <returns>The total flow.</returns>
        public int FlowFrom(string node)
        {
            int totalFlow = 0;
            foreach(Edge<string, EdgeData> edge in _network.OutgoingEdges(node))
            {
                totalFlow += edge.Data.Flow;
            }
            return totalFlow;
        }

        /// <summary>
        /// Displays the graph information in the correct format.
        /// </summary>
        /// <returns>String formatted in the correct way to be displayed.</returns>
        public string GraphDisplay()
        {
            StringBuilder graphDisplay = new();
            foreach (string node in _network.Nodes)
            {
                foreach (Edge<string, EdgeData> edge in GetOutgoingEdges(node))
                {
                    if(edge.Data.Capacity > 0)
                    {
                        graphDisplay.Append(edge.Source + " -> " + edge.Destination + " : " + edge.Data.Flow + " / " + edge.Data.Capacity + "\r\n");
                    }
                }
            }
            return graphDisplay.ToString().Trim();
        }
    }
}
