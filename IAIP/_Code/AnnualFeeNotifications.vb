Imports System.Collections.Generic
Imports System.Configuration
Imports System.Linq
Imports System.Threading.Tasks
Imports Iaip.ApiCalls.EmailQueue

Namespace AnnualFeesCode
    Module AnnualFeeNotifications

        Public Async Function SendAnnualFeeNotificationAsync(dv As DataView, feeYear As Integer, deadline As Date) As Task(Of EmailQueueResponse)
            Dim emails As New List(Of NewEmailTask)
            Dim gecoUrl As String = ConfigurationManager.AppSettings("GecoUrl")
            Dim deadlineFormatted As String = deadline.ToString("MMMM d, yyyy")

            For Each rowView As DataRowView In dv
                Dim recipientsList As List(Of String) = rowView("Emails").ToString().Split(","c).Select(Function(s) s.Trim()).ToList()
                Dim airsNumber As String = rowView("Airs No.").ToString().Trim()
                Dim airsNumberFormatted As String = $"{Left(airsNumber, 3)}-{Right(airsNumber, 5)}"
                Dim facilityName As String = rowView("Facility Name (snapshot)").ToString().Trim()

                Dim newEmail As New NewEmailTask() With {
                    .From = "GeorgiaAirProtectionBranch@dnr.ga.gov",
                    .Recipients = recipientsList,
                    .Subject = $"Data Collection for {feeYear} Calendar Year Emission Fees (Facility ID {airsNumberFormatted}: {facilityName})",
                    .Body = EmailBody(feeYear, deadlineFormatted, gecoUrl)
                }

                emails.Add(newEmail)
            Next

            Return Await SendEmailsAsync(emails.ToArray()).ConfigureAwait(False)
        End Function

        Private Function EmailBody(feeYear As Integer, deadline As String, gecoUrl As String) As String
            Return $"Dear Sir/Madam:

This letter is a notification that you must complete the Georgia Air Emission Fees Reporting form for the {feeYear} calendar year. You must complete and submit the Emission Fees Reporting form, even if no fees are due. For the {feeYear} calendar year, you should report your respective fee amount via an ""online fee form."" This online form will eliminate the need to send paper forms and allow immediate reporting. Additionally, this online form has features to check the data, reducing the need to contact you for additional or corrected information.

You must complete your online Emission Fees Reporting form by {deadline}. You must still complete the online form if your facility did not operate during the calendar year. 

Please refer to the following links for information and instructions on accessing the forms:

    * GECO Registration and Information: {gecoUrl}files/Generating-an-Invoice-on-GECO.pdf
    * Generating an Invoice on GECO: {gecoUrl}files/GECO-Information-Sheet.pdf

Sincerely,
Lydia Davis
Fee Unit Manager
Financial Unit
"
        End Function

    End Module
End Namespace
