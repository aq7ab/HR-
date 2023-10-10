using DAO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 薪酬发放管理
    /// </summary>
    public class SalaryGrantController : Controller
    {
        SGDDAO sg = new SGDDAO();
        SSDAO ss = new SSDAO();
        // GET: SalaryGrant
        /// <summary>
        /// 打开薪酬发放登记
        /// </summary>
        /// <returns></returns>
        public ActionResult Register_locate()
        {
            return View();
        }
        /// <summary>
        /// 查询薪酬发放登记
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Register_list(string sql)
        {
            ViewBag.h = await sg.SGDSelectAsync(sql);
            return View();
        }
        /// <summary>
        /// 打开薪酬发放登记审核
        /// </summary>
        /// <returns></returns>
        public ActionResult Check_list()
        {
           
            return View();
        }
        /// <summary>
        /// 打开薪酬发放查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query_locate()
        {

            return View();
        }
        /// <summary>
        /// 打开登记
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Register_commit(string sql,string name)
        {
            ViewBag.h = await sg.SGDSelectIDAsync(sql);
            ViewBag.name = name;
            ViewBag.bh = await sg.BhAsync();
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 打开复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Check(string sql, string name,string bh)
        {
            ViewBag.h = await sg.SGDSelectBHAsync(sql);
            ViewBag.name = name;
            ViewBag.bh = bh;
            User user = (User)Session["user"];
            ViewBag.u = user;
          
            return View();
        }
        /// <summary>
        /// 打开查询详细
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Query_list(string name, string bh,string djr,string sj)
        {
            ViewBag.h = await sg.SGDSelectCXAsync(bh);
            ViewBag.name = name;
            ViewBag.bh = bh;
            ViewBag.djr = djr;
            ViewBag.sj = sj;
            return View();
        }
        /// <summary>
        /// 打开查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="name"></param>
        /// <param name="bh"></param>
        /// <returns></returns>
        public ActionResult SSGSelect(string sql)
        {
            ViewBag.h = sql;
            return View();
        }
        /// <summary>
        /// 查询复核分页
        /// </summary>
        /// <param name="fenye"></param>
        /// <returns></returns>
        public async Task<ActionResult> FY(FenYe<SG> fenye, string tj)
        {
            fenye = await sg.SGSelectAsync(fenye, tj);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询明细详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Xx(string id)
        {
            return Json(await ss.SSDSelectIDAsync(id), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Tian(SG s,List<SGD> gg )
        {
            return await sg.InsertAsync(s,gg);
        }

        [HttpPost]
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Xiu(SG s, List<SGD> gg)
        {
            return await sg.UpdateAsync(s, gg);
        }
        
    }
}