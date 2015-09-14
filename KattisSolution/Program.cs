using System;
using System.Configuration;
using System.IO;
using System.Text;
using KattisSolution.IO;

namespace KattisSolution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }

        public static void Solve(Stream stdin, Stream stdout)
        {
            IScanner scanner = new OptimizedPositiveIntReader(stdin);
            // uncomment when you need more advanced reader
            // scanner = new Scanner(stdin);
            scanner = new LineReader(stdin);
            var writer = new BufferedStdoutWriter(stdout);

            var testCases = scanner.NextInt();

            for (int i = 0; i < testCases; i++)
            {
                var line = scanner.Next();
                var result = 0;
                int stringNotCovered = line.Length;

                for (int j = 1; j < line.Length; j++)
                {
                    if (line.Substring(0, j).StartsWith(line.Substring(j, Math.Min(line.Length - j, j))))
                    {
                        //                        if (result == 0)
                        //                        {
                        //                            result = j;
                        //                            break;
                        //                        }
                        if (result == 0)
                        {
                            result = j;

                            int k = j;
                            while (k + result < line.Length && line.Substring(k, result) == line.Substring(0, j))
                            {
                                k += result;
                            }
                            stringNotCovered = line.Length - k - j;
                        }
                        else if (result != 0 && j % result == 0)
                        {
                            // ignore - we already handled it
                        }
                        else
                        {
                            // we already have a solution, just check if it covers more ground
                            // this one doesn't repeat itself
                            if (stringNotCovered > 0 && 2 * j > line.Length - stringNotCovered)
                            {
                                result = j;
                                stringNotCovered = 0;
                                break;
                            }
                        }
                    }
                }

                writer.Write(result);
                writer.Write("\n");
            }

            writer.Flush();
        }
    }
}