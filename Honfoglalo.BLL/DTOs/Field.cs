using Honfoglalo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.BLL.DTOs
{
    public class Field
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Rank { get; set; }
        public Location FieldLocation { get; set; }
    }
}
