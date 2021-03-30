using Honfoglalo.DAL.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.BLL.Service
{
    public class AttackInfo
    {
        public Users Attacker { get; set; }
        public Field Field { get; set; }
    }
}
