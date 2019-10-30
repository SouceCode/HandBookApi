using System;

namespace HandBookApi.Models
{
    public class Base_Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ReMark { get; set; }
        public DateTime CreateDate { get; set; }
    }
}