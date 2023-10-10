using DAO;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HumanResourcesController : Controller
    {
        TransferDAO tt = new TransferDAO();
        //公共属性设置
        ConfigPublicCharDao cp = new ConfigPublicCharDao();
        //职位发布登记
        EngageMajorReleaseDao ed = new EngageMajorReleaseDao();
        //
        EngageResumeDao er = new EngageResumeDao();
        //人力资源
        HumanFileDao hf = new HumanFileDao();
        // GET: HumanResources
        /// <summary>
        /// 打开人力资源档案登记
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> HumanRegister(string id)
        {
            ViewBag.xia = await tt.XiaSelectAsync();
            ViewBag.ssssss = await cp.ZCSelectAsync();
            ViewBag.xi=await er.ERSelectIdAsync(id);
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        public ActionResult RL()
        {
            return View();
        }
        /// <summary>
        /// 人力资源档案复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Human_check(string id)
        {
            ViewBag.xia = await tt.XiaSelectAsync();
            ViewBag.ssssss = await cp.ZCSelectAsync();
            ViewBag.xx = await hf.Jia(id);
            HumanFile ff= await hf.Jia(id);
            ViewBag.img = ff.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 人力资源档案变更
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Change_list_information(string id)
        {
            ViewBag.xia = await tt.XiaSelectAsync();
            ViewBag.ssssss = await cp.ZCSelectAsync();
            ViewBag.xx = await hf.Jia(id);
            HumanFile ff = await hf.Jia(id);
            ViewBag.img = ff.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        
        public ActionResult WSC()
        {
            var file = HttpContext.Request.Files["file"];//获取上传文件对象
                                                         //生成文件名
            string name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //获取后缀名
            string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            //相对路径
            string mz = name + ext;
            string path = $"~/Uploaders/" + mz;
            //绝对路径
            string jpath = Server.MapPath(path);
            //创建上传文件对应的文件夹
            (new FileInfo(jpath)).Directory.Create();
            file.SaveAs(jpath);
            return Content(mz);
            
        }
        /// <summary>
        /// 提交人力资源档案登记
        /// </summary>
        /// <param name="ee"></param>
        /// <returns></returns>
        public async Task<int> HMInsert(HumanFile ee)
        {
            return await hf.HFInsertAsync(ee);
        }
        /// <summary>
        /// 提交人力资源档案复核
        /// </summary>
        /// <param name="ee"></param>
        /// <returns></returns>
        public async Task<int> HMUpdate(HumanFile ee)
        {
            return await hf.HFUpdateAsync(ee);
        }
        /// <summary>
        /// 打开人力资源档案登记复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> CheckList()
        {
            ViewBag.h = await hf.HFCount();
            return View();
        }
        /// <summary>
        /// 打开人力资源档案查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Query_locate()
        {
            return View();
        }
        /// <summary>
        /// 打开人力资源档案变更
        /// </summary>
        /// <returns></returns>
        public ActionResult Change_locate()
        {
            return View();
        }
        /// <summary>
        /// 打开人力资源档案变更查询
        /// </summary>
        /// <returns></returns>
        public ActionResult Change_keywords()
        {
            return View();
        }
        

        /// <summary>
        /// 打开人力资源档案列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Query_list(string str)
        {
            ViewBag.tj = str;
            return View();
        }
        /// <summary>
        /// 打开人力资源档案变更列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Change_list(string str)
        {
            ViewBag.tj = str;
            return View();
        }
        
        /// <summary>
        /// 打开人力资源档案查看
        /// </summary>
        /// <returns></returns>
        public async Task <ActionResult> Query_list_information(string id)
        {
            ViewBag.xx = await hf.Jia(id);
            HumanFile ff = await hf.Jia(id);
            ViewBag.img = ff.human_picture;
            return View();
        }
        
        /// <summary>
        /// 查询人力资源档案登记复核
        /// </summary>
        /// <param name="hh"></param>
        /// <param name="tj"></param>
        /// <returns></returns>
        public async Task<ActionResult> FenYe(FenYe<HumanFile> hh,string tj)
        {
            hh = await hf.HFSelectAsync(hh, tj);
            return Json(hh, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> HFXia()
        {
            IEnumerable cs = await hf.GetFindAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }
    }
}