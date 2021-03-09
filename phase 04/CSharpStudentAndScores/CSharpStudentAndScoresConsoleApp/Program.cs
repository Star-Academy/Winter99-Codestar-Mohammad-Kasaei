using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace CSharpStudentAndScoresConsoleApp
{
    class Program
    {
        
        private static IEnumerable<StudentAverage> FindTop3Students(List<Student> studentsList, List<ScoreRecord> scoresList)
        {
            var top3StudentNumbersWithString = (from scoreRow in scoresList
                                                     group scoreRow by scoreRow.StudentNumber into gr
                                                     select new { StudentNumber = gr.Key, StudentAverage = gr.Average(row => row.Score) });
            var top3StudentsNamesWithAverage = (from row in top3StudentNumbersWithString
                                                join student in studentsList on row.StudentNumber equals student.StudentNumber
                                                orderby row.StudentAverage descending
                                                select new StudentAverage(student.FirstName , student.LastName , row.StudentAverage)).Take(3);
            return top3StudentsNamesWithAverage;
        }

        static void Main(string[] args)
        {
            var cmd = new CommandLine();
            var reader = new FileReader(@"..\..\..\..\");
            var students = reader.readTextFile("Students.json");
            var scores = reader.readTextFile("Scores.json");
            var studentsList = JsonSerializer.Deserialize<List<Student>>(students);
            var scoreRecords = JsonSerializer.Deserialize<List<ScoreRecord>>(scores);
            cmd.DisplayResult(FindTop3Students(studentsList, scoreRecords));
        }
    }
}
