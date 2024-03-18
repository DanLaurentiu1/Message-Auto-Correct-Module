using System;
using System.Net.Http.Headers;
using System.Xml.Linq;
public class Trie
{
	private readonly TrieNode root = new TrieNode();

	public TrieNode Root{ get { return root; } }
	public void AddWord(string word)
	{
		// start at the root
		var node = root;

		// for each character, if the node (in this case the root) does not contain the character, we add it
		// then we just go next
		foreach (char c in word)
		{
			if (!node.Children.ContainsKey(c))
			{
                node.Children[c] = new TrieNode();
				node.Children[c].Height = node.Height + 1;
			}

			node = node.Children[c];
		}

		node.IsWord = true;
	}

	// Just for information about the nodes, the display is done with dfs
	public void DisplayAllTheNodesDFS(TrieNode node)
	{
		if (node.Children == null)
			return;

		foreach(var n in node.Children)
		{
			Console.WriteLine("Key is " + n.Key);
            Console.WriteLine("Height is " + n.Value.Height);
            Console.WriteLine("IsWord is " + n.Value.IsWord);
            DisplayAllTheNodesDFS(n.Value);
        }
	}

	public List<string> Search(string word)
	{
		// we go from the root
        var node = Root;

		//prepare the result list
		var result = new List<string>();

		if (word == "")
			return result;

        string res = "";

		// for every character in the word
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
			{
				// if at least 70% of the word that we are searching for is in the trie
				// this means that the word is probably misspelled
				if (node.Height >= ((word.Length * 0.7) - 1))
				{
					int remaining = word.Length - node.Height;
					result = Correction(res, node, remaining);
					if (result.Count == 0)
					{
						Console.WriteLine("There isn't any suitable correction for this word in the data-base");
					}
					return result;
				}
				else
				{
					result.Add("Cannot provide a correction, too many possibilities");
					return result;
				}
			}

			//prefix of all the characters found until now
            res += c;
            node = node.Children[c];
        }
		result.Add("Cannot provide a correction, the word is already correct");
        return result;

    }
	
	
    private static List<string> Correction(string prefix, TrieNode node, int remaining)
	{
		var result = new List<string>();
		while (remaining != 0)
		{
			string tempResult = prefix;

			foreach(var (key, value) in node.Children)
			{
				if (value.IsWord == true)
				{
					string add = prefix + key;
					var cond = true;
					foreach (var item in result)
					{
						if (item == add)
						{
							cond = false;
							break;
						}
					}
					if (cond == true)
					{
                        result.Add(prefix + key);
                    }
				}
				else
				{
					var tempRes = new List<string>();
					var tempRemaining = remaining - 1;
					tempRes = Correction(tempResult + key, value, tempRemaining);

					if (tempRes.Count != 0)
					{
						foreach (var item in tempRes)
						{
							var cond = true;
							foreach (var item2 in result)
							{
								if (item == item2)
								{
									cond = false;
									break;
								}
							}
							if (cond == true)
								result.Add(item);
						}
					}
				}
            }
            remaining -= 1;
        }
        return result;
    }
	
}
