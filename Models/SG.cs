using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 薪酬发放登记表
    /// </summary>
    public class SG
    {
        public int Sgr_id { get; set; }
        public string Salary_grant_id { get; set; }
        public string First_kind_id { get; set; }
        public string First_kind_name { get; set; }
        public string Second_kind_id { get; set; }
        public string Second_kind_name { get; set; }
        public string Third_kind_id { get; set; }
        public string Third_kind_name { get; set; }
        public int Human_amount { get; set; }
        public int Salary_standard_sum { get; set; }
        public int Salary_paid_sum { get; set; }
        public string Register { get; set; }
        public DateTime Regist_time { get; set; }
        public string Checker { get; set; }
        public DateTime Check_time { get; set; }
        
        public int Check_status { get; set; }
    }
}
