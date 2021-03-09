using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudentAndScoresConsoleApp
{
    public class CommandLine
    {
        public void DisplayResult(IEnumerable<StudentAverage> filteredStudents)
        {
            Console.WriteLine("3 top people in the scores with average values:");
            foreach (var studentAverage in filteredStudents)
            {
                Console.WriteLine(String.Format("{0} {1} ==>> {2}", studentAverage.FirstName, studentAverage.LastName, studentAverage.Average));
            }
        }
    }
}
