/* EdgeData.cs
 * Author: Jonas Bronson
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkFlow
{
    /// <summary>
    /// The EdgeData class.
    /// </summary>
    public class EdgeData
    {
        /// <summary>
        /// The flow of the edge.
        /// </summary>
        private int _flow;

        /// <summary>
        /// The capacity of the edge.
        /// </summary>
        public int Capacity { get; }

        /// <summary>
        /// Gets or sets the flow of the edge.
        /// </summary>
        public int Flow 
        {
            get
            {
                return _flow;
            }
            set
            {
                if(value < 0 || value > Capacity)
                {
                    throw new ArgumentException();
                }
                _flow = value;
            }
        }

        /// <summary>
        /// Gets or sets the residual capacity of the edge.
        /// </summary>
        public int ResidualCapacity { get; set; }

        public EdgeData(int cap)
        {
            if(cap < 0)
            {
                throw new ArgumentException();
            }
            Capacity = cap;
            ResidualCapacity = cap;
        }

        /// <summary>
        /// A ToString that returns information in a helpful format.
        /// </summary>
        /// <returns>COmpleted string with information about the edge.</returns>
        public override string ToString()
        {
            return "flow: " + Flow + "; capacity: " + Capacity + "; residual capacity: " + ResidualCapacity;
        }
    }
}
