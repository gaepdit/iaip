Public Module RegexPatterns

    ' Letters and numbers
    Friend Const AlphaNumericPattern As String = "^[a-zA-Z0-9]+$"
    Friend Const NumericPattern As String = "^[0-9]+$"
    Friend Const AtLeastOneDigitPattern As String = "\d"
    Friend Const AtLeastOneLetterPattern As String = "[a-zA-Z]"
    Friend Const NonAlphabeticPattern As String = "[^a-zA-Z]"

    ' Valid AIRS number formats are in the form 001-00001 or 00100001 or 1-1.
    ' AIRS numbers consist of a three digit county code and a five-digit facility number.
    ' The county code must be odd and can be in the range of 1 through 321 or 777.
    ' The facility number can be any positive integer from 1 to 99999.
    ' The hyphen is optional. If it is included, then leading zeros are also optional. If excluded, leading zeros are required.
    ' Test regex here: https://regex101.com/r/2uYyHl/7
    Friend Const AirsNumberPattern As String = "(?:^(?:777|321|3[0-1][13579]|[0-2][0-9][13579])(?!00000)\d{5})$|^(?:(?:777|321|3[0-1][13579]|[0-2]?[0-9]?[13579])-(?!0{1,5}$)\d{1,5})$"

    ' Valid RMP IDs are in the form 0000-0000-0000 (with the dashes)
    Friend Const RmpIdPattern As String = "^\d{4}-\d{4}-\d{4}$"

    ' Valid SIC codes are one to four digits
    Friend Const SicCodePattern As String = "^\d{1,4}$"

    ' Valid NAICS codes are two to six digits
    Friend Const NaicsCodePattern As String = "^\d{2,6}$"

    ' Valid permit numbers are in the form 0000-000-0000-A-00-? or 0000-ABC-0000-A-00-?
    ' (with the dashes)
    ' Test regex here: https://regex101.com/r/SDDTMz/2
    Friend Const PermitNumberPattern As String = "^\d{4}-(?:[A-Z]{3}|[0-9]{3})-\d{4}-[A-Z]-\d{2}-[A-Z0-9]$"

    ' Official DNR email address:
    '   user.name@dnr.ga.gov
    '   user.name@dnr.state.ga.us
    '   user.name@gadnr.org
    '   user.name@gaepd.com
    '   user.name@gaepd.org
    '   user.name@georgiaepd.com
    '   user.name@georgiaepd.org
    ' Test regex here: https://regexr.com/3dea9
    Public Const DnrEmailPattern As String = "^[\w-\.]+@(dnr\.(ga\.gov|state\.ga\.us)|gadnr\.org|(ga|georgia)epd\.(com|org))$"

    ' Currently unused
    ' See https://stackoverflow.com/questions/123559/a-comprehensive-regex-for-phone-number-validation#comment2795100_123666
    'Friend Const MatchPhoneNumberPattern As String = "^(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]‌​)\s*)|([2-9]1[02-9]|[2-9][02-8]1|[2-9][02-8][02-9]))\s*(?:[.-]\s*)?)?([2-9]1[02-‌​9]|[2-9][02-9]1|[2-9][02-9]{2})\s*(?:[.-]\s*)?([0-9]{4})$"
    ' See http://www.authorcode.com/how-to-use-regular-expression-for-validating-phone-numbers-in-net/
    'Friend Const MatchPhoneNumberPattern As String = "^\(?([2-9][0-9]{2})\)?[-. ]?([2-9][0-9]{2})[-. ]?([0-9]{4})$"

End Module
