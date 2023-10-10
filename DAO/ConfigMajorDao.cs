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
    public class ConfigMajorDao
    {
        //连接字符串
        string lj = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        /// <summary>
        /// 查询职位设置
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ConfigMajor>> CMSelectAsync()
        {
            using (SqlConnection con = new SqlConnection(lj))
            {
                string sql = "select * from config_major";
                return await con.QueryAsync<ConfigMajor>(sql);
            }
        }
        public async Task<IEnumerable<ConfigMajor>> CMSelectIAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(lj))
            {
                string sql = $@"select * from config_major where major_id = '{id}'";
                return await con.QueryAsync<ConfigMajor>(sql);
            }
        }
        /// <summary>
        /// 根据ID查分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<ConfigMajorKind>> CMSelectIdAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(lj))
            {
                string sql = $@"select * from config_major_kind where major_kind_id = '{id}'";
                return (List<ConfigMajorKind>)await con.QueryAsync(sql);
            }
        }
        /// <summary>
        /// 删除职位设置
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public async Task<int> CMDeleteAsync(ConfigMajor cm)
        {
            using (SqlConnection con = new SqlConnection(lj))
            {
                string sql = $@"delete from config_major where mak_id = '{cm.mak_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 添加职位设置
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public async Task<int> CMInsertAsync(ConfigMajor cm)
        {
            using (SqlConnection con = new SqlConnection(lj))
            {
                string ii = $@"select major_kind_id from config_major_kind where major_kind_name = '{cm.major_kind_name}'";
                string id = await con.QueryFirstAsync<string>(ii);
               
                string sql = $@"insert into config_major(major_kind_id,major_kind_name,major_id,major_name,test_amount) values('{id}','{cm.major_kind_name}','{cm.major_id}','{cm.major_name}','{cm.test_amount}')";
                return await con.ExecuteAsync(sql); 
            }
        }
    }
}