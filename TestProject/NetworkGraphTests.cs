/* NetworkFlowTests.cs
 * Author: George Lavezzi
 * Modified by: Rod Howell
 */
using NetworkFlow;
using Ksu.Cis300.Graphs;
using System.IO.Pipes;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace TestProject
{
    /// <summary>
    /// Unit tests for the NetworkGraph class.
    /// </summary>
    [TestFixture]
    public class NetworkGraphTests
    {
        /// <summary>
        /// test graph
        /// </summary>
        /// <returns>graph</returns>
        public NetworkGraph graph1()
        {
            String[] temp = {"s,v1,16", "s,v2,13", "v1,v3, 12", "v2,v1, 4",
                    "v2,v4, 14",  "v3,t, 20", "v3,v2, 9", "v4,v3, 7", "v4,t, 4"};
            return new NetworkGraph(temp);
        }

        /// <summary>
        /// test graph
        /// </summary>
        /// <returns>graph</returns>
        public NetworkGraph graph2()
        {
            String[] temp = {"s,a,20", "s,b,2", "s,c,4", "c,t,7", "a,t,7",
                        "b,t,8", "b,c,3", "a,b,8" };
            return new NetworkGraph(temp);
        }



        /// <summary>
        /// Uses Constructor to test Nodes
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestNodes()
        {
            NetworkGraph t = graph1();
            String[] nodes = { "t", "s", "v1", "v2", "v3", "v4" };
            List<String> uut = new();
            foreach(String n in t.Nodes) uut.Add(n);
            Assert.That(uut.Count == 6);
            foreach (String s in nodes) Assert.That(uut.Contains(s));
                
        }
        

        /// <summary>
        /// Tests FindMaxFlow
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestFindMaxFlow()
        {
            Dictionary<(String, String), int> answer = new();
            answer[("s", "a")] = 15;
            answer[("s","b")]= 2;
            answer[("s", "c")] = 4;
            answer[("a", "b")] = 8;
            answer[("b", "t")] = 8;
            answer[("a", "t")] = 7;
            answer[("b", "c")] = 2;
            answer[("c", "t")] = 6;

            NetworkGraph t = graph2();
            t.FindMaxFlow("s", "t");
            foreach(String u in t.Nodes)
            {
                foreach (Edge<String, EdgeData> e in t.GetOutgoingEdges(u))
                { 
                    if (answer.ContainsKey((e.Source, e.Destination)))
                    {
                        Assert.That(answer[(e.Source, e.Destination)] == e.Data.Flow);
                    }
                    else {
                        Assert.That(e.Data.Flow == 0);
                    }
                }
            }
        }
        
        /// <summary>
        /// Tests FlowFrom
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestFlowFrom()
        {
            NetworkGraph t = graph1();
            t.FindMaxFlow("v1", "t");
            Assert.That(t.FlowFrom("v3") == 12);
            Assert.That(t.FlowFrom("v2") == 0);
            Assert.That(t.FlowFrom("s") == 0); 
        
        }

        /// <summary>
        /// Adds a flow of 4 to the given edge in the given network
        /// </summary>
        /// <param name="t">The network</param>
        /// <param name="e">The edge</param>
        private void UpdateFlow(NetworkGraph t, Edge<string, EdgeData> e)
        {
            e.Data.Flow = 4;
            e.Data.ResidualCapacity -= 4;
        }

        /// <summary>
        /// Tests that FindMaxFlow correctly backs out from a wrong flow.
        /// </summary>
        [Test]
        public void TestBackFlow()
        {
            NetworkGraph t = graph1();
            foreach(Edge<String, EdgeData> e in t.GetOutgoingEdges("s"))  if (e.Destination.Equals("v1")) UpdateFlow(t, e);
            foreach (Edge<String, EdgeData> e in t.GetOutgoingEdges("v1")) if (e.Destination.Equals("v3")) UpdateFlow(t, e);
            foreach (Edge<String, EdgeData> e in t.GetOutgoingEdges("v3")) if (e.Destination.Equals("v2")) UpdateFlow(t, e);
            foreach (Edge<String, EdgeData> e in t.GetOutgoingEdges("v2")) if (e.Destination.Equals("v4")) UpdateFlow(t, e);
            foreach (Edge<String, EdgeData> e in t.GetOutgoingEdges("v4")) if (e.Destination.Equals("t")) UpdateFlow(t, e);
            foreach (Edge<string, EdgeData> e in t.GetOutgoingEdges("v1")) if (e.Destination.Equals("s")) e.Data.ResidualCapacity += 4;
            foreach (Edge<string, EdgeData> e in t.GetOutgoingEdges("v3")) if (e.Destination.Equals("v1")) e.Data.ResidualCapacity += 4; 
            foreach (Edge<string, EdgeData> e in t.GetOutgoingEdges("v2")) if (e.Destination.Equals("v3")) e.Data.ResidualCapacity += 4; 
            foreach (Edge<string, EdgeData> e in t.GetOutgoingEdges("v4")) if (e.Destination.Equals("v2")) e.Data.ResidualCapacity += 4; 
            foreach (Edge<string, EdgeData> e in t.GetOutgoingEdges("t")) if (e.Destination.Equals("v4")) e.Data.ResidualCapacity += 4;
            t.FindMaxFlow("s", "t");
            Assert.That(t.FlowFrom("s") == 23);
            Assert.That(t.FlowFrom("v4") == 11);
            Assert.That(t.FlowFrom("v3") == 19);
            Assert.That(t.FlowFrom("v2") == 11);
            Assert.That(t.FlowFrom("v1") == 12);
        }
    }
}