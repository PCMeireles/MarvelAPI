using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarvelAPI.Models
{
    public class HeroiDetalhePersistent
    {      
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string modified { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnails { get; set; }

        public string resourceURI { get; set; }

        [JsonProperty("comics")]
        public Comic Comics { get; set; }

        [JsonProperty("series")]
        public Serie Series { get; set; }

        [JsonProperty("stories")]
        public Storie Stories { get; set; }

        [JsonProperty("events")]
        public Event Events { get; set; }

        [JsonProperty("urls")]
        public List<Url> Urls { get; set; }

    }

    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }

    }

    public class Comic
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }


    }
   
    public class Serie
    {
        [JsonProperty("available")]
        public string Available { get; set; }
        
        [JsonProperty("collectionURI")]
        public string CollectionURI { get; set; }
        
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class Storie
    {
        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public string Returned { get; set; }
    }

    public class Event
    {
        [JsonProperty("available")]
        public string Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("returned")]
        public string Returned { get; set; }
    }

    public class Url
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Urltype { get; set; }
    }

    public class Item
    {
        [JsonProperty("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
    
}