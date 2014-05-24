using System;
using System.Collections.Generic;
namespace TikTak
{
    public interface IBoard
    {
        void Add(int key, string value);
        void Clear();
        bool ContainsKey(int key);
        Dictionary<int, string> Content { get; }
        void Remove(int key);
    }
}
