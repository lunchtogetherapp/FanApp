using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneApp1
{
    public class PuzzleWord
    {
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public string Type { get; private set; }
        public string Hint { get; private set; }

        public PuzzleWord(string question, string answer, string type, string hint)
        {
            this.Question = question;
            this.Answer = answer;
            this.Type = type;
            this.Hint = hint;
        }
    }

    public class GrouppedList<T> : List<T>
    {
        public string Key { get; private set; }
        public delegate string GetKey(T item);

        public GrouppedList(string key)
        {
            this.Key = key;
        }

        public static List<GrouppedList<T>> CreateGroups(IEnumerable<T> items, GetKey getKey)
        {
            List<GrouppedList<T>> result = new List<GrouppedList<T>>();
            foreach (T item in items)
            {
                string key = getKey(item);
                if (result.Any(group => group.Key.Equals(key)))
                {
                    GrouppedList<T> group = result.First(g => g.Key.Equals(key));
                    group.Add(item);
                }
                else
                {
                    GrouppedList<T> group = new GrouppedList<T>(key);
                    group.Add(item);
                    result.Add(group);
                }
            }

            return result;
        }
    }
}
