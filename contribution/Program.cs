using System.Globalization;

namespace contribution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userInput = Console.ReadLine();
            Console.WriteLine(Calculate(userInput));
        }

        static double Calculate(string userInput)
        {
           var userInputSplit = userInput.Split(" ");

           var (sum, percent, months) = (
                double.Parse(userInputSplit[0], CultureInfo.InvariantCulture), 
                double.Parse(userInputSplit[1]), 
                double.Parse(userInputSplit[2])
           );

           return sum * Math.Pow(1.0 + (percent / 100.0) / 12.0, months);
        }
    }
}
