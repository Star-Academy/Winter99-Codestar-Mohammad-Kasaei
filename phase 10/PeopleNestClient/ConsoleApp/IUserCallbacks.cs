using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using PeopleClientLibrary;

namespace ConsoleApp
{
    public interface IUserCallbacks
    {
        string DefaultAppSettingsPath();
        bool Init(string appSettingsPath);
        bool IndexCreation(string indexName, bool forceCreate);
        bool BulkInsertFromFile(string filePath);
        ImmutableList<Person> SearchPeople(IEnumerable<string> args);
        IDictionary<int, long> AgeReport();
        bool Terminate();
    }
}