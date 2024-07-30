/* EdgeTests.cs
 * Author: George Lavezzi
 */
using NetworkFlow;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    /// <summary>
    /// Unit tests for the EdgeData class.
    /// </summary>
    [TestFixture]
    internal class EdgeTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestConstructor()
        {
            EdgeData e1 = new(10);
            Assert.That(e1.Capacity.Equals(10));
            Assert.That(e1.Flow.Equals(0));
            Assert.Throws<ArgumentException>(() => new EdgeData( -10));
        }

        /// <summary>
        /// Tests the Flow property.
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestFlow()
        {
            EdgeData e1 = new( 10);
            Assert.Throws<ArgumentException>(() => e1.Flow = -2);
            Assert.Throws<ArgumentException>(() => e1.Flow = 12);
            e1.Flow = 10;
            Assert.That(e1.Flow == 10);
        }

        /// <summary>
        /// Tests the ResidualCapacity property.
        /// </summary>
        [Test]
        [Timeout(1000)]
        public void TestResdiualCapacity()
        {
            EdgeData e1 = new(10);
            Assert.That(e1.ResidualCapacity == 10);

            e1.ResidualCapacity = 5;
            Assert.That(e1.ResidualCapacity == 5);
        }

    }
}
