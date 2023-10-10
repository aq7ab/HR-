using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Jurisdiction
    {
        public int JuriID { get; set; }
        public string JurName { get; set; }
        public int GroupID { get; set; }
        public string JurAddress { get; set; }
        public int JurPid { get; set; }
    }
}
