using SensitiveWords.Application.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensitiveWords.Application.Interfaces
{
    public interface ISensitiveWordEngine
    {
        SensitiveWordTrie Trie { get; }

        Task ReloadAsync();

        void AddWord(string word);

        void RemoveWord(string word);
    }
}
