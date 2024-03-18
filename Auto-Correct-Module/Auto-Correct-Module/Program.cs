using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Diagnostics;
internal partial class AutoCorrect
{
    private static void Main(string[] args)
    {
        // string originalFilePath = "C:\\Users\\Lau\\Documents\\GitHub\\Data-Auto-Correct-Module\\Auto-Correct-Module\\Auto-Correct-Module\\word.txt";
        string newFilePath = "C:\\Users\\Lau\\Documents\\GitHub\\Data-Auto-Correct-Module\\Auto-Correct-Module\\Auto-Correct-Module\\resources.txt";
        // FormatFile(originalFilePath, newFilePath);

        var trie = new Trie();

        var words = ReadFromFile(newFilePath);

        foreach (var word in words)
        {
            trie.AddWord(word);
        }

        var result1 = trie.Search("uninteresxxxx");
        Print(result1);

        var result2 = trie.Search("car!8");
        Print(result2);

        var result3 = trie.Search("");
        Print(result3);

        var result4 = trie.Search("correcaaa");
        Print(result4);

        var result5 = trie.Search("uninters");
        Print(result5);

    }

    private static void Print(List<string> result)
    {
        foreach(var r in result)
        {
            Console.WriteLine(r);
        }
    }
    private static void FormatFile(string originalFilePath, string newFilePath)
    {

        StreamWriter fileWriter = new StreamWriter(newFilePath);
        StreamReader fileReader = new StreamReader(originalFilePath);

        string? line = fileReader.ReadLine();

        while (line != null)
        {
            bool cond = false;
            foreach (char c in line)
            {
                if (!Char.IsLetter(c))
                {
                    cond = true;
                    break;
                }
            }
            if (cond == false)
            {
                fileWriter.WriteLine(line);
            }
            else
            {
                cond = false;
            }
            line = fileReader.ReadLine();
        }

        fileWriter.Close();
        fileReader.Close();
    }

    private static List<string> ReadFromFile(string filePath)
    {
        var words = new List<string>();
        var reader = new StreamReader(filePath);

        string? line = reader.ReadLine();

        while(line != null)
        {
            words.Add(line);
            line = reader.ReadLine();
        }

        reader.Close();

        return words;
    }
}