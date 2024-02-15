using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    internal class Elements
    {
        //создать класс элемент
        //вынеси часть логикик из view в модель
        private readonly string[] elementsName =
        { 
            "air",
            "algae",
            "bacteria",
            "dust",
            "earth",
            "energy",
            "fire",
            "isle",
            "lake",
            "lava",
            "life",
            "mud",
            "pressure",
            "sand",
            "steam",
            "stone",
            "swamp",
            "volcano",
            "water",
            "wind",
        };

        private List<string> openedElements = new List<string>
        {
            "air",
            "earth",
            "fire",
            "water"
        };


        private readonly Dictionary<KeyValuePair<string, string>, string[]> connections = new Dictionary<KeyValuePair<string, string>, string[]>
        {
            {new KeyValuePair<string, string>( "life", "water" ), new string[]{ "algae" } },
            {new KeyValuePair<string, string>( "life", "swamp" ), new string[]{ "bacteria" } },
            {new KeyValuePair<string, string>( "earth", "wind" ), new string[]{ "dust" } },
            {new KeyValuePair<string, string>( "air", "fire" ), new string[]{ "energy" } },
            {new KeyValuePair<string, string>( "earth", "lake" ), new string[]{ "isle" } },
            {new KeyValuePair<string, string>( "water", "water" ), new string[]{ "lake" } },
            {new KeyValuePair<string, string>( "earth", "fire" ), new string[]{ "lava" } },
            {new KeyValuePair<string, string>( "energy", "swamp" ), new string[]{ "life" } },
            {new KeyValuePair<string, string>( "dust", "water" ), new string[]{ "mud" } },
            {new KeyValuePair<string, string>( "earth", "earth" ), new string[]{ "pressure" } },
            {new KeyValuePair<string, string>( "stone", "wind" ), new string[]{ "sand" } },
            {new KeyValuePair<string, string>( "lava", "water" ), new string[]{ "steam" } },
            {new KeyValuePair<string, string>( "earth", "pressure" ), new string[]{ "stone" } },
            {new KeyValuePair<string, string>( "earth", "water" ), new string[]{ "swamp" } },
            {new KeyValuePair<string, string>( "lava", "pressure" ), new string[]{ "volcano" } },
            {new KeyValuePair<string, string>( "air", "air" ), new string[]{ "wind" } },
        };

        public Elements() 
        {
            Array.Sort(elementsName);
        }

        public int GetElementsCount()
        {
            return elementsName.Length;
        }

        public bool CheckСonnection(KeyValuePair<string, string> connection)
        {
            return connections.ContainsKey(connection) 
                || connections.ContainsKey(new KeyValuePair<string, string>(connection.Value, connection.Key));
        }

        private KeyValuePair<string, string> GetSortedKeyValuePair(KeyValuePair<string, string> connection)
        {
            if(connection.Key.CompareTo(connection.Value) < 0)
            {
                return connection;
            }
            else
            {
                return new KeyValuePair<string, string>(connection.Value, connection.Key);

            }
        }

        private string[] OpenedElementsAddElements(string[] elements)
        {

            List<string> newOpenedElements = new List<string>();

            foreach (var element in elements)
            {
                if (!openedElements.Contains(element))
                {
                    newOpenedElements.Add(element);
                    openedElements.Add(element);
                }
            }
            return newOpenedElements.ToArray();
        }

        public string[] TryCraetingElementsByСonnection(KeyValuePair<string, string> connection)
        {
            if (!CheckСonnection(connection)) 
                return new string[0];

            return OpenedElementsAddElements(connections[GetSortedKeyValuePair(connection)]);
        }

        public string[] GetOpenedElements()
        {
            return openedElements.ToArray();
        }

    }
}
