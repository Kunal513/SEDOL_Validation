using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOL_Validation
{
    public class ValidationClass : ISedolValidator
    {
        public ISedolValidationResult ValidateSedol(string input)
        {
            return new Validation(input);
        }
    }

    class Validation : ISedolValidationResult
    {
        string input = string.Empty;
        bool is_ValidSedol = false;
        bool is_UserDefined = false;
        string validationOp = string.Empty;
        int user_defined_idx = 0;
        char user_defined_char = '9';
        public Validation(string ip)
        {
            input = ip;
        }
        public string InputString { get { return input; } }
        public bool IsValidSedol { get { return isValidSedol(); } }
        public bool IsUserDefined { get { return isUserDefined(); } }
        public string ValidationDetails { get { return validationOp; } }

        private bool isValidSedol()
        {
            if (input == null || string.IsNullOrEmpty(input.Trim()) || (!string.IsNullOrEmpty(input.Trim()) && input.Trim().Length != 7))
            {
                validationOp = "Input string was not 7-characters long";
                is_ValidSedol = false;
            }
            else if (!(input.All(char.IsLetterOrDigit)))
            {
                validationOp = "SEDOL contains invalid characters";
                is_ValidSedol = false;
            }
            else if (!check_char())
            {
                validationOp = "Checksum digit does not agree with the rest of the input";
                is_ValidSedol = false;
            }
            else
            {
                validationOp = "Null";
                is_ValidSedol = true;
            }
            return is_ValidSedol;
        }

        private bool isUserDefined()
        {
            if (input != null && input.Length > 0 && input[user_defined_idx] == user_defined_char && input.All(char.IsLetterOrDigit))
                is_UserDefined = true;

            return is_UserDefined;
        }

        private bool check_char()
        {
            bool isValidCharSet = false;
            int[] weights = { 1, 3, 1, 7, 3, 9 };            
            int sum = 0;
            for (int i = 0; i < (weights.Length > input.Length ? input.Length : weights.Length); i++)
                sum = sum + (weights[i] * char_Code(input[i]));
            while (sum > 10)
                sum = 10 - (sum % 10);
            if (Convert.ToString(sum) == input[input.Length - 1].ToString())
                isValidCharSet = true;
            return isValidCharSet;
        }

        private int char_Code(char singleChar)
        {
            if (char.IsLetter(singleChar))
                return Char.ToUpper(singleChar) - 55;
            return Convert.ToInt16(singleChar-48);
        }
    }
}
