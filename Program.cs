using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDOL_Validation
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] testData = { "   ", string.Empty, null, "", "12", "123456789", "1234567", "0709954", "B0YBKJ7", "9123451", "9ABCDE8", "9123_51", "VA.CDE8", "9123458", "9ABCDE1" };
            Console.WriteLine("Prgram starts..................................");
            for (int i = 0; i < testData.Length; i++)
            {
                ISedolValidationResult val = new Validation(testData[i]);
                Console.WriteLine("##############################################################");
                Console.WriteLine("Validating and printing output...........................");
                Console.WriteLine("Input string: " + val.InputString);
                Console.WriteLine("Is Valid Sedol :" + val.IsValidSedol);
                Console.WriteLine("Is User Defined :" + val.IsUserDefined);
                Console.WriteLine("Validation Details: " + val.ValidationDetails);
                Console.ReadKey();
            }
            Console.WriteLine("Program End........................................");
        }
    }
}
