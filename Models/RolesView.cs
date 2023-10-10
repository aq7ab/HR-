using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class RolesView
    {
        public int u_id { get; set; }
        public string u_name { get; set;}
        public string u_true_name { get; set;}
        public string u_password { get; set; }
        public string RolesName { get; set; }
        public int UserRolesID { get; set; }
        public int RolesID { get; set; }
        

    }
}
