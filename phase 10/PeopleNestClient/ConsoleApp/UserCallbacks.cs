using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PeopleClientLibrary;

namespace ConsoleApp
{
    public class UserCallbacks : IUserCallbacks
    {
        private AppSettings _appSettings;
        private PeopleClient _client;


        public string DefaultAppSettingsPath()
        {
            return "../../../AppSettings.json";
        }

        public bool Init(string appSettingsPath)
        {
            try
            {
                _appSettings = AppSettings.CreateFromFile(appSettingsPath);
                return _appSettings != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IndexCreation(string indexName, bool forceCreate)
        {
            try
            {
                _client = new PeopleClient(_appSettings.Server, _appSettings.Port, indexName, true);
                return forceCreate ? _client.CreateIndex() : _client.CheckConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool BulkInsertFromFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                var people = JsonConvert.DeserializeObject<List<Person>>(content);
                return _client.Bulk(people);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ImmutableList<Person> SearchPeople(string[] args)
        {
            string NextArg(ref int i)
            {
                return args[i++];
            }

            var argPointer = 0;
            switch (args[argPointer++])
            {
                case "all":
                    return _client.SearchAll();
                case "name_fuzzy":
                {
                    var phrase = NextArg(ref argPointer);
                    var fuzziness = int.Parse(NextArg(ref argPointer));
                    return _client.SearchNameFuzzy(phrase, fuzziness);
                }
                case "eye_color_term":
                {
                    var phrase = NextArg(ref argPointer);
                    return _client.SearchEyeColorTerm(phrase);
                }
                case "eye_color_terms":
                {
                    var phrase = args.Skip(argPointer).ToList();
                    return _client.SearchEyeColorTerms(phrase);
                }
                case "age_range":
                {
                    var min = int.Parse(NextArg(ref argPointer));
                    var max = int.Parse(NextArg(ref argPointer));
                    return _client.SearchAgeRange(min, max);
                }
                case "distance":
                {
                    var lat = int.Parse(NextArg(ref argPointer));
                    var lon = int.Parse(NextArg(ref argPointer));
                    var distance = int.Parse(NextArg(ref argPointer));
                    return _client.SearchLocationDistance(lat, lon, distance);
                }
                case "full_text":
                {
                    var phrase = NextArg(ref argPointer);
                    return _client.SearchFullTexts(phrase);
                }
                case "name_age":
                {
                    var name = NextArg(ref argPointer);
                    var age = int.Parse(NextArg(ref argPointer));
                    return _client.SearchNameAndAge(name, age);
                }
            }

            return null;
        }

        public IDictionary<int, long> AgeReport()
        {
            return _client.AgeReport();
        }

        public bool Terminate()
        {
            /*
             * Nothing to do in terminate phase
             * User can always exit the program  :)
             */
            return true;
        }
    }
}