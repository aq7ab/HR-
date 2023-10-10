using DAO;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    /// <summary>
    /// 调动管理
    /// </summary>
    public class TransferController : Controller
    {
        TransferDAO tt=new TransferDAO();
        //标准薪酬
        SSDAO ss = new SSDAO();
        // GET: Transfer
        /// <summary>
        /// 打开调动登记
        /// </summary>
        /// <returns></returns>
        public ActionResult Register_locate()
        {
            return View();
            
        }
        /// <summary>
        /// 打开调动审核
        /// </summary>
        /// <returns></returns>
        public ActionResult Check_list()
        {
            return View();

        }
        /// <summary>
        /// 打开调动查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Locate()
        {
            return View();

        }
       
        /// <summary>
        /// 打开调动登记列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Register_list(string str)
        {
            ViewBag.strList = str;
            return View();
            
        }
        /// <summary>
        /// 打开调动查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List(string str)
        {
            ViewBag.strList = str;
            return View();

        }
        /// <summary>
        /// 打开调动
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Register(string bh)
        {
            User user = (User)Session["user"];
            ViewBag.u = user;
            ViewBag.xia = await tt.XiaSelectAsync();
            ViewBag.s = await tt.SSelectIDAsync(bh);
            return View();

        }
        /// <summary>
        /// 打开审核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Check(int bh)
        {
            User user = (User)Session["user"];
            ViewBag.u = user;
            ViewBag.xia = await tt.XiaSelectAsync();
            ViewBag.s = await tt.SSDelectIDAsync(bh);
            return View();

        }
        /// <summary>
        /// 打开查看
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Detail(int bh)
        {
           
            ViewBag.s = await tt.SSDelectIDAsync(bh);
            return View();

        }
        
        /// <summary>
        /// 查询调动登记列表分页
        /// </summary>
        /// <param name="fenye"></param>
        /// <returns></returns>
        public async Task<ActionResult> FY(FenYe<Fill> fenye, string tj)
        {
            fenye = await tt.SGSelectAsync(fenye, tj);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询调动审核分页
        /// </summary>
        /// <param name="fenye"></param>
        /// <returns></returns>
        public async Task<ActionResult> FY1(FenYe<MC> fenye, string tj)
        {
            fenye = await tt.SHSelectAsync(fenye, tj);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择调动登记机构
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Xia()
        {
            IEnumerable cs = await tt.GetFindAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 选择调动职位
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Xia1()
        {
            IEnumerable cs = await tt.ZwAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据id查询薪酬
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Cx(int id)
        {
            SS cs = await ss.SSelectIDAsync(id);
            return Json(cs, JsonRequestBehavior.AllowGet);
        }

        public async Task<int> Insert(MC mC)
        {
            return await tt.DDInsertAsync(mC);
        }
        public async Task<int> Update(MC mC)
        {
            return await tt.DDUpdateAsync(mC);
        }
    }
}