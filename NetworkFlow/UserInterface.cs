/* UserInterface.cs
 * Author: Jonas Bronson
 */

using Ksu.Cis300.Graphs;
using System.Runtime.CompilerServices;

namespace NetworkFlow
{
    /// <summary>
    /// The User Interface for the Network Flow program.
    /// </summary>
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// New network graph that is created.
        /// </summary>
        private NetworkGraph? _networkGraph = null;

        /// <summary>
        /// Loads the network graph into memory and the UI if the requirements are met.
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        /// <exception cref="IOException">If the file has less than 2 nodes.</exception>
        private void UxLoadButtonClick(object sender, EventArgs e)
        {
            string filePath = "";
            List<string> nodes = new();
            try
            {
                if (UxOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = UxOpenFileDialog.FileName;
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            // while condition prevents ReadLine() from being null
                            nodes.Add(reader.ReadLine()!);
                        }
                        if (nodes.Count <= 1) throw new IOException("The network has fewer than 2 nodes.");
                    }
                    string[] nodesArray = nodes.ToArray();
                    NetworkGraph temp = new NetworkGraph(nodesArray);
                    _networkGraph = temp;
                    Populate();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Populates the UI with the correct information.
        /// </summary>
        private void Populate()
        {
            List<string> nodes = new List<string>();
            UxSourceList.Items.Clear();
            UxDestinationList.Items.Clear();
            // will not be null since method is only called after _networkGraph has been instantiated
            foreach(string node in _networkGraph!.Nodes)
            {
                nodes.Add(node);
            }
            nodes.Sort();
            foreach (string node in nodes)
            {
                UxSourceList.Items.Add(node);
                UxDestinationList.Items.Add(node);
            }
            if (nodes.Contains("s")) UxSourceList.SetSelected(nodes.IndexOf("s"), true); 
            else UxSourceList.SetSelected(0, true);
            if (nodes.Contains("t")) UxDestinationList.SetSelected(nodes.IndexOf("t"), true);
            else UxDestinationList.SetSelected(0, true);
            UxGraphText.Text = _networkGraph.GraphDisplay();
        }

        /// <summary>
        /// Run upon clicking the solve button; finds the max flow of the network graph.
        /// </summary>
        /// <param name="sender">Object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void UxSolveButtonClick(object sender, EventArgs e)
        {
            if(_networkGraph != null)
            {
                _networkGraph.ZeroEdgeData();
                // will not be null since method is only called after _networkGraph has been instantiated
                _networkGraph.FindMaxFlow(UxSourceList.SelectedItem.ToString()!, UxDestinationList.SelectedItem.ToString()!);
                UxGraphText.Text = _networkGraph.GraphDisplay();
                // will not be null since method is only called after _networkGraph has been instantiated
                UxFlowText.Text = ("Net Flow from " +  UxSourceList.SelectedItem + " to " + UxDestinationList.SelectedItem + " is " + _networkGraph.FlowFrom(UxSourceList.SelectedItem.ToString()!));
            }
        }
    }
}