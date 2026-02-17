using RadioBrowserWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WpfApp1.Models
{
    public class Group
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("content")]
        public List<Station>? Stations = new List<Station>();

        public void UpdateGroup()
        {
            Thread t = new Thread(() =>
            {
                if (Stations != null)
                    for (int i = 0; i < Stations.Count; i++)
                    {
                        
                    }
                else
                {

                }
            });
            t.Start();
        }
    }
}
