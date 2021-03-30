using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class WinningHistory                     //Talbalt reprezental, ami eltarolja, hogy egy adott kerdest melyik jatekos nyerte meg
    {
        public int Id { get; set; }                 //Kulcs
        public int QuestionId { get; set; }         //Kerdes kulcs
        public int UserId { get; set; }             //Felhasznalo kulcs
    }
}
