using System;
using System.Collections.Generic;
using Iaip;
using Xunit;

namespace IAIP.Testx.ClassesTests
{
    public class ReverseComparerTests
    {
        private SortedList<int, string> mySortedList;
        private SortedList<int, string> reverseSortedList;

        public ReverseComparerTests()
        {
            Dictionary<int, string> dict = new Dictionary<int,string> 
            {
                {1, "abc"},
                {2, "def"},
                {3, "ghi"}
            };

            mySortedList = new SortedList<int, string>(dict);

            reverseSortedList = new SortedList<int, string>(dict, new ReverseComparer<int>(Comparer<int>.Default));
        }

        [Fact]
        public void CompareEachElement()
        {
            int count = mySortedList.Count;

            Assert.Equal(count, reverseSortedList.Count);

            for (int i = 0; i < mySortedList.Count; i++)
            {
                Assert.Equal(mySortedList.Values[i], reverseSortedList.Values[count - 1 - i]);
            }
        }
    }
}
