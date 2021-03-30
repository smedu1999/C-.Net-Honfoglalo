using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.DAL.Model
{
    public class FieldOwner                     //Tabla, amelyben eltaroljuk, hogy egy adott mezo kie
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public String FieldName { get; set; }
    }
}
