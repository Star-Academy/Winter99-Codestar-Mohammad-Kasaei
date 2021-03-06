using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace CSharpStudentAndScoresConsoleApp
{
    class Program
    {
        private static readonly string Path = "C:\\Mohammad\\work\\Mohaimen\\CodeStar\\codestar-winter\\Winter99-Codestar-Mohammad-Kasaei\\phase 04\\CSharpStudentAndScores\\";

        private static String ReadDataFile(String fileName)
        {
            return File.ReadAllText(Path + fileName);
        }

        private static (String, String) ReadInputFiles()
        {
            return (
                ReadDataFile("Students.json"),
                ReadDataFile("Scores.json")
                );
        }


        private static IEnumerable<(string FirstName , string LastName , double Average)> FindTop3Students(List<Student> studentsList, List<ScoreRecord> scoresList)
        {
            var result = studentsList.Join(scoresList,
                obj => obj.StudentNumber,
                obj => obj.StudentNumber,
                (student, scoreRecord) =>
                new
                {
                    student.StudentNumber,
                    scoreRecord.Score
                }
                ).GroupBy(obj => obj.StudentNumber)
                .Select(group => new
                {
                    StudentNumber = group.Key,
                    Average = group.Average(row => row.Score)
                })
                .OrderByDescending(row => row.Average)
                .Join(studentsList,
                obj => obj.StudentNumber,
                obj => obj.StudentNumber,
                (studentAverage , student)=>
                (
                    student.FirstName,
                    student.LastName,
                    studentAverage.Average
                ))
                .Take(3);
            return result;
        }

        private static void PrintResults(IEnumerable<(string FirstName, string LastName, double Average)> filteredStudents)
        {
            Console.WriteLine("3 top people in the scores with average values:");
            foreach (var (FirstName, LastName, Average) in filteredStudents)
            {
                Console.WriteLine(FirstName + " " + LastName + " ==>> " + Average);
            }
        }


        static void Main(string[] args)
        {
            (var Students, var Scores) = ReadInputFiles();
            var studentsList = JsonSerializer.Deserialize<List<Student>>(Students);
            var scoreRecords = JsonSerializer.Deserialize<List<ScoreRecord>>(Scores);
            PrintResults(FindTop3Students(studentsList, scoreRecords));
        }
    }
}
