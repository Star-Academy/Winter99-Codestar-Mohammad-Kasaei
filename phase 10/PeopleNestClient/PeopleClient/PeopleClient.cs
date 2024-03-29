﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Nest;
using PeopleClientLibrary.validator;

namespace PeopleClientLibrary
{
    public class PeopleClient : ElasticObjectClient<Person>
    {
        public PeopleClient(string server, int port, string indexName, bool debugMode = false)
            : base(server, port, indexName, debugMode)
        {
        }

        protected override CreateIndexDescriptor CreateMapping(CreateIndexDescriptor descriptor)
        {
            return descriptor
                .Map<Person>(m => m
                    .Properties(p => p
                        .Number(n => n
                            .Name(person => person.Age)
                        )
                        .Text(t => t
                            .Name(person => person.EyeColor)
                            .Fields(f => f
                                .Keyword(kw => kw
                                    .Name("keyword")
                                )
                            )
                        )
                        .Text(t => t
                            .Name(person => person.Name)
                        )
                        .Text(t => t
                            .Name(person => person.Gender)
                            .Fields(f => f
                                .Keyword(kw => kw
                                    .Name("keyword")
                                )
                            )
                        )
                        .Text(t => t
                            .Name(person => person.Company)
                            .Fields(f => f
                                .Keyword(kw => kw
                                    .Name("keyword")
                                )
                            )
                        )
                        .Text(t => t
                            .Name(person => person.Email)
                            .Fields(f => f
                                .Keyword(kw => kw
                                    .Name("keyword")
                                )
                            )
                        )
                        .Text(t => t
                            .Name(person => person.Phone)
                        )
                        .Text(t => t
                            .Name(person => person.Address)
                        )
                        .Text(t => t
                            .Name(person => person.About)
                        )
                        .Date(t => t
                            .Name(person => person.RegistrationDate)
                            .Format("yyyy/MM/dd HH:mm:ss")
                        )
                        .GeoPoint(t => t
                            .Name(person => person.Location)
                        )
                    )
                );
        }

        private ImmutableList<Person> PeopleSearchWrapper(Func<SearchDescriptor<Person>, ISearchRequest> searchQuery)
        {
            return Client
                .Search(searchQuery)
                .Validate()
                .Documents
                .ToImmutableList();
        }

        public ImmutableList<Person> SearchAll()
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .MatchAll(d => d)
                )
            );
        }

        public ImmutableList<Person> SearchNameFuzzy(string phrase, int fuzziness = 1)
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .Fuzzy(c => c
                        .Field(p => p.Name)
                        .Value(phrase)
                        .Fuzziness(
                            Fuzziness.EditDistance(fuzziness)
                        )
                    )
                )
            );
        }

        public ImmutableList<Person> SearchEyeColorTerm(string phrase)
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .Term(t => t
                        .Field(p => p.EyeColor)
                        .Value(phrase)
                    )
                )
            );
        }

        public ImmutableList<Person> SearchEyeColorTerms(List<string> phrases)
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .Terms(t => t
                        .Field(p => p.EyeColor)
                        .Terms(phrases)
                    )
                )
            );
        }

        public ImmutableList<Person> SearchAgeRange(int min, int max)
        {
            return PeopleSearchWrapper(
                s => s
                    .Query(q => q
                        .Range(r => r
                            .Field(p => p.Age)
                            .GreaterThanOrEquals(min)
                            .LessThanOrEquals(max)
                        )
                    )
            );
        }

        public ImmutableList<Person> SearchLocationDistance(double lat, double lon, double distanceKm)
        {
            return PeopleSearchWrapper(
                s => s
                    .Query(q => q
                        .GeoDistance(gdq => gdq
                            .Field(p => p.Location)
                            .Distance(distanceKm, DistanceUnit.Kilometers)
                            .Location(lat, lon)
                        )
                    )
            );
        }

        public ImmutableList<Person> SearchFullTexts(string phrase)
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .MultiMatch(m => m
                        .Fields(f => f
                            .Field(p => p.Name)
                            .Field(p => p.About)
                            .Field(p => p.Address)
                        )
                    )
                )
            );
        }

        public ImmutableList<Person> SearchNameAndAge(string name, int age)
        {
            return PeopleSearchWrapper(s => s
                .Query(q => q
                    .Bool(bq => bq
                        .Must(mustQuery => mustQuery
                                .Match(mq => mq
                                    .Field(p => p.Name)
                                    .Query(name)
                                ),
                            mustQuery => mustQuery.Term(t => t
                                .Field(p => p.Age)
                                .Value(age)
                            )
                        )
                    )
                )
            );
        }

        public IDictionary<int, long> AgeReport()
        {
            const string aggregationName = "age_report";
            var result = Client.Search<Person>(s => s
                    .Query(q => q
                        .MatchAll()
                    )
                    .Aggregations(a => a
                        .Terms(aggregationName,
                            descriptor => descriptor
                                .Field(p => p.Age)
                        )
                    )
                )
                .Validate();
            IDictionary<int, long> report = new Dictionary<int, long>();
            foreach (var bucket in result.Aggregations.Terms(aggregationName).Buckets)
            {
                report.Add(int.Parse(bucket.Key), bucket.DocCount ?? 0);
            }

            return report;
        }
    }
}