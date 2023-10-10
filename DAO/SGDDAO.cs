using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    
    public class SGDDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fill>> SGDSelectAsync(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"SELECT [first_kind_id],[first_kind_name],[second_kind_id],[second_kind_name],[third_kind_id],[third_kind_name], sum([demand_salaray_sum]) as [demand_salaray_sum],sum(salary_sum) as salary_sum,sum([paid_salary_sum]) as [paid_salary_sum], sum(case when {str} then 1 else 0 end) as Number FROM [dbo].[human_file] where {str} GROUP BY [first_kind_name],[second_kind_name],[third_kind_name],[first_kind_id],[second_kind_id],[third_kind_id];";
                return (List<Fill>)await connection.QueryAsync<Fill>(sql);
            }
        }

        /// <summary>
        /// 根据ID查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fill>> SGDSelectIDAsync(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[human_file] where {str}";
                return (List<Fill>)await connection.QueryAsync<Fill>(sql);
            }
        }
        /// <summary>
        /// 根据薪酬单编号查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fill>> SGDSelectBHAsync(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[salary_grant_details] l inner join [dbo].[human_file] h on l.human_id=h.human_id where {str}";
                return (List<Fill>)await connection.QueryAsync<Fill>(sql);
            }
        }
        /// <summary>
        /// 根据薪酬单编号查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<Fill>> SGDSelectCXAsync(string str)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[salary_grant_details] l inner join [dbo].[human_file] h on l.human_id=h.human_id where salary_grant_id='{str}'";
                return (List<Fill>)await connection.QueryAsync<Fill>(sql);
            }
        }
        /// <summary>
        /// 查询复核分页
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public async Task<FenYe<SG>> SGSelectAsync(FenYe<SG> fc, string tj)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("@pageSize", fc.PageSize);
                dd.Add("@keyName", "sgr_id");
                dd.Add("@tableName", "salary_grant");
                dd.Add("@currentPage", fc.CurrentPage);
                dd.Add("where", tj);
                dd.Add("@row", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[FenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @row out";
                fc.List = await con.QueryAsync<SG>(sql, dd);
                fc.Rows = dd.Get<int>("row");
                return fc;
            }
        }
        /// <summary>
        /// SGD的编号
        /// </summary>
        /// <returns></returns>
        public async Task<string> BhAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $@"SELECT RIGHT(CONCAT('000000',FLOOR(RAND()* 1000000)),6)";
                string sj = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                string bh = "HS" + sj + await connection.QueryFirstAsync<string>(sql);
                return bh;
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<int> InsertAsync( SG sg,List<SGD> gg)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $@"insert into [dbo].[salary_grant](salary_grant_id, first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, human_amount, salary_standard_sum, salary_paid_sum, register, regist_time)values('{sg.Salary_grant_id}','{sg.First_kind_id}','{sg.First_kind_name}','{sg.Second_kind_id}','{sg.Second_kind_name}','{sg.Third_kind_id}','{sg.Third_kind_name}','{sg.Human_amount}','{sg.Salary_standard_sum}','{sg.Salary_paid_sum}','{sg.Register}','{sg.Regist_time}')";
                int res = await connection.ExecuteAsync(sql);
                if (res > 0)
                {
                    for (int i = 0; i < gg.Count; i++)
                    {
                        string ss = $@"insert into [dbo].[salary_grant_details](salary_grant_id, human_id, human_name, bouns_sum, sale_sum, deduct_sum, salary_standard_sum, salary_paid_sum)values('{gg[i].Salary_grant_id}','{gg[i].Human_id}','{gg[i].Human_name}','{gg[i].Bouns_sum}','{gg[i].Sale_sum}','{gg[i].Deduct_sum}','{gg[i].Salary_standard_sum}','{gg[i].Salary_paid_sum}')";
                        await connection.ExecuteAsync(ss);
                        string dd = $@"update [dbo].[human_file] set [demand_salaray_sum]='{gg[i].Salary_paid_sum}',zt='1' where [human_id]='{gg[i].Human_id}'";
                        await connection.ExecuteAsync(dd);
                    }
                }
                return 1;


            }
          
            }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sg"></param>
        /// <param name="gg"></param>
        /// <returns></returns>

        public async Task<int> UpdateAsync(SG sg, List<SGD> gg)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $@"update [dbo].[salary_grant] set [checker]='{sg.Checker}',[check_time]='{sg.Check_time}',[check_status]=1,[salary_paid_sum]='{sg.Salary_paid_sum}' where [salary_grant_id]='{sg.Salary_grant_id}'";
                int res = await connection.ExecuteAsync(sql);
                if (res > 0)
                {
                    for (int i = 0; i < gg.Count; i++)
                    {
                        string ss = $@"update [dbo].[salary_grant_details] set [bouns_sum]='{gg[i].Bouns_sum}',[sale_sum]='{gg[i].Sale_sum}',[deduct_sum]='{gg[i].Deduct_sum}',[salary_paid_sum]='{gg[i].Salary_paid_sum}' where [human_id]='{gg[i].Human_id}'";
                        await connection.ExecuteAsync(ss);
                        string dd = $@"update [dbo].[human_file] set [paid_salary_sum]='{gg[i].Salary_paid_sum}' where [human_id]='{gg[i].Human_id}'";
                        await connection.ExecuteAsync(dd);
                    }
                }
                return 1;
            }
           
        }


    }
}
