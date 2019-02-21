using Xunit;
using static Iaip.DAL.Finance.Invoices;

namespace IAIP.Testx.Models
{
    public class InvoiceIdTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("-1234")]
        [InlineData("E-1234")]
        [InlineData("E1234")]
        [InlineData("E-")]
        public void RejectsInvalidInvoiceId(string input)
        {
            int newInvoiceID = 0;
            Assert.Equal(InvoiceValidationResult.Malformed, ValidateInvoiceId(input, ref newInvoiceID));
            Assert.Equal(0, newInvoiceID);
        }

        [Theory]
        [InlineData("1234")]
        private void AcceptsValidInvoiceId(string input)
        {
            int newInvoiceID = 0;
            Assert.Equal(InvoiceValidationResult.Valid, ValidateInvoiceId(input, ref newInvoiceID));
            Assert.Equal(int.Parse(input), newInvoiceID);
        }
    }
}
