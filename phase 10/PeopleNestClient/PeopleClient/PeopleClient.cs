using Nest;

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
                        .Point(t => t
                            .Name(person => person.Location)
                        )
                    )
                );
        }
    }
}