using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class QuestionFieldLevel                //Kerdes-Mezo Tablak osszekapcsolasa nehezseg alapjan
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }     //Alternate Key hasznalata
        public int FieldId { get; set; }
    }
}
