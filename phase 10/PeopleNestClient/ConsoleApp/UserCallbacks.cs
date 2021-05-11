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

        public ImmutableList<Person> SearchPeople(IEnumerable<string> arguments)
        {
            var searchInstructions = new Dictionary<string, Func<IEnumerable<string>, ImmutableList<Person>>>
            {
                {"all", (args) => _client.SearchAll()},
                {
                    "name_fuzzy", (args) =>
                    {
                        var phrase = args.First();
                        var fuzziness = int.Parse(args.First());
                        return _client.SearchNameFuzzy(phrase, fuzziness);
                    }
                },
                {
                    "eye_color_term", (args) =>
                    {
                        var phrase = args.First();
                        return _client.SearchEyeColorTerm(phrase);
                    }
                },
                {
                    "eye_color_terms", (args) =>
                    {
                        var phrase = args.ToList();
                        return _client.SearchEyeColorTerms(phrase);
                    }
                },
                {
                    "age_range", (args) =>
                    {
                        var min = int.Parse(args.First());
                        var max = int.Parse(args.First());
                        return _client.SearchAgeRange(min, max);
                    }
                },
                {
                    "distance", (args) =>
                    {
                        var lat = int.Parse(args.First());
                        var lon = int.Parse(args.First());
                        var distance = int.Parse(args.First());
                        return _client.SearchLocationDistance(lat, lon, distance);
                    }
                },
                {
                    "full_text", (args) =>
                    {
                        var phrase = args.First();
                        return _client.SearchFullTexts(phrase);
                    }
                },
                {
                    "name_age", (args) =>
                    {
                        var name = args.First();
                        var age = int.Parse(args.First());
                        return _client.SearchNameAndAge(name, age);
                    }
                }
            };

            return searchInstructions[arguments.First()]?.Invoke(arguments);
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