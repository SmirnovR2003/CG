using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _4.Model
{
    struct Item
    {
        public string Hint { get; set; }
        public string Word { get; set; }
    }

    internal class FileReader
    {
        private string _fileName;
        private Item _item;

        public FileReader(string fileName) 
        {
            _fileName = fileName;
        }

        internal string GetHint()
        {
            return _item.Hint;
        }

        internal string GetWord()
        {
            return _item.Word;
        }

        internal void ReadFromFile()
        {
            using (StreamReader r = new StreamReader(_fileName))
            {
                string json = r.ReadToEnd();
                Item[] items = JsonSerializer.Deserialize<Item[]>(json);
                Random rand = new Random();
                _item = items[rand.Next(0, items.Length)];
            }

        }
    }
}
