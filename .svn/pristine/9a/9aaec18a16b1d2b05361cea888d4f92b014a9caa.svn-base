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
    public class TransferDAO
    {
        string conStr = "Data Source=.;Initial Catalog=HR_DB;Integrated Security=True";


        /// <summary>
        /// 查询调动登记分页
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public async Task<FenYe<Fill>> SGSelectAsync(FenYe<Fill> fc, string tj)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("@pageSize", fc.PageSize);
                dd.Add("@keyName", "huf_id");
                dd.Add("@tableName", "human_file");
                dd.Add("@currentPage", fc.CurrentPage);
                dd.Add("where", tj);
                dd.Add("@row", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[FenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @row out";
                fc.List = await con.QueryAsync<Fill>(sql, dd);
                fc.Rows = dd.Get<int>("row");
                return fc;
            }
        }

        /// <summary>
        /// 查询调动审核分页
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public async Task<FenYe<MC>> SHSelectAsync(FenYe<MC> fc, string tj)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                DynamicParameters dd = new DynamicParameters();
                dd.Add("@pageSize", fc.PageSize);
                dd.Add("@keyName", "mch_id");
                dd.Add("@tableName", "major_change");
                dd.Add("@currentPage", fc.CurrentPage);
                dd.Add("where", tj);
                dd.Add("@row", direction: ParameterDirection.Output, dbType: DbType.Int32);
                string sql = "exec [dbo].[FenYe] @pageSize, @keyName, @tableName, @currentPage,@where, @row out";
                fc.List = await con.QueryAsync<MC>(sql, dd);
                fc.Rows = dd.Get<int>("row");
                return fc;
            }
        }

        /// <summary>
        /// 三级机构级联
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LianJi>> GetFindAsync()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[config_file_first_kind]";
                IEnumerable<CFFK> first_Cascaders = await con.QueryAsync<CFFK>(sql);
                List<LianJi> fir = new List<LianJi>();
                foreach (CFFK item in first_Cascaders)
                {

                    LianJi cf = new LianJi()
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

        public async Task<IEnumerable<LianJi>> Cha(string id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select [second_kind_id],[second_kind_name] from [dbo].[config_file_second_kind] where [first_kind_id]='{id}'";
                IEnumerable<CFSK> first_Cascaders = await con.QueryAsync<CFSK>(sql);
                List<LianJi> fir = new List<LianJi>();
                foreach (CFSK item in first_Cascaders)
                {

                    LianJi cf = new LianJi()
                    {
                        value = item.Second_kind_id,
                        label = item.Second_kind_name,
                        children = await Cha1(item.Second_kind_id),
                    };
                    fir.Add(cf);
                }
                return fir;
            }

        }

        public async Task<IEnumerable<LianJi>> Cha1(string id)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select [third_kind_id],[third_kind_name] from [dbo].[config_file_third_kind] where [second_kind_id]='{id}'";
                IEnumerable<CFTK> first_Cascaders = await con.QueryAsync<CFTK>(sql);
                List<LianJi> fir = new List<LianJi>();
                foreach (CFTK item in first_Cascaders)
                {

                    LianJi cf = new LianJi()
                    {
                        value = item.Third_kind_id,
                        label = item.Third_kind_name,
                        
                    };
                    fir.Add(cf);
                }
                return fir;
            }

        }


        /// <summary>
        /// 根据ID查全部
        /// </summary>
        /// <returns></returns>
        public async Task<Fill> SSelectIDAsync(string id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[human_file] where [human_id]='{id}'";
                return await connection.QueryFirstAsync<Fill>(sql);
            }
        }

        /// <summary>
        /// 根据ID查全部调动
        /// </summary>
        /// <returns></returns>
        public async Task<MC> SSDelectIDAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select * from [dbo].[major_change] where mch_id='{id}'";
                return await connection.QueryFirstAsync<MC>(sql);
            }
        }

        /// <summary>
        /// 职位级联
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<First>> ZwAsync()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string sql = $"select major_kind_id as value,major_kind_name as label from [dbo].[config_major_kind]";
                List<First> first_Cascaders = new List<First>() { };
                List<First> fir = con.Query<First>(sql).ToList();
                foreach (var item in fir)
                {
                    string sql_1 = $"select major_id as value,major_name as label from [dbo].[config_major] where major_kind_id={item.value} ";
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
        /// 薪酬标准下拉框
        /// </summary>
        /// <returns></returns>
        public async Task<List<SS>> XiaSelectAsync()
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = "select * from [dbo].[salary_standard]";
                return (List<SS>)await connection.QueryAsync<SS>(sql);
            }
        }
        /// <summary>
        /// 添加调动
        /// </summary>
        /// <param name="mC"></param>
        /// <returns></returns>
        public async Task<int> DDInsertAsync(MC mC)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select major_kind_name from [dbo].[config_major_kind] where [major_kind_id]='{mC.new_major_kind_id}'";
                string cc=connection.QueryFirstOrDefault<string>(sql);
                string sql1 = $"select major_name from [dbo].[config_major] where [major_kind_id]='{mC.new_major_kind_id}' and [major_id]='{mC.new_major_id}'";
                string cc1 = connection.QueryFirstOrDefault<string>(sql1);
                string yi = $"select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{mC.new_first_kind_id}'";
                string yij=connection.QueryFirstOrDefault<string>(yi);

                string er = $@"select second_kind_name from [dbo].[config_file_second_kind] where[second_kind_id]='{mC.new_second_kind_id}'";
                string erj = "";
                try
                {
                    erj= connection.QueryFirstOrDefault<string>(er);
                }
                catch (Exception e)
                {
                   erj= null;
                }

                string san = $@"select [third_kind_name] from [dbo].[config_file_third_kind] where [third_kind_id]='{mC.new_third_kind_id}'";
                string sanj = "";
                try
                {
                    sanj = connection.QueryFirstOrDefault<string>(san);
                }
                catch (Exception e)
                {
                    sanj = null;
                }
                string gb = $"update [dbo].[human_file] set dd=1 where [human_id]='{mC.human_id}'";
                await connection.ExecuteAsync(gb);
                string ss = $"insert into [dbo].[major_change](first_kind_id, first_kind_name, second_kind_id, second_kind_name, third_kind_id, third_kind_name, major_kind_id, major_kind_name, major_id, major_name, new_first_kind_id, new_first_kind_name, new_second_kind_id, new_second_kind_name, new_third_kind_id, new_third_kind_name, new_major_kind_id, new_major_kind_name, new_major_id, new_major_name, human_id, human_name, salary_standard_id, salary_standard_name, salary_sum, new_salary_standard_id, new_salary_standard_name, new_salary_sum, change_reason, register,  regist_time)" +
                    $"values('{mC.first_kind_id}','{mC.first_kind_name}','{mC.second_kind_id}','{mC.second_kind_name}','{mC.third_kind_id}','{mC.third_kind_name}','{mC.major_kind_id}','{mC.major_kind_name}','{mC.major_id}','{mC.major_name}','{mC.new_first_kind_id}','{yij}','{mC.new_second_kind_id}','{erj}','{mC.new_third_kind_id}','{sanj}','{mC.new_major_kind_id}','{cc}','{mC.new_major_id}','{cc1}','{mC.human_id}','{mC.human_name}','{mC.salary_standard_id}','{mC.salary_standard_name}','{mC.salary_sum}','{mC.new_salary_standard_id}','{mC.new_salary_standard_name}','{mC.new_salary_sum}','{mC.change_reason}','{mC.register}','{mC.regist_time}')";

                return await connection.ExecuteAsync(ss);
            }
        }


        /// <summary>
        /// 审核调动
        /// </summary>
        /// <param name="mC"></param>
        /// <returns></returns>
        public async Task<int> DDUpdateAsync(MC mC)
        {
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                string sql = $"select major_kind_name from [dbo].[config_major_kind] where [major_kind_id]='{mC.new_major_kind_id}'";
                string cc = connection.QueryFirstOrDefault<string>(sql);
                string sql1 = $"select major_name from [dbo].[config_major] where [major_kind_id]='{mC.new_major_kind_id}' and [major_id]='{mC.new_major_id}'";
                string cc1 = connection.QueryFirstOrDefault<string>(sql1);
                string yi = $"select [first_kind_name] from [dbo].[config_file_first_kind] where [first_kind_id]='{mC.new_first_kind_id}'";
                string yij = connection.QueryFirstOrDefault<string>(yi);

                string er = $@"select second_kind_name from [dbo].[config_file_second_kind] where[second_kind_id]='{mC.new_second_kind_id}'";
                string erj = "";
                try
                {
                    erj = connection.QueryFirstOrDefault<string>(er);
                }
                catch (Exception e)
                {
                    erj = null;
                }

                string san = $@"select [third_kind_name] from [dbo].[config_file_third_kind] where [third_kind_id]='{mC.new_third_kind_id}'";
                string sanj = "";
                try
                {
                    sanj = connection.QueryFirstOrDefault<string>(san);
                }
                catch (Exception e)
                {
                    sanj = null;
                }
                string max = $"select max(major_change_amount)+1 from [dbo].[human_file] where [human_id]='{mC.human_id}'";
                string max1= connection.QueryFirstOrDefault<string>(max);
                string gb = $"update [dbo].[human_file] set dd = null,major_change_amount='{max1}',first_kind_id='{mC.new_first_kind_id}',first_kind_name='{yij}',second_kind_id='{mC.new_second_kind_id}',second_kind_name='{erj}',third_kind_id='{mC.new_third_kind_id}', third_kind_name='{sanj}',human_major_kind_id='{mC.new_major_kind_id}',human_major_kind_name='{cc}', human_major_id='{mC.new_major_id}', hunma_major_name='{cc1}',salary_standard_id='{mC.new_salary_standard_id}', salary_standard_name='{mC.new_salary_standard_name}',salary_sum='{mC.new_salary_sum}'where [human_id]='{mC.human_id}'";
                await connection.ExecuteAsync(gb);
                string ss = $"update [dbo].[major_change]set new_first_kind_id='{mC.new_first_kind_id}', new_first_kind_name='{yij}', new_second_kind_id='{mC.new_second_kind_id}', new_second_kind_name='{erj}', new_third_kind_id='{mC.new_third_kind_id}', new_third_kind_name='{sanj}', new_major_kind_id='{mC.new_major_kind_id}', new_major_kind_name='{cc}', new_major_id='{mC.new_major_id}', new_major_name='{cc1}',new_salary_standard_id='{mC.new_salary_standard_id}', new_salary_standard_name='{mC.new_salary_standard_name}', new_salary_sum='{mC.new_salary_sum}',  check_reason='{mC.check_reason}', check_status='1',checker='{mC.checker}', check_time='{mC.check_time}' where mch_id='{mC.mch_id}'";
                return await connection.ExecuteAsync(ss);
            }
        }
    }
}
