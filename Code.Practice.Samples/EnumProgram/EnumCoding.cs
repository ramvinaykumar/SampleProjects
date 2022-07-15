using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Practice.Samples.EnumProgram
{
    public class EnumCoding
    {
        /// This is used to get Enum name by its value as integer.  
        public void GetEnumName()
        {
            int[] enumValues = new int[] { 0, 1, 3, 5 };
            foreach (int enumValue in enumValues)
            {
                Console.WriteLine(Enum.GetName(typeof(MicrosoftOffice), enumValue));
            }
            Console.ReadKey();
        }

        /// This is used to iterate enum variables.  
        public void IterateEnumVariables()
        {
            Console.WriteLine("List of Enums");
            string[] officenames = Enum.GetNames(typeof(MicrosoftOffice));
            foreach (string officename in officenames)
            {
                Console.WriteLine(officename);
            }
            Console.ReadKey();
        }
    }
}
