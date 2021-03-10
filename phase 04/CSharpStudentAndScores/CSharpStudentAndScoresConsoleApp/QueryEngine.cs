using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpStudentAndScoresConsoleApp
{
    public class QueryEngine
    {
        private readonly List<Student> students;
        private readonly List<ScoreRecord> scoreRecords;

        public QueryEngine(List<Student> students, List<ScoreRecord> scoreRecords)
        {
            this.students = students;
            this.scoreRecords = scoreRecords;
        }

        public IEnumerable<StudentAverage> QueryTopStudentsByAverage(int count)
        {
            var top3StudentNumbersWithString = (from scoreRow in scoreRecords
                                                group scoreRow by scoreRow.StudentNumber into gr
                                                select new { StudentNumber = gr.Key, StudentAverage = gr.Average(row => row.Score) });
            var top3StudentsNamesWithAverage = (from row in top3StudentNumbersWithString
                                                join student in students on row.StudentNumber equals student.StudentNumber
                                                orderby row.StudentAverage descending
                                                select new StudentAverage(student.FirstName, student.LastName, row.StudentAverage)).Take(count);
            return top3StudentsNamesWithAverage;
        }

    }
}
