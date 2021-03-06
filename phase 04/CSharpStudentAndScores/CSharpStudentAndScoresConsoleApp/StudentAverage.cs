using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpStudentAndScoresConsoleApp
{
    class StudentAverage
    {
        public StudentAverage(string firstName, string lastName, double average)
        {
            FirstName = firstName;
            LastName = lastName;
            Average = average;
        }

        public String FirstName { get; private set; }
        public String LastName { get; private set; }
        public double Average { get; private set; }
    }
}
