using CampusPulse.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampusPulse.Core.Domain
{
    public class Book
    {
        public string Acedamics { get; set; }
        public string Branch { get; set; }
        public int Semester { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Money ActualPrice { get; set; }
        public Money SellingPrice { get; set; }
        public string Discount { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Barcode { get; set; }
        public string Editor { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }

    }
}
