using MicroService.Application.Constant;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MicroService.Application.Common.Helpers
{
    public static class CommonHelper
    {
        public static string GetFullActionMethodName(string controllerName, string actionMethodName)
        {
            return $"{ApplicationConstant.Common.MS_NAME}.{controllerName}.{actionMethodName}";
        }

        /// <summary>
        /// Mask Number
        /// </summary>
        /// <param name="number"></param>
        /// <param name="lengthToMask"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static string MaskNumber(string number, int lengthToMask)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentNullException("CpfAccountNumber is empty");
            }

            if (lengthToMask > 0 && lengthToMask < number.Length)
            {
                number = string.Format(CultureInfo.InvariantCulture, "{0}{1}", number.Substring(0, 1), number.Substring(lengthToMask + 1).PadLeft(number.Length - 1, '*'));
            }

            return number;
        }

        /// <summary>
        /// Process the contact number to validate and append country code
        /// </summary>
        /// <param name="contact">string contact</param>
        /// <param name="countryCode">string countryCode</param>
        /// <returns></returns>
        public static string PrefixCountryCode(string contact, string countryCode)
        {
            if (!string.IsNullOrEmpty(contact) && !string.IsNullOrEmpty(countryCode))
            {
                //check mobile that starts with 65
                if (contact.StartsWith(countryCode) && contact.Length == 10)
                {
                    return ValidateContactNumber(contact.Substring(2, 8));
                }
                //check mobile that starts with +65
                else if (contact.StartsWith("+"+ countryCode) && contact.Length == 11)
                {
                    return ValidateContactNumber(contact.Substring(3, 8));
                }
                //check mobile that starts with 8 or 9
                else
                {
                    return ValidateContactNumber(contact);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Validate the contact number
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public static string ValidateContactNumber(string contact)
        {
            Regex rgx = new Regex(@"^[8,9]{1}\d{7}$");

            bool validContact = rgx.IsMatch(contact);

            if (validContact)
            {
                return ApplicationConstant.Common.COUNTRY_CODE + contact;
            }

            return string.Empty;
        }

        /// <summary>
        /// Return the requested string after substringing as per given number
        /// </summary>
        /// <param name="input">Input data which need to format</param>
        /// <param name="noOfDigit">No of digit that will be returned as result</param>
        /// <returns></returns>
        public static string ReturnRequestedFormattedData(string input, int noOfDigit)
        {
            var formattedString = string.Empty;

            if (!string.IsNullOrEmpty(input) && input.Contains('-'))
            {
                var inputData = input.Replace("-", string.Empty);
                formattedString = inputData.Substring(inputData.Length - noOfDigit);
            }

            return formattedString;
        }
    }
}
