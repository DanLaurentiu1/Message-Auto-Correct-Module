using System;

public class TrieNode
{
    public bool IsWord { get; set; } = false;
    public int Height { get; set; } = 0;
    public Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();
}
