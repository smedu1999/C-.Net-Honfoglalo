using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class Field                           //Mezoket reprezentalo tabla
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Rank { get; set; }
        public Location FieldLocation { get; set; }
    }

    [Owned]
    public class Location
    {
        public Countries Country { get; set; }
        public String Region { get; set; }
    }
    public enum Countries { Hungary, Germany, Italy, Spain, Russia  }
}
