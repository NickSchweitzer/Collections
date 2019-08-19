# Command Line Arguments Parser

This is a very basic class for turning the command line arguments passed into the `Main()` function into a Dictionary. The default parameters for the command line parser are case insensitive. To make the parser treat the keys in a case senstive manner, set the appropriate value during Construction.

# Sample

```csharp
static void Main(string[] args)
{
    CmdArguments parsedArgs = new CmdArguments(args);

    if (parsedArgs.ContainsKey("boolswitch"))
        Console.WriteLine("boolswitch = true");
    else
        Console.WriteLine("boolswitch = false");

    Console.WriteLine($"spaceswitch = {parsedArgs["spaceSwitch"]}");
    Console.WriteLine($"equalswitch = {parsedArgs["equalSwitch"]}");
    Console.WriteLine($"colonswitch = {parsedArgs["colonSwitch"]}");
}
```

```shell
> program.exe -boolswitch /spaceswitch spacevalue -equalswitch=equalvalue --colonswitch:colonvalue
boolswitch = true
spaceswitch = spacevalue
equalswitch = equalvalue
colonswitch = colonvalue
```