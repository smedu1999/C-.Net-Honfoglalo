using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class WrongAnswers
    {
        public int Id { get; set; }                 //Kulcs
        public String Question1 { get; set; }       //Közös oszlop
        public String Answer1 { get; set; }         //..
        public String Answer2 { get; set; }         //Rossz valaszok
        public String Answer3 { get; set; }         //..
    }
}
