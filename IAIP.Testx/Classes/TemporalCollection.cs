using Iaip;
using System;
using System.Collections.Generic;
using Xunit;

namespace IAIP.Testx.ClassesTests
{
    public class TemporalCollection
    {
        private TemporalCollection<string> collection;

        public TemporalCollection()
        {
            Dictionary<DateTime, string> dict = new Dictionary<DateTime, string>
            {
                { new DateTime(2015,1,1), "abc" },
                { new DateTime(2016,1,1), "def" },
                { new DateTime(2017,1,1), "ghi" },
                { new DateTime(2018,1,1), "jkl" },
                { new DateTime(2089,1,1), "uvw" },
                { new DateTime(2099,1,1), "xyz" }
            };

            collection = new TemporalCollection<string>(dict);
        }

        [Fact]
        public void CheckValues()
        {
            Assert.Equal("abc", collection.GetValueAt(new DateTime(2015, 1, 1)));
            Assert.Equal("def", collection.GetValueAt(new DateTime(2016, 6, 1)));
            Assert.Equal(collection.GetValueAt(new DateTime(2017, 4, 1)), collection.GetValueAt(new DateTime(2017, 8, 1)));
        }

        [Fact]
        public void CheckCurrentValues()
        {
            Assert.Equal("jkl", collection.GetCurrentValue());
        }

        [Fact]
        public void CheckFutureValues()
        {
            Assert.Equal("uvw", collection.GetValueAt(new DateTime(2095, 1, 1)));
            Assert.Equal("xyz", collection.GetValueAt(new DateTime(2150, 1, 1)));
        }

        [Fact]
        public void CheckEarliestDate()
        {
            Assert.Equal(new DateTime(2015, 1, 1), collection.EarliestDate);
        }

        [Fact]
        public void AddNewValue()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.GetValueAt(new DateTime(2010, 1, 1)));
            collection.AddValue(new DateTime(2009, 1, 1), "mno");
            Assert.Equal("mno", collection.GetValueAt(new DateTime(2010, 1, 1)));
        }

        [Fact]
        public void DateOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.GetValueAt(new DateTime(2010, 1, 1)));
        }

        [Fact]
        public void EmptyCollection()
        {
            var collection = new TemporalCollection<string>();

            Assert.Equal(0, collection.Count);

            Assert.Throws<ArgumentOutOfRangeException>(() => collection.GetValueAt(new DateTime(2010, 1, 1)));
            Assert.Throws<ArgumentOutOfRangeException>(() => collection.EarliestDate);
        }
    }
}
