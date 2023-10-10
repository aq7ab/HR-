using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    //三级
    public class CFTKDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<CFTK>> ThirdSelectAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from [dbo].[config_file_third_kind]";
                return (List<CFTK>)await connection.QueryAsync<CFTK>(sql);
            }
        }

        /// <summary>
        /// 根据ID查全部
        /// </summary>
        /// <returns></returns>
        public async Task<CFTK> ThirdSelectIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[config_file_third_kind] where ftk_id='{id}'";
                return await connection.QueryFirstAsync<CFTK>(sql);
            }
        }
        /// <summary>
        /// 修改的加载事件
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<First>> GetFindAsync()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select first_kind_id as value,first_kind_name as label from [dbo].[config_file_first_kind]";
                List<First> first_Cascaders = new List<First>() { };
                List<First> fir = con.Query<First>(sql).ToList();
                foreach (var item in fir)
                {
                    string sql_1 = $"select second_kind_id as value,second_kind_name as label from [dbo].[config_file_second_kind] where first_kind_id={item.value} ";
                    First cf = new First()
                    {
                        value = item.value,
                        label = item.label,
                        children = con.Query<Second>(sql_1).ToList(),
                    };
                    first_Cascaders.Add(cf);
                }
                return first_Cascaders;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="fFK"></param>
        /// <returns></returns>
        public async Task<int> ThirdUpdateAsync(CFTK fFK)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $@"update [dbo].[config_file_third_kind] set [third_kind_sale_id]='{fFK.Third_kind_sale_id}',[third_kind_is_retail]='{fFK.Third_kind_is_retail}' where [ftk_id]='{fFK.Ftk_id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="fFK"></param>
        /// <returns></returns>
        public async Task<int> ThirdInsertAsync(CFTK fFK)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string i = "SELECT TOP 1 \r\n    CASE \r\n        WHEN [third_kind_id] + 1 < 10 THEN '0' + CAST([third_kind_id] + 1 AS VARCHAR(2))\r\n        ELSE CAST([third_kind_id] + 1 AS VARCHAR(2))\r\n    END AS FormattedValue\r\nFROM [dbo].[config_file_third_kind] \r\nORDER BY ftk_id DESC";
                string id = await connection.QueryFirstAsync<string>(i);
                string sql = $"insert into [dbo].[config_file_third_kind](first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, third_kind_sale_id, third_kind_is_retail)values('{fFK.First_kind_id}',(select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{fFK.First_kind_id}'),'{fFK.Second_kind_id}',(select [second_kind_name] from [dbo].[config_file_second_kind] where [second_kind_id]='{fFK.Second_kind_id}'),'{id}','{fFK.Third_kind_name}','{fFK.Third_kind_sale_id}','{fFK.Third_kind_is_retail}')";
               
              
                return await connection.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ThirdDeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $@"delete from [dbo].[config_file_third_kind] where [ftk_id]='{id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
    }
}
