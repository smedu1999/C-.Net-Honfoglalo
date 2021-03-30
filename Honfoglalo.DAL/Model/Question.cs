using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class Question                           //Kerdeseket reprezentalo tabla
    {
        public int Id { get; set; }                 //Kulcs

        public String Question1 { get; set; }       //Kerdes
                                                  
        public WrongAnswers WrongAns {get; set;}
        public String CorrectAnswer { get; set; }   //Jo valasz
        public int Difficulty { get; set; }        //Kerdes nehezsege
        public bool IsDeleted { get; set; }         //Soft Delete

    }
}
