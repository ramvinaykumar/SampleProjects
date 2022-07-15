using System;
using System.Collections.Generic;
using System.Text;

namespace OpsDev.HiringFramework.PhoneInterview.CodeALive
{
    public class Palindrome
    {
        private string _inputstr;

        public Palindrome(string word)
        {
            this._inputstr = word;



            // read the input and check if the first character of the word and last charater of the word are similar
            var _reversestr = string.Empty;

            for (int j = this._inputstr.Length - 1; j >= 0; j--)
            {
                _reversestr += this._inputstr[j].ToString();
            }
            if (_reversestr == _inputstr)
            {
                Console.WriteLine("String is Palindrome Input = {0} and Output= {1}", _inputstr, _reversestr);
            }
            else
            {
                Console.WriteLine("String is not Palindrome Input = {0} and Output= {1}", _inputstr, _reversestr);
            }
        }

        public bool IsPalindrome()
        {
            var result = TestPalindrome();

            return result;
        }

        private bool TestPalindrome()
        {
            return false;
        }
    }
}
