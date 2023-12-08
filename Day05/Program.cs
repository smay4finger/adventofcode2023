
using System.Diagnostics;
static long LowestInRange(Almanac almanac, long start, long length)
{
    Console.WriteLine($"calculating from {start} to {start + length - 1}");
    long lowest = long.MaxValue;
    for (long seed = start; seed <= length; seed++)
    {
        var location = almanac.SeedToLocation(seed);
        if (location < lowest)
            lowest = location;
    }
    return lowest;
}

var stopWatch = new Stopwatch();


var exampleAlmanac = new Almanac("Day05_Example.txt");

if (exampleAlmanac.Seeds.Select(s => exampleAlmanac.SeedToLocation(s)).Min() != 35) throw new Exception();
if (exampleAlmanac.RangeSeeds.Select(s => exampleAlmanac.SeedToLocation(s)).Min() != 46) throw new Exception();

var exapmleSeedRanges = exampleAlmanac.Seeds.Chunk(2);
stopWatch.Start();
var exampleResult = from seedRange in exapmleSeedRanges.AsParallel()
                    select LowestInRange(exampleAlmanac, seedRange[0], seedRange[1]);
var exampleLowest = exampleResult.Min();
stopWatch.Stop();
Console.WriteLine($"Example part 2 needed {stopWatch.Elapsed}");

var almanac = new Almanac("Day05.txt");
Console.WriteLine($"Part 1: lowest location is {almanac.Seeds.Select(s => almanac.SeedToLocation(s)).Min()}");

var seedRanges = almanac.Seeds.Chunk(2);


stopWatch.Start();
var result = from seedRange in seedRanges.AsParallel().WithDegreeOfParallelism(5)
             select LowestInRange(almanac, seedRange[0], seedRange[1]);
var lowestLocation = result.Min();
stopWatch.Stop();

Console.WriteLine($"Part 2: lowest location is {lowestLocation}, this calculation was done in {stopWatch.Elapsed}");