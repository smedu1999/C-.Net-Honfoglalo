using System;
using System.Collections.Generic;
using System.Text;

namespace Honfoglalo.BLL.Service
{
    public class AnswersInfo
    {
        public int qId { get; set; }
        public int fId { get; set; }
        public int assId { get; set; }
        public int defId { get; set; }
        public string AssaulterAnswer { get; set; }
        public string DefenderAnswer { get; set; }
    }
}
