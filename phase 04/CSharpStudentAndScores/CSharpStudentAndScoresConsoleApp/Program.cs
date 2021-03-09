using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace CSharpStudentAndScoresConsoleApp
{
    class Program
    {
        private static readonly string path = "C:\\Mohammad\\work\\Mohaimen\\CodeStar\\codestar-winter\\Winter99-Codestar-Mohammad-Kasaei\\phase 04\\CSharpStudentAndScores\\";

        private static String ReadDataFile(String fileName)
        {
            return File.ReadAllText(path + fileName);
        }

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

        private static void PrintResults(IEnumerable<StudentAverage> filteredStudents)
        {
            Console.WriteLine("3 top people in the scores with average values:");
            foreach (var studentAverage in filteredStudents)
            {
                Console.WriteLine(String.Format("{0} {1} ==>> {2}", studentAverage.FirstName, studentAverage.LastName, studentAverage.Average));
            }
        }


        static void Main(string[] args)
        {
            var students = ReadDataFile("Students.json");
            var scores = ReadDataFile("Scores.json");
            var studentsList = JsonSerializer.Deserialize<List<Student>>(students);
            var scoreRecords = JsonSerializer.Deserialize<List<ScoreRecord>>(scores);
            PrintResults(FindTop3Students(studentsList, scoreRecords));
        }
    }
}
