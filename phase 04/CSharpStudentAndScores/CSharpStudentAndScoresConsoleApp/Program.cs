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

        private static String ReadFile(String FileName)
        {
            var FileContent = File.ReadAllText(Path + FileName);
            return FileContent;
        }

        private static (String, String) ReadInputFiles()
        {
            return (
                ReadFile("Students.json"),
                ReadFile("Scores.json")
                );
        }


        private static IEnumerable<(string FirstName , string LastName , double Average)> findTop3Students(List<Student> StudentsList, List<ScoreRecord> ScoresList)
        {
            var Result = StudentsList.Join(ScoresList,
                obj => obj.StudentNumber,
                obj => obj.StudentNumber,
                (student, scoreRecord) =>
                new
                {
                    student.StudentNumber,
                    scoreRecord.Score
                }
                ).GroupBy(obj => obj.StudentNumber)
                .Select(g => new
                {
                    StudentNumber = g.Key,
                    Average = g.Average(row => row.Score)
                })
                .OrderByDescending(row => row.Average)
                .Join(StudentsList,
                obj => obj.StudentNumber,
                obj => obj.StudentNumber,
                (studentAverage , student)=>
                (
                    student.FirstName,
                    student.LastName,
                    studentAverage.Average
                ))
                .Take(3);
            return Result;
        }


        static void Main(string[] args)
        {
            (var Students, var Scores) = ReadInputFiles();
            var StudentsList = JsonSerializer.Deserialize<List<Student>>(Students);
            var scoreRecords = JsonSerializer.Deserialize<List<ScoreRecord>>(Scores);

            Console.WriteLine("3 top people in the scores with average values:");
            foreach (var v in findTop3Students(StudentsList , scoreRecords))
            {
                Console.WriteLine(v.FirstName + " " + v.LastName + " ==>> " + v.Average);
            }
        }
    }
}
