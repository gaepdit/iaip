using Iaip.Apb;
using Xunit;
using static Iaip.Apb.ApbFacilityId;

namespace IAIP.Testx.Models
{
    public class ApbFacilityIdTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("111")]
        [InlineData("ABC")]
        [InlineData("0010001")]
        [InlineData("001-0001")]
        public void RejectsInvalidAirsNumbers(string input)
        {
            Assert.False(IsValidAirsNumberFormat(input));
        }

        [Theory]
        [InlineData("00100001")]
        [InlineData("001-00001")]
        [InlineData("041300100001")]
        [InlineData("04-13-001-00001")]
        private void AcceptsValidAirsNumbers(string input)
        {
            Assert.True(IsValidAirsNumberFormat(input));
        }

        [Fact]
        private void FacilityIdPropertiesCorrect()
        {
            ApbFacilityId airs = new ApbFacilityId("12345678");


            Assert.Equal("12345678", airs.ShortString);
            Assert.Equal("123", airs.CountySubstring);
            Assert.Equal("041312345678", airs.DbFormattedString);
            Assert.Equal("GA0000001312345678", airs.EpaFacilityIdentifier);
            Assert.Equal("123-45678", airs.FormattedString);
            Assert.Equal(12345678, airs.ToInt());
        }

        [Fact]
        private void FacilityIdCTypeWorks()
        {
            ApbFacilityId airs2 = (ApbFacilityId)"123-45678";


            Assert.Equal("12345678", airs2.ShortString);
            Assert.Equal("123", airs2.CountySubstring);
            Assert.Equal("041312345678", airs2.DbFormattedString);
            Assert.Equal("GA0000001312345678", airs2.EpaFacilityIdentifier);
            Assert.Equal("123-45678", airs2.FormattedString);
            Assert.Equal(12345678, airs2.ToInt());
        }
    }
}
