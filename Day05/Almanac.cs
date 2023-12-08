// See https://aka.ms/new-console-template for more information
using Day05;

public class Almanac
{
    private readonly Mapping[] seedToSoil;
    private readonly Mapping[] soilToFertilizer;
    private readonly Mapping[] fertilizerToWater;
    private readonly Mapping[] waterToLight;
    private readonly Mapping[] lightToTemperature;
    private readonly Mapping[] temperatureToHumidity;
    private readonly Mapping[] humidityToLocation;
    private readonly long[] seeds;

    public Almanac(string file)
    {
        string[] mappingData = File.ReadAllLines(file);
        seedToSoil = ReadMapping(mappingData, "seed-to-soil").ToArray();
        soilToFertilizer = ReadMapping(mappingData, "soil-to-fertilizer").ToArray();
        fertilizerToWater = ReadMapping(mappingData, "fertilizer-to-water").ToArray();
        waterToLight = ReadMapping(mappingData, "water-to-light").ToArray();
        lightToTemperature = ReadMapping(mappingData, "light-to-temperature").ToArray();
        temperatureToHumidity = ReadMapping(mappingData, "temperature-to-humidity").ToArray();
        humidityToLocation = ReadMapping(mappingData, "humidity-to-location").ToArray();
        seeds = ReadSeeds(mappingData).ToArray();
    }

    private static IEnumerable<long> ReadSeeds(string[] mappingData)
    {
        foreach (var line in mappingData)
            if (line.StartsWith("seeds:"))
                return line[7..].Split(" ").Select(s => long.Parse(s));
        throw new Exception();
    }

    private static IEnumerable<Mapping> ReadMapping(string[] mappingData, string name)
    {
        bool inData = false;
        foreach (var line in mappingData)
        {
            if (line.StartsWith(name + " map:"))
                inData = true;
            else if (line.Trim().Length == 0)
                inData = false;
            else if (inData)
            {
                var data = line.Split(" ");
                if (data.Length != 3) throw new Exception();
                yield return new Mapping(
                    dst: long.Parse(data[0]),
                    src: long.Parse(data[1]),
                    len: long.Parse(data[2]));
            }
        }
    }

    private static long Map(IEnumerable<Mapping> mappings, long value)
    {
        foreach (Mapping mapping in mappings)
            if (mapping.InRange(value))
                return mapping.Convert(value);
        return value;
    }

    public long SeedToLocation(long seed)
    {
        var soil = Map(seedToSoil, seed);
        var fertilizer = Map(soilToFertilizer, soil);
        var water = Map(fertilizerToWater, fertilizer);
        var light = Map(waterToLight, water);
        var temperature = Map(lightToTemperature, light);
        var humidity = Map(temperatureToHumidity, temperature);
        var location = Map(humidityToLocation, humidity);
        return location;
    }

    public IEnumerable<long> Seeds => seeds;
    public IEnumerable<long> RangeSeeds
    {
        get
        {
            for (int i = 0; i < seeds.Length / 2; i++)
                for (long j = 0; j < seeds[2 * i + 1]; j++)
                    yield return seeds[2 * i] + j;
        }
    }

    public IEnumerable<long> RangeSeeds2
    {
        get
        {
            for (int i = 0; i < seeds.Length / 2; i++)
            {
                yield return seeds[2 * i];
                yield return seeds[2 * i] + seeds[2 * i + 1];
            }
        }
    }
}
