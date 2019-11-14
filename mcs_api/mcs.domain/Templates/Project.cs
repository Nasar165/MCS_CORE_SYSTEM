using System;

namespace mcs.domain.Templates
{
    public sealed class Project : Property
    {
        public string Project_Id { get; set; }
        public string Name { get; set; }
        public int Buildings { get; set; }
        public int Units { get; set; }
        public int Status_Id { get; set; }
        public DateTime Completion_Date { get; set; }
    }
}