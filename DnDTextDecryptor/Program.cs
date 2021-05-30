using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DnDTextDecryptor.Test")]

namespace DnDTextDecryptor
{
    internal class Program
    {
        internal static List<char> vokaler = new List<char>() { 'A', 'O', 'U', 'Å', 'E', 'I', 'Y', 'Ä', 'Ö' };

        static void Main(string[] args)
        {
            void WriteResult(string inTestName, int i, string result)
            {
                Console.WriteLine($"Result of {inTestName} with offset: {i}!");
                Console.WriteLine();
                Console.WriteLine(result);
                _ = Console.ReadKey();
                Console.WriteLine();
                Console.WriteLine();
            }

            for (int i = 0; i < 28; i++)
            {
                var result = SimpleOffset(Resources.OriginalText.ToUpper(), i);
                WriteResult("SimpleOffset", i, result);
            }
            for (int i = 0; i < 28; i++)
            {
                var result = IncreaseOffsetWithEachNewChar(Resources.OriginalText.ToUpper(), i);
                WriteResult("IncreaseOffsetWithEachNewChar", i, result);
            }
            for (int i = 0; i < 28; i++)
            {
                var result = IncreaseOffsetWithEachNewVOKAL(Resources.OriginalText.ToUpper(), i);
                WriteResult("IncreaseOffsetWithEachNewVOKAL", i, result);
            }
            for (int i = 0; i < 28; i++)
            {
                var result = IncreaseOffsetWithEachNewWord(Resources.OriginalText.ToUpper(), i);
                WriteResult("IncreaseOffsetWithEachNewWord", i, result);
            }
        }

        internal static string SimpleOffset(string inText, int inOffset)
        {
            using (StringWriter writer = new StringWriter())
            {
                foreach (char c in inText)
                {
                    if (c == '\\' || c == 'n' || c == 'r' || c == '.' || c == ',' || c == ':' || c == ' ')
                        writer.Write(c);
                    else
                        writer.Write(OffsetChar(c, inOffset));
                }
                return writer.ToString();
            }
        }

        internal static string IncreaseOffsetWithEachNewChar(string inText, int inInitialOffset)
        {
            var offset = inInitialOffset;
            using (StringWriter writer = new StringWriter())
            {
                foreach (char c in inText)
                {
                    if (c == '\\' || c == 'n' || c == 'r' || c == '.' || c == ',' || c == ':' || c == ' ')
                        writer.Write(c);
                    else
                        writer.Write(OffsetChar(c, offset++));
                }
                return writer.ToString();
            }
        }

        internal static string IncreaseOffsetWithEachNewVOKAL(string inText, int inInitialOffset)
        {
            var offset = inInitialOffset;
            using (StringWriter writer = new StringWriter())
            {
                foreach (char c in inText)
                {
                    if (c == '\\' || c == 'n' || c == 'r' || c == '.' || c == ',' || c == ':' || c == ' ')
                        writer.Write(c);
                    else
                    {
                        if (vokaler.Contains(c))
                        {
                            offset++;
                        }
                        writer.Write(OffsetChar(c, offset));
                    }
                }
                return writer.ToString();
            }
        }

        internal static string IncreaseOffsetWithEachNewWord(string inText, int inInitialOffset)
        {
            inText = inText.Replace("  ", " ");
            var offset = inInitialOffset;
            using StringWriter writer = new StringWriter();
            foreach (char c in inText)
            {
                if (c == '\\' || c == 'n' || c == 'r' || c == '.' || c == ',' || c == ':')
                    writer.Write(c);
                else
                {
                    if (c == ' ')
                    {
                        offset++;
                        writer.Write(c);
                        continue;
                    }
                    writer.Write(OffsetChar(c, offset));
                }
            }
            return writer.ToString();
        }

        internal static char OffsetChar(char inChar, int inOffset)
        {
            return IntToChar(CharToInt(inChar) + inOffset);
        }


        internal static int CharToInt(char inChar)
        {
            //urg swedish
            switch (inChar)
            {
                case 'Å':
                    return 26;
                case 'Ä':
                    return 27;
                case 'Ö':
                    return 28;
                default:
                    return (int)inChar - 65;
            }
        }

        internal static char IntToChar(int inChar)
        {
            inChar %= 29;

            switch (inChar)
            {
                case 26:
                    return 'Å';
                case 27:
                    return 'Ä';
                case 28:
                    return 'Ö';
                default:
                    return (char)(inChar + 65);
            }
        }
    }
}
