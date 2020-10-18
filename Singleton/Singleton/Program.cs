using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 0;
            string output = string.Empty;
            AppEngine appEngine = null;
            Random random = new Random();

            appEngine = AppEngine.Default;   
            Console.WriteLine(string.Format("Using appEngine ID={0} via property 'Default'!", appEngine.ID));

            input = random.Next(int.MinValue, int.MaxValue);
            output = appEngine.GetBinary(input);
            Console.WriteLine(string.Format("The binary form of {0} is {1}", input, output));

            appEngine = AppEngine.GetDefault();
            Console.WriteLine(string.Format("Using appEngine ID={0} via method 'GetDefault()'!", appEngine.ID));

            input = random.Next(int.MinValue, int.MaxValue);
            output = appEngine.GetBinary(input);
            Console.WriteLine(string.Format("The binary form of {0} is {1}", input, output));

            Console.ReadKey();
        }
    }
}
