using System.Collections.Generic;
using NUnit.Framework;
using Utilities.Runtime;

namespace Utilities.Tests.EditMode.Tests
{
    public sealed class UtilitiesEditModeTests
    {
        [Test]
        public void FormatNumber_ShouldReturnFormattedString()
        {
            Assert.AreEqual("1.5M", TextFormatter.FormatNumber(1500000));
            Assert.AreEqual("1.5k", TextFormatter.FormatNumber(1500));
            Assert.AreEqual("123", TextFormatter.FormatNumber(123));
        }
        
        [Test]
        public void FormatTime_ShouldReturnCorrectFormat()
        {
            string result = TextFormatter.FormatTime(3661);
            
            Assert.AreEqual("00:01:01:01", result);
        }
        
        [Test]
        public void Shuffle_ShouldChangeOrder()
        {
            List<int> original = new List<int> { 1, 2, 3, 4, 5 };
            List<int> shuffled = RuntimeUtilities.Shuffle(new List<int>(original));

            CollectionAssert.AreEquivalent(original, shuffled);
            Assert.AreNotEqual(original, shuffled);
        }
        
        [Test]
        public void BubbleSort_ShouldSortListCorrectly()
        {
            var unsorted = new List<int> { 5, 2, 4, 1, 3 };
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            var result = RuntimeUtilities.BubbleSort(new List<int>(unsorted));

            CollectionAssert.AreEqual(expected, result);
        }
    }
}