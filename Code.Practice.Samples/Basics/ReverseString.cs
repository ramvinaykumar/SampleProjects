using System;
using System.Linq;

namespace Code.Practice.Samples.Basics
{
    public class ReverseString
    {
        public string ReverseStringUsingForLoop(string input)
        {
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    output += input[i];
                }
            }
            return output;
        }

        public string ReverseStringUsingInBuildFunction(string input)
        {
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                // With Inbuilt Method Array.Reverse Method  
                char[] charArray = input.ToCharArray();
                Array.Reverse(charArray);
                output = new string(charArray);
                // Console.WriteLine(new string(charArray));
            }
            return output;
        }

        public string ReverseStringUsingLINQ(string input)
        {
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                // using Linq  
                output = new string(input.ToCharArray().Reverse().ToArray());
            }
            return output;
        }

        public string ReverseStringUsingWhile(string input)
        {
            string output = string.Empty;
            if (!string.IsNullOrWhiteSpace(input))
            {
                int length;

                length = input.Length - 1;

                while (length >= 0)
                {
                    output += input[length];
                    length--;
                }
            }
            return output;
        }
    }
}
