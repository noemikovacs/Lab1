using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Models
{
    public class StudentItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Year { get; set; }
        public bool IsWithFreq{ get; set; }
    }
}