using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KWGene.Models
{
    public class zNodes
    {
        public int id { get; set; }
        public int pId { get; set; }
        public string name { get; set; }
        public bool open { get; set; }
        public bool @checked { get; set; }

        public bool chkDisabled { get; set; }
    }
}