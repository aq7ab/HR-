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
    public class EngageMajorReleaseDao
    {
        string str = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";

        /// <summary>
        /// 职位发布登记
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public async Task<int> EMRInsertAsync(EngageMajorRelease em)
        {
            using(SqlConnection con = new SqlConnection(str))
            {
                string fd = $@"select first_kind_name from config_file_first_kind where first_kind_id = '{em.first_kind_name}'";
                string fid = await con.QueryFirstAsync<string>(fd);
                if (em.second_kind_name=="")
                {
                    em.second_kind_name = null;
                }
                string sd = $@"select second_kind_name from config_file_second_kind where second_kind_id = '{em.second_kind_name}'";
                string sid = "";
                try
                {
                    sid = await con.QueryFirstAsync<string>(sd);
                }
                catch (Exception e)
                {
                    sid = null;
                }
               
               

                string td = $@"select third_kind_name from config_file_third_kind where third_kind_id = '{em.third_kind_name}'";
                string tid = "";
                try
                {
                    tid = await con.QueryFirstAsync<string>(td);
                }
                catch (Exception e)
                {
                    tid = null;
                }
               

                string mdk = $@"select major_kind_name from config_major_kind where major_kind_id = '{em.major_kind_name}'";
                string midk = await con.QueryFirstAsync<string>(mdk);

                string md = $@"select major_name from config_major where major_id = '{em.major_name}' and major_kind_id = '{em.major_kind_name}'";
                string mid = await con.QueryFirstAsync<string>(md);

                string sql = $@"insert into engage_major_release
                (first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, major_kind_id, major_kind_name, major_id, major_name, human_amount, engage_type, deadline, register, changer, regist_time, change_time, major_describe, engage_required)
                 values('{em.first_kind_name}','{fid}','{em.second_kind_name}','{sid}','{em.third_kind_name}','{tid}','{em.major_kind_name}','{midk}','{em.major_name}','{mid}','{em.human_amount}','{em.engage_type}','{em.deadline}','{em.register}','{em.changer}','{em.regist_time}','{em.change_time}','{em.major_describe}','{em.engage_required}')";
                return await con.ExecuteAsync(sql);
            }
        }
        /// <summary>
        /// 职位发布变更
        /// </summary>
        /// <param name="emr"></param>
        /// <returns></returns>
        public async Task<FenYe<EngageMajorRelease>> EMRSelectAsync(FenYe<EngageMajorRelease> emr,string tj)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("@pageSize", emr.PageSize);
                dd.Add("@keyName", "mre_id");
                dd.Add("@tableName", "engage_major_release");
                dd.Add("@currentPage", emr.CurrentPage);
                dd.Add("@where", tj);
                dd.Add("@row", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[FenYe] @pageSize, @keyName, @tableName, @currentPage, @where, @row out";
                emr.List = await con.QueryAsync<EngageMajorRelease>(sql, dd);
                emr.Rows = dd.Get<int>("row");
                return emr;
            }
        }
        public async Task<EngageMajorRelease> EMRSelectIdAsync(int id)
        {
            using(SqlConnection con = new SqlConnection(str))
            {
                string sql = $@"select * from engage_major_release where mre_id = '{id}'";
                return await con.QueryFirstAsync<EngageMajorRelease>(sql);
            }
        }
        public async Task<int> EMRUpdateAsync(EngageMajorRelease ee)
        {
            using(SqlConnection con = new SqlConnection(str))
            {
                string sql = $@"update engage_major_release set first_kind_name = '{ee.first_kind_name}',
                    second_kind_name = '{ee.second_kind_name}',third_kind_name = '{ee.third_kind_name}',
major_kind_name = '{ee.major_kind_name}',major_name = '{ee.major_name}',engage_type = '{ee.engage_type}',human_amount = '{ee.human_amount}',deadline = '{ee.deadline}',
register = '{ee.register}',regist_time = '{ee.regist_time}',major_describe = '{ee.major_describe}',engage_required = '{ee.engage_required}' where mre_id = '{ee.mre_id}'";
                return await con.ExecuteAsync(sql);
            }
        }

        public async Task<int> EMRDeleteAsync(int id)
        {
            using(SqlConnection con = new SqlConnection(str))
            {
                string sql = $@"delete from engage_major_release where mre_id = '{id}'";
                return await con.ExecuteAsync(sql);
            }
        }
        public async Task<IEnumerable<Lian>> GetFindAsync()
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = $"select * from [dbo].[config_file_first_kind]";
                IEnumerable<CFFK> first_Cascaders = await con.QueryAsync<CFFK>(sql);
                List<Lian> fir = new List<Lian>();
                foreach (CFFK item in first_Cascaders)
                {

                    Lian cf = new Lian()
                    {
                        value = item.First_kind_id,
                        label = item.First_kind_name,
                        children = await Cha(item.First_kind_id),
                    };
                    fir.Add(cf);
                }
                return fir;
            }



        }

        public async Task<IEnumerable<Lian>> Cha(string id)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = $"select [second_kind_id],[second_kind_name] from [dbo].[config_file_second_kind] where [first_kind_id]='{id}'";
                IEnumerable<CFSK> first_Cascaders = await con.QueryAsync<CFSK>(sql);
                List<Lian> fir = new List<Lian>();
                foreach (CFSK item in first_Cascaders)
                {

                    Lian cf = new Lian()
                    {
                        value = item.Second_kind_id,
                        label = item.Second_kind_name,
                        children = (IEnumerable<Lian>)await Cha1(item.Second_kind_id),
                    };
                    fir.Add(cf);
                }
                return fir;
            }

        }

        public async Task<IEnumerable<Lian>> Cha1(string id)
        {
            using (SqlConnection con = new SqlConnection(str))
            {
                string sql = $"select [third_kind_id],[third_kind_name] from [dbo].[config_file_third_kind] where [second_kind_id]='{id}'";
                IEnumerable<CFTK> first_Cascaders = await con.QueryAsync<CFTK>(sql);
                List<Lian> fir = new List<Lian>();
                foreach (CFTK item in first_Cascaders)
                {

                    Lian cf = new Lian()
                    {
                        value = item.Third_kind_id,
                        label = item.Third_kind_name,

                    };
                    fir.Add(cf);
                }
                return fir;
            }

        }

    }
}
