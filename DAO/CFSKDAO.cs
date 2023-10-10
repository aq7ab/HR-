﻿using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
   //二级
    public class CFSKDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public async Task<List<CFSK>> SecondSelectAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from [dbo].[config_file_second_kind]";
                return (List<CFSK>)await connection.QueryAsync<CFSK>(sql);
            }
        }
        /// <summary>
        /// 根据ID查全部
        /// </summary>
        /// <returns></returns>
        public async Task<CFSK> SecondSelectIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[config_file_second_kind] where fsk_id='{id}'";
                return await connection.QueryFirstAsync<CFSK>(sql);
            }
        }
        /// <summary>
        /// 根据一级ID查二级
        /// </summary>
        /// <returns></returns>
        public async Task<List<CFSK>> SelectIDAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[config_file_second_kind] where [first_kind_id]='{id}'";
                return (List<CFSK>)await connection.QueryAsync<CFSK>(sql);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="cFSK"></param>
        /// <returns></returns>
        public async Task<int> SecondInsertAsync(CFSK cfsk)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"insert into [dbo].[config_file_second_kind]([first_kind_id], [first_kind_name], [second_kind_id], [second_kind_name], [second_salary_id], [second_sale_id])" +
                                   $"values('{cfsk.First_kind_id}',(select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{cfsk.First_kind_id}')," +
                                   $"(SELECT TOP 1  CASE  WHEN [second_kind_id] + 1 < 10 THEN '0' + CAST([second_kind_id] + 1 AS VARCHAR(2)) ELSE CAST([second_kind_id] + 1 AS VARCHAR(2))  END AS FormattedValue FROM [dbo].[config_file_second_kind] ORDER BY fsk_id DESC)," +
                                   $"'{cfsk.Second_kind_name}','{cfsk.Second_salary_id}','{cfsk.Second_sale_id}')";
                return await connection.ExecuteAsync(sql);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="fFK"></param>
        /// <returns></returns>
        public async Task<int> SecondUpdateAsync(CFSK fFK)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $@"update  [dbo].[config_file_second_kind] set second_salary_id='{fFK.Second_salary_id}', second_sale_id='{fFK.Second_sale_id}' where fsk_id='{fFK.Fsk_id}'";
                return await connection.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> SecondDeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {

                string sql = $@"delete from [dbo].[config_file_second_kind] where [fsk_id]='{id}'";
                return await connection.ExecuteAsync(sql);
            }
        }

    }
}
