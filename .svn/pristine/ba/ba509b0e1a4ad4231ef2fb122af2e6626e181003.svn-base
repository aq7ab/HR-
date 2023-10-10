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
    public class ConfigMajorKindDao
    {
        //连接字符串
        string str = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";
        /// <summary>
        /// 查询职位分类
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ConfigMajorKind>> CMKSelectAsync()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = "select * from config_major_kind";
                return await con.QueryAsync<ConfigMajorKind>(sql);
            }
        }
        /// <summary>
        /// 添加职位分类
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public async Task<int> CMKInsertAsync(ConfigMajorKind cm)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = $@"insert into config_major_kind(major_kind_id,major_kind_name) values('{cm.major_kind_id}','{cm.major_kind_name}')";
                return await con.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 根据id删除职位分类数据
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        public async Task<int> CMKDeleteAsync(ConfigMajorKind cm)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = $@"delete from config_major_kind where mfk_id = '{cm.mfk_id}'";
                return await con.ExecuteAsync(sql);
            }
        }
    }
}
