﻿using Nest;
using Newtonsoft.Json;

namespace PeopleClientLibrary
{
    public class Person
    {
        [JsonProperty("age")] public int Age { get; set; }

        [JsonProperty("eyeColor")] public string EyeColor { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("gender")] public string Gender { get; set; }

        [JsonProperty("company")] public string Company { get; set; }

        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("about")] public string About { get; set; }

        [JsonProperty("registration_date")] public string RegistrationDate { get; set; }

        [Ignore] [JsonProperty("latitude")] public double Latitude { get; set; }

        [Ignore] [JsonProperty("longitude")] public double Longitude { get; set; }

        public string Location
        {
            get => $"{Latitude},{Longitude}";
            set
            {
                var values = value.Split(",");
                Latitude = double.Parse(values[0]);
                Longitude = double.Parse(values[1]);
            }
        }
    }
}