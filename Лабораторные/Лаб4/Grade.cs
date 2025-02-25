using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Program;

namespace Grabe
{
    public enum Subject
    {
        Math,
        Physics,
        Chemistry,
        Biology,
        History
    }
    public struct Grade
    {
        public Subject Subject { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }

        public Grade(Subject subject, int score, DateTime date)
        {
            Subject = subject;
            Score = score;
            Date = date;
        }
    }
}