using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokemonAPI.Deserializers
{
    public class PokemonDeserializer
    {
        public int id { get; set; }
        public Name name { get; set; }
        public List<string> type { get; set; }
        public Base @base { get; set; }
        public string description { get; set; }
        public Evolution evolution { get; set; }
        public Profile profile { get; set; }
    }

    public class Base
    {
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        [JsonProperty("Sp. Attack")]
        public int SpAttack { get; set; }

        [JsonProperty("Sp. Defense")]
        public int SpDefense { get; set; }
        public int Speed { get; set; }
    }

    public class Evolution
    {
        public List<List<string>> next { get; set; }
        public List<string> prev { get; set; }
    }

    public class Name
    {
        public string english { get; set; }
    }

    public class Profile
    {
        public string height { get; set; }
        public string weight { get; set; }
        public List<string> egg { get; set; }
        public List<List<string>> ability { get; set; }
        public string gender { get; set; }
    }
}