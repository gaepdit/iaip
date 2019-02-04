using Iaip;
using Iaip.DAL;
using IAIP.Testx.ExtensionMethods;
using Xunit;

namespace IAIP.Testx.ControlTests
{
    public class AirsNumberTextBoxTests
    {
        private AirsNumberTextBox airs;

        public AirsNumberTextBoxTests()
        {
            airs = new AirsNumberTextBox();
        }

        [Fact]
        private void InitialValues()
        {
            Assert.Null(airs.AirsNumber);
            Assert.Equal(9, airs.MaxLength);
            Assert.Equal("000-00000", airs.Cue);
            Assert.False(airs.FacilityMustExist);
            Assert.Equal(AirsNumberValidationResult.Empty, airs.ValidationStatus);
        }

        [Theory]
        [InlineData("111")]
        [InlineData("ABC")]
        [InlineData("0010001")]
        [InlineData("001-0001")]
        private void InvalidFormat(string input)
        {
            airs.Text = input;
            bool result = airs.Validate();

            Assert.True(result);
            Assert.Equal(AirsNumberValidationResult.InvalidFormat, airs.ValidationStatus);
            Assert.Equal(input, airs.Text);
        }
        
        [Theory]
        [InlineData("00100001")]
        [InlineData("001-00001")]
        [InlineData("99999999")]
        [InlineData("999-99999")]
        private void ValidFormat(string input)
        {
            airs.Text = input;
            bool result = airs.Validate();

            Assert.True(result);
            Assert.Equal(AirsNumberValidationResult.Valid, airs.ValidationStatus);
            Assert.Equal(string.Concat(input.Substring(0, 3), "-", input.Substring(input.Length - 5)), airs.Text);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        private void EmptyInput(string input)
        {
            airs.Text = input;
            bool result = airs.Validate();

            Assert.True(result);
            Assert.Equal(AirsNumberValidationResult.Empty, airs.ValidationStatus);
            Assert.Equal(string.Empty, airs.Text);
        }
        
        [Fact]
        private void ValidationStatusChanged()
        {
            airs.ValidationStatusChanged += Airs_ValidationStatusChanged;

            Assert.Null(airs.Tag);

            airs.Text = "001-00001";
            airs.Validate();

            Assert.NotNull(airs.Tag);
            Assert.True((bool) airs.Tag);
        }

        private void Airs_ValidationStatusChanged(object sender, System.EventArgs e)
        {
            airs.Tag = true;
        }
    }
}
