using Code.Practice.Samples.Accounting;
using Code.Practice.Samples.BankAccountNumberValidation;
using Code.Practice.Samples.Basics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Practice.Samples
{
    /// <summary>
    /// Main class for Program
    /// </summary>
    class Program
    {
        /// <summary>
        /// Static variable of repository
        /// </summary>
        public static InvoiceRepository invoiceRepository;

        /// <summary>
        /// Main method of the class
        /// </summary>
        /// <param name="args">string[] args</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Dictionary Data Linq
            // DictionaryDataLinq();

            // Palindrom
            // CheckPalindrome();
            // CheckPalindromWord();

            // Check whether a string can be segmented or not
            //string[] listOfWord = { "leet", "code" };
            //var segmentWord = WordBreak("leetcode", listOfWord);

            //string[] listOfWord = { "apple", "pear", "pie", "fire", "wood" };
            //var segmentWord = WordBreak("firewood", listOfWord);
            //Console.WriteLine("Word segment status ==>>" + segmentWord);

            //int[] set = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //int sumNumber = 5;

            //foreach (string s in GetCombinations(set, sumNumber, ""))
            //{
            //    Console.WriteLine(s);
            //}

            // FindCombinations(sumNumber, 6);

            // Invoice
            //invoiceRepository = new InvoiceRepository(InvoiceData().AsQueryable());
            //Console.WriteLine("GetTotal() Function getting called");
            //Console.WriteLine("Get Total Value ==>>" + invoiceRepository.GetTotal(111));
            //Console.WriteLine("GetTotal() Function calling is done.");
            //Console.WriteLine("GetTotalOfUnpaid() Function getting called");
            //Console.WriteLine("Get Total Of Unpaid ==>>" + invoiceRepository.GetTotalOfUnpaid());
            //Console.WriteLine("GetTotalOfUnpaid() Function calling is done.");

            //Console.WriteLine("GetItemsReport() Function getting called");
            //var report = invoiceRepository.GetItemsReport(null, null);
            //Console.WriteLine(report);
            //Console.WriteLine("GetItemsReport() Function calling is done.");

            //Category category = new Category();

            //var result201 = category.Solution(201);
            //Console.WriteLine("Category ID == 201");
            //Console.WriteLine(result201);

            //var result101 = category.Solution(101);
            //Console.WriteLine("Category ID == 101");
            //Console.WriteLine(result101);

            //var result109 = category.Solution(109);
            //Console.WriteLine("Category ID == 109");
            //Console.WriteLine(result109);

            //var result202 = category.Solution(202);
            //Console.WriteLine("Category ID == 202");
            //Console.WriteLine(result202);

            // Get get Enum name by its value as integer

            //EnumCoding enumCoding = new EnumCoding();
            //enumCoding.GetEnumName();

            // iterate enum variables
            //enumCoding.IterateEnumVariables();

            // Reverse the string
            ReverseString reverse = new ReverseString();
            //reverse.ReverseStringUsingForLoop("Keshav");
            //Console.WriteLine("Reversed String UsingForLoop == " + reverse.ReverseStringUsingForLoop("Keshav"));
            //Console.WriteLine("Reversed String UsingLINQ == " + reverse.ReverseStringUsingLINQ("Keshav"));
            //Console.WriteLine("Reversed String UsingInBuildFunction == " + reverse.ReverseStringUsingInBuildFunction("Keshav"));
            //Console.WriteLine("Reversed String UsingWhile == " + reverse.ReverseStringUsingWhile("Keshav"));


            //var validCharacter = "()[]{}";
            //var inputStringCorrect = "()[]{}";
            //var wrongData = "(]";
            //var isFound = false;
            //var validateArray = inputStringCorrect.ToCharArray();

            //for (int i = 0; i < validateArray.Length; i++)
            //{
            //    var data = validateArray[i].ToString();
            //    if ((data.StartsWith('(') && (data.EndsWith(')'))) || ((data.StartsWith('[')) && (data.EndsWith(']'))) || ((data.StartsWith('{')) && (data.EndsWith('}'))))
            //    {
            //        isFound = true;
            //    }
            //}
            //Console.WriteLine("The output is like ==>" + isFound);

            // Remove adjusent character

            //string str9 = "acbbcdd";
            //Console.Write(remove(str9) + "\n");

            // Convert byte array
            //byte[] byteArray = Encoding.Default.GetBytes("1111111111");
            //Console.WriteLine($"Byte Array is: {string.Join(" ", byteArray)}");
            //string str = Encoding.Default.GetString(byteArray);
            //Console.WriteLine($"String is: {str}");
            //byte[] encodedByteArray = Encoding.Default.GetBytes((str));
            //Console.WriteLine($"Encoded ByteArray is: {string.Join(" ", encodedByteArray)}");

            // Validate the given bank account number with correct format
            //ValidatePaymentInputRequestDto inputRequestDto = new ValidatePaymentInputRequestDto();
            //inputRequestDto.BankName = "OCBC";
            //inputRequestDto.BankAccountNumber = "9999999999"; // DBS
            //inputRequestDto.BankAccountNumber = "999999999"; // POSB
            //inputRequestDto.BankAccountNumber = "9999999999"; // OCBC
            //inputRequestDto.BankAccountNumber = "999999999999"; // OCBC
            //inputRequestDto.BankAccountNumber = "9999999999"; // UOB

            //BankAccountValidationService validationService = new BankAccountValidationService();
            //validationService.A55ValidatePaymentInputAsync(inputRequestDto);

            Console.ReadLine();
        }

        private static string removeString(string str, char last_removed)
        {
            if (str.Length == 0 || str.Length == 1)
                return str;

            if (str[0] == str[1])
            {
                last_removed = str[0];
                while (str.Length > 1 && str[0] ==
                       str[1])
                {
                    str = str.Substring(1, str.Length - 1);
                }
                str = str.Substring(1, str.Length - 1);
                return removeString(str, last_removed);
            }

            string rem_str = removeString(str.Substring(1, str.Length - 1), last_removed);

            if (rem_str.Length != 0 && rem_str[0] == str[0])
            {
                last_removed = str[0];

                // Remove first character
                return rem_str.Substring(1, rem_str.Length - 1);
            }

            if (rem_str.Length == 0 && last_removed == str[0])
                return rem_str;

            return (str[0] + rem_str);
        }

        static string remove(string str)
        {
            char last_removed = '\0';
            return removeString(str, last_removed);
        }

        /// <summary>
        /// Dictionary Data Linq
        /// </summary>
        private static void DictionaryDataLinq()
        {
            // init 
            var orderList = new List<Order>();
            orderList.Add(new Order(1, 1, 2010, true));//(orderId, customerId, year, isPayed)
            orderList.Add(new Order(2, 2, 2010, true));
            orderList.Add(new Order(3, 1, 2010, true));
            orderList.Add(new Order(4, 2, 2011, true));
            orderList.Add(new Order(5, 2, 2011, false));
            orderList.Add(new Order(6, 1, 2011, true));
            orderList.Add(new Order(7, 3, 2012, false));

            // lookup Order by its id (1:1, so usual dictionary is ok)
            Dictionary<Int32, Order> orders = orderList.ToDictionary(o => o.OrderId, o => o);

            // lookup Order by customer (1:n) 
            // would need something like Dictionary<Int32, IEnumerable<Order>> orderIdByCustomer
            ILookup<Int32, Order> byCustomerId = orderList.ToLookup(o => o.CustomerId);
            foreach (var customerOrders in byCustomerId)
            {
                Console.WriteLine("Customer {0} ordered:", customerOrders.Key);
                foreach (var order in customerOrders)
                {
                    Console.WriteLine("    Order {0} is payed: {1}", order.OrderId, order.IsPayed);
                }
            }

            // the same using old fashioned Dictionary
            Dictionary<Int32, List<Order>> orderIdByCustomer;
            orderIdByCustomer = byCustomerId.ToDictionary(g => g.Key, g => g.ToList());
            foreach (var customerOrders in orderIdByCustomer)
            {
                Console.WriteLine("Customer {0} ordered:", customerOrders.Key);
                foreach (var order in customerOrders.Value)
                {
                    Console.WriteLine("    Order {0} is payed: {1}", order.OrderId, order.IsPayed);
                }
            }

            // lookup Order by payment status (1:m) 
            // would need something like Dictionary<Boolean, IEnumerable<Order>> orderIdByIsPayed
            ILookup<Boolean, Order> byPayment = orderList.ToLookup(o => o.IsPayed);
            IEnumerable<Order> payedOrders = byPayment[false];
            foreach (var payedOrder in payedOrders)
            {
                Console.WriteLine("Order {0} from Customer {1} is not payed.", payedOrder.OrderId, payedOrder.CustomerId);
            }
        }

        /// <summary>
        /// Sample data for testing Invoice
        /// </summary>
        private static List<Invoice> InvoiceData()
        {
            List<Invoice> invoice = new List<Invoice>();

            var item1 = new List<InvoiceItem>() { new InvoiceItem() { Count = 10, Name = "A", Price = 50 }, new InvoiceItem() { Count = 23, Name = "B", Price = 508 } };
            invoice.Add(new Invoice { AcceptanceDate = null, Buyer = "Buyer 1", CreationDate = DateTime.UtcNow.Date, Description = "Response status code does not indicate success", Id = 111, Number = "Number 222", Seller = "Seller KBV", InvoiceItems = item1 });

            var item2 = new List<InvoiceItem>() { new InvoiceItem() { Count = 1, Name = "AA", Price = 5 }, new InvoiceItem() { Count = 123, Name = "1B", Price = 1508 } };
            invoice.Add(new Invoice { AcceptanceDate = DateTime.UtcNow.Date.AddDays(5), Buyer = "Buyer 2", CreationDate = DateTime.UtcNow.Date, Description = "Response status code does not indicate success 123", Id = 222, Number = "Number 234", Seller = "KBV Seller 2", InvoiceItems = item2 });

            var item3 = new List<InvoiceItem>() { new InvoiceItem() { Count = 110, Name = "11A", Price = 150 }, new InvoiceItem() { Count = 213, Name = "B11", Price = 5018 } };
            invoice.Add(new Invoice { AcceptanceDate = null, Buyer = "Buyer 3", CreationDate = DateTime.UtcNow.Date, Description = "Response status code does not indicate success Inv 123 ", Id = 333, Number = "Number 212", Seller = "Seller 3", InvoiceItems = item3 });

            var item4 = new List<InvoiceItem>() { new InvoiceItem() { Count = 102, Name = "A22", Price = 5022 }, new InvoiceItem() { Count = 223, Name = "B33", Price = 5038 } };
            invoice.Add(new Invoice { AcceptanceDate = DateTime.UtcNow.Date.AddDays(45), Buyer = "Buyer 4", CreationDate = DateTime.UtcNow.Date, Description = "Inv 123 Response status code does not indicate success", Id = 444, Number = "Number 555", Seller = "Seller 4", InvoiceItems = item4 });

            var item5 = new List<InvoiceItem>() { new InvoiceItem() { Count = 310, Name = "AKKK", Price = 5077 }, new InvoiceItem() { Count = 283, Name = "UUHHB", Price = 509988 } };
            invoice.Add(new Invoice { AcceptanceDate = DateTime.UtcNow.Date.AddDays(30), Buyer = "Buyer 5", CreationDate = DateTime.UtcNow.Date, Description = "Response status code does not indicate success 4443 GDGSFDG", Id = 555, Number = "Number 342", Seller = "Seller 5", InvoiceItems = item5 });
            return invoice;
        }

        /// <summary>
        /// Program to check whether a number is palindrom or not
        /// </summary>
        private static void CheckPalindrome()
        {
            int num, rem, sum = 0, temp;
            Console.WriteLine("\n >>>> To Find a Number is Palindrome or not <<<< ");
            Console.Write("\n Enter a number: ");
            num = Convert.ToInt32(Console.ReadLine());
            temp = num;
            while (num > 0)
            {
                rem = num % 10; //for getting remainder by dividing with 10    1221
                num = num / 10; //for getting quotient by dividing with 10 10004545000   
                sum = sum * 10 + rem; // multiplying the sum with 10 and adding remainder
            }
            Console.WriteLine("\n The Reversed Number is: {0} \n", sum);
            if (temp == sum) //checking whether the reversed number is equal to entered number    
            {
                Console.WriteLine("\n Number is Palindrome \n\n");
            }
            else
            {
                Console.WriteLine("\n Number is not a palindrome \n\n");
            }
        }

        /// <summary>
        /// Check whether given word is palindrom or not
        /// </summary>
        private static void CheckPalindromWord()
        {
            string _inputstr, _reversestr = string.Empty;
            Console.Write("Enter a string : ");
            _inputstr = Console.ReadLine();
            if (_inputstr != null)
            {
                for (int i = _inputstr.Length - 1; i >= 0; i--)
                {
                    _reversestr += _inputstr[i].ToString();
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
        }

        private static bool WordBreak(string s, string[] lstWord)
        {
            if (lstWord == null || s == null)
                return false;

            var count = s.Length;
            var dp = new bool[count + 1];

            dp[0] = true;
            for (var i = 1; i < dp.Length; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    var word = s.Substring(j, i - j);
                    if (lstWord.Contains(word) && dp[j])
                    {
                        dp[i] = true;
                        break;
                    }
                }
            }

            return dp[count];
        }

        /// <summary>
        /// I want to find out several combinations in the set of number such that the summation of it equal to a known number, for example, 18. 
        /// We can find out that 5, 6, 7 is matched (5+6+7=18). Numbers in a combination cannot be repeated and the number in a set may not be consecutive.
        /// </summary>
        /// <param name="set">array of numbers on which need to find the combination </param>
        /// <param name="sum">The number which sum looking from set of numbers</param>
        /// <param name="values">it will be used for storing the combination</param>
        /// <returns>Returns the possible combination</returns>
        private static IEnumerable<string> GetCombinations(int[] set, int sum, string values)
        {
            for (int i = 0; i < set.Length; i++)
            {
                int left = sum - set[i];
                string vals = set[i] + "," + values;
                if (left == 0)
                {
                    yield return vals;
                }
                else
                {
                    int[] possible = set.Take(i).Where(n => n <= sum).ToArray();
                    if (possible.Length > 0)
                    {
                        foreach (string s in GetCombinations(possible, left, vals))
                        {
                            yield return s;
                        }
                    }
                }
            }
        }

        private static void FindCombinations(int n, int k)
        {
            Console.WriteLine("Given Number: " + n + ", required sum K: " + k);

            List<Int32> combinationList = new List<int>();
            CombinationUtil(n, k, 0, 1, combinationList);
        }

        public static void CombinationUtil(int N, int sum, int currSum, int startNumber, List<int> combinationList)
        {
            if (currSum == sum)
            {
                Console.WriteLine(combinationList);
                return;
            }

            for (int i = startNumber; i <= N; i++)
            {
                if (currSum + i > sum)
                    break;
                combinationList.Add(i);
                CombinationUtil(N, sum, currSum + i, i, combinationList);
                combinationList.Remove(combinationList.Count - 1);
            }
        }

        /// <summary>
        /// Order class
        /// </summary>
        class Order
        {
            /// <summary>
            /// OrderId
            /// </summary>
            public Int32 OrderId { get; private set; }

            /// <summary>
            /// CustomerId
            /// </summary>
            public Int32 CustomerId { get; private set; }

            /// <summary>
            /// Year
            /// </summary>
            public Int32 Year { get; private set; }

            /// <summary>
            /// IsPayed
            /// </summary>
            public Boolean IsPayed { get; private set; }

            // additional properties
            // private List<OrderItem> _items;

            /// <summary>
            /// Parameterized constructor
            /// </summary>
            /// <param name="orderId">Int32 orderId</param>
            /// <param name="customerId">Int32 customerId</param>
            /// <param name="year">Int32 year</param>
            /// <param name="isPayed">Boolean isPayed</param>
            public Order(Int32 orderId, Int32 customerId, Int32 year, Boolean isPayed)
            {
                OrderId = orderId;
                CustomerId = customerId;
                Year = year;
                IsPayed = isPayed;
            }
        }
    }
}
