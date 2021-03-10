using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace CSharpStudentAndScoresConsoleApp
{
    class Program
    {
        private static readonly string dataPath = @"..\..\..\..\";
        private static readonly string studentsFileName = @"Students.json";
        private static readonly string scoresFileName = @"Scores.json";

        private static readonly FileReader reader = new FileReader(dataPath);

        private static List<Student> students;
        private static List<ScoreRecord> scoreRecords;

        private static void LoadData()
        {
            var studentsText = reader.readTextFile(studentsFileName);
            var scoresText = reader.readTextFile(scoresFileName);
            students = JsonSerializer.Deserialize<List<Student>>(studentsText);
            scoreRecords = JsonSerializer.Deserialize<List<ScoreRecord>>(scoresText);
        }

        static void Main(string[] args)
        {
            var cmd = new CommandLine();
            LoadData();
            var queryEngine = new QueryEngine(students, scoreRecords);
            cmd.DisplayResult(queryEngine.QueryTopStudentsByAverage(3));
        }
    }
}
