using System;
using System.Collections.Generic;

namespace Server.Hubs
{
    public static class State
    {
        public static IDictionary<string, IList<string>> groups = new Dictionary<string, IList<string>>();

        public static IList<string> AddToState(string user, string group)
        {
            if (groups.ContainsKey(group))
            {
                groups[group].Add(user);

                return groups[group];
            }

            groups.Add(group, new List<string>() { user });

            return groups[group];
        }

        public static void RemoveFromState(string user, string group)
        {
            groups[group].Remove(user);

            return;
        }

        public static IList<string> CurrentState(string group) { return groups[group]; }
    }
}
