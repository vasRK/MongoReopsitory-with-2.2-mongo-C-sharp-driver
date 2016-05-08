using System;
using System.Collections.Generic;

namespace Repository
{
    public class Cat :IDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IList<Guid> Childrens { get; set; }
        public List<Guid> Hobbies { get; set; }
        public List<Report> Reports { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Report
    {
        public Guid Id { get;set;}

        public int Score { get; set; }
    }
}
