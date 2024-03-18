using System.Globalization;
using CsvHelper;
using DrillBitSizer;

class Program
{
    public static void WriteBracketedValue(decimal number, SortedDictionary<decimal, DrillBitSize> dict)
    {
        decimal closest;
        if (dict.ContainsKey(number))
        {
            closest = number;
        }
        else
        {
            var lower = dict.LowerKey(number);
            var higher = dict.HigherKey(number);
            closest = number - lower < higher - number ? lower : higher;
        }

        var bracketedValues = new[]
        {
            dict.LowerKey(closest),
            closest,
            dict.HigherKey(closest)
        };

        foreach (var bracketedValue in bracketedValues)
        {
            Console.WriteLine($"{dict[bracketedValue].GetSize(),10} ({bracketedValue:F4}, {GetPercentError(bracketedValue, number),5:F2}% error)");
        }
    }

    public static decimal GetPercentError(decimal expected, decimal actual)
    {
        return (expected - actual) / actual * 100;
    }

    public static void Main(string[] args)
    {
        var drillBitSizesByDecimalInches = new SortedDictionary<decimal, DrillBitSize>();

        using (var reader = new StreamReader("SizeChart.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<DrillBitSize>();
            foreach (var record in records)
            {
                drillBitSizesByDecimalInches[record.DecimalInches] = record;
            }
        }
        
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (decimal.TryParse(input, out decimal number))
            {
                if (number <= new decimal(0.0004) || number >= new decimal(0.9922))
                {
                    Console.WriteLine("out of range");
                }
                else
                {
                    WriteBracketedValue(number, drillBitSizesByDecimalInches);
                }
            }
            else
            {
                Console.WriteLine("bad input");
            }
            
            Console.WriteLine();
        }
    }
}