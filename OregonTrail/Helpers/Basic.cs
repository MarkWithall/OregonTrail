using System.Diagnostics;

// NOTE(msw): to match naming conventions of BASIC
// ReSharper disable InconsistentNaming

namespace OregonTrail;

internal static class Basic
{
    public static void PRINT(params object[] objects)
    {
        if (objects.Length == 0)
        {
            Console.WriteLine();
            return;
        }

        var strings = objects.Select(o => o.ToString() ?? string.Empty).ToArray();

        var suppressNewLine = false;
        if (strings.LastOrDefault() == ";")
        {
            suppressNewLine = true;
            strings = strings.Take(strings.Length - 1).ToArray();
        }

        if (strings.Any(s => s == ";"))
        {
            Write(strings.Select(s => s == ";" ? " " : s).ToArray());
        }
        else if (strings.Length > 1)
        {
            Write(strings.Select(s => s.PadRight(15)).ToArray());
        }
        else
        {
            Write(strings.First());
        }

        return;

        void Write(params string[] ss)
        {
            if (suppressNewLine)
            {
                Console.Write(string.Join("", ss));
            }
            else
            {
                Console.WriteLine(string.Join("", ss));
            }
        }
    }

    public static string LIN(int count) =>
        string.Join("", Enumerable.Repeat(Environment.NewLine, count));

    public static Text DIM(int size) => new(size);

    public static void INPUT(Text text)
    {
        Console.Write("?");
        if (Console.ReadLine() is {} value)
        {
            text.SetValue(value);
        }
    }

    public static void INPUT(ref Number n)
    {
        double valNum;
        Console.Write("?");
        while (!double.TryParse(Console.ReadLine(), out valNum))
        {
            Console.WriteLine("BAD INPUT");
            Console.Write("?");
        }
        n = valNum;
    }

    public static void ENTER(ref Number terminal, Number timeLimitInSeconds, ref Number timeTaken, Text text)
    {
        terminal = 0;
        var sw = Stopwatch.StartNew();
        // NOTE(msw): Console.ReadLine will block even if the task times out
        var task = Task.Run(Console.ReadLine);
        text.SetValue(
            task.Wait(TimeSpan.FromSeconds(timeLimitInSeconds))
                ? task.Result ?? string.Empty
                : string.Empty
        );
        sw.Stop();
        timeTaken = sw.Elapsed.TotalSeconds;
    }

    public static int TIM(int field) => DateTime.Now.Minute;

    public static Number RND(int seed) => new Random(seed).NextDouble();

    public static Number INT(Number n) => Math.Floor(n);

    public static string TAB(int size) => new(' ', size);

    public static void RESTORE() => DataPointer = 0;

    public static void READ(ref Number value) => value = DATA[DataPointer++];
}
