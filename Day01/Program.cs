using System.Diagnostics;

static int Part1(string[] lines)
{
    var sum = 0;
    foreach (var line in lines)
    {
        var digits = line.Where(c => char.IsDigit(c));
        sum += int.Parse($"{digits.First()}{digits.Last()}");

    }
    return sum;
}

static int Part2(string[] lines)
{
    var sum = 0;
    foreach (var line in lines)
    {
        Span<char> chars = line.ToCharArray();
        for (int i = 0; i < line.Length; i++)
        {
            if (chars[i..].ToString().StartsWith("one")) chars[i] = '1';
            if (chars[i..].ToString().StartsWith("two")) chars[i] = '2';
            if (chars[i..].ToString().StartsWith("three")) chars[i] = '3';
            if (chars[i..].ToString().StartsWith("four")) chars[i] = '4';
            if (chars[i..].ToString().StartsWith("five")) chars[i] = '5';
            if (chars[i..].ToString().StartsWith("six")) chars[i] = '6';
            if (chars[i..].ToString().StartsWith("seven")) chars[i] = '7';
            if (chars[i..].ToString().StartsWith("eight")) chars[i] = '8';
            if (chars[i..].ToString().StartsWith("nine")) chars[i] = '9';
            if (chars[i..].ToString().StartsWith("zero")) chars[i] = '0';
        }
        var digits = chars.ToString().Where(c => char.IsDigit(c));
        sum += int.Parse($"{digits.First()}{digits.Last()}");

    }
    return sum;
}

var example1 = File.ReadAllLines("Day01_Example1.txt");
Debug.Assert(Part1(example1) == 142);

var example2 = File.ReadAllLines("Day01_Example2.txt");
Debug.Assert(Part2(example2) == 281);

var lines = File.ReadAllLines("Day01.txt");
Console.WriteLine($"Part 1: sum is {Part1(lines)}");
Console.WriteLine($"Part 2: sum is {Part2(lines)}");
