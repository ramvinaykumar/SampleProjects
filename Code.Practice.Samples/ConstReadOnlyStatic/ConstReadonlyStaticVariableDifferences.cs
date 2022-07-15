namespace Code.Practice.Samples.ConstReadOnlyStatic
{
    public class ConstReadonlyStaticVariableDifferences
    {
        private readonly int rollNumber;
        /// <summary>
        /// Const variable value can not be changed any where in the program
        /// </summary>
        private const string myName = "Ram Vinay Kumar";
        private static readonly int empNumber;

        /// <summary>
        /// Public Constructor
        /// </summary>
        public ConstReadonlyStaticVariableDifferences()
        {
            // Readonly variable value can be only changed in public constructor
            rollNumber = 002311789;

            // if we'll put here the static readonly variable, it'll throw compile error
            // empNumber = 222;

            // myName = "dfda";
        }

        /// <summary>
        /// Static construcotr
        /// </summary>
        static ConstReadonlyStaticVariableDifferences()
        {
            // Static readonly variable value can be only changed in static constructor and can be changed only one time
            empNumber = 11789;

            // readonly variable can not be used inside static constructor, it'll throw compile error
            // rollNumber = 000;

        }

    }
}
