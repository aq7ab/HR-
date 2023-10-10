﻿using DAO;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebApplication1.Controllers 
{
    public class RecruitController : Controller
    {

        //一级机构设置
        CFFKDAO cf = new CFFKDAO();
        //二级机构设置
        CFSKDAO sk = new CFSKDAO();
        //三级机构设置
        CFTKDAO tk = new CFTKDAO();
        //职位设置
        ConfigMajorDao cd = new ConfigMajorDao();
        //职位分类设置
        ConfigMajorKindDao kd = new ConfigMajorKindDao();
        //公共属性设置
        ConfigPublicCharDao cp = new ConfigPublicCharDao();
        //职位发布登记
        EngageMajorReleaseDao ed = new EngageMajorReleaseDao();
        //简历登记
        EngageResumeDao er = new EngageResumeDao();
        // GET: Recruit
        /// <summary>
        /// 打开职位发布登记
        /// </summary>
        /// <returns></returns>
        //public ActionResult PositionRegister()
        //{
        //    PositionRegisterAsync();
        //    return View();
        //}
        public async Task<ActionResult> PositionRegister()
        {
            ViewBag.s = await cf.FirstSelectAsync();
            ViewBag.ss = await sk.SecondSelectAsync();
            ViewBag.sss = await tk.ThirdSelectAsync();
            ViewBag.ssss = await cd.CMSelectAsync();
            ViewBag.sssss = await kd.CMKSelectAsync();
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 提交职位发布登记
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public async Task<int> EMAdd(EngageMajorRelease em)
        {
            return await ed.EMRInsertAsync(em);
        }
        /// <summary>
        /// 打开职位发布变更
        /// </summary>
        /// <returns></returns>
        public ActionResult PositionChangeUpdate()
        {
            return View();
        }
        /// <summary>
        /// 查询职位发布变更
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public async Task<ActionResult> FenYe(FenYe<EngageMajorRelease> em,string tj)
        {
            em = await ed.EMRSelectAsync(em,tj);
            return Json(em, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 打开修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> PositionReleaseChange(int id)
        {
            ViewBag.s = await ed.EMRSelectIdAsync(id);
            return View();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> EMDelete(int id)
        {
            return await ed.EMRDeleteAsync(id);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="em"></param>
        /// <returns></returns>
        public async Task<int> EMDUpdate(EngageMajorRelease em)
        {
            return await ed.EMRUpdateAsync(em);
        }
        /// <summary>
        /// 打开职位发布查询
        /// </summary>
        /// <returns></returns>
        public ActionResult PositionReleaseSearch()
        {
            return View();
        }
        /// <summary>
        /// 打开简历登记
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Register(string fl,string mc,string zp)
        {
            ViewBag.s = await cd.CMSelectAsync();
            ViewBag.ss = await kd.CMKSelectAsync();
            ViewBag.fl = fl;
            ViewBag.mc = mc;
            ViewBag.zp = zp;
            User user = (User)Session["user"];
            ViewBag.u = user.U_name;
            return View();
        }
        /// <summary>
        /// 提交简历登记
        /// </summary>
        /// <param name="ee"></param>
        /// <returns></returns>
        public async Task<int> ERAdd(EngageResume ee)
        {
            return await er.ERInsertAsync(ee);
        }
        /// <summary>
        /// 打开简历筛选
        /// </summary>
        /// <returns></returns>
        public ActionResult ResumeChoose()
        {
            return View();
        }
        /// <summary>
        /// 级联职位
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ERXia()
        {
            IEnumerable cs = await er.ERXiaAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 级联机构
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> EMRXia()
        {
            IEnumerable cs = await ed.GetFindAsync();
            return Json(cs, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 打开面试结果登记
        /// </summary>
        /// <returns></returns>
        public ActionResult InterviewList()
        {
            return View();
        }
        /// <summary>
        /// 打开简历筛选查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ResumeList(string str)
        {
            ViewBag.str = str;
            return View();
        }
        /// <summary>
        /// 简历筛选查询
        /// </summary>
        /// <param name="ee"></param>
        /// <param name="tj"></param>
        /// <returns></returns>
        public async Task<ActionResult> ERFenYe(FenYe<EngageResume> ee, string tj)
        {
            ee = await er.ERSelectAsync(ee, tj);
            return Json(ee, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 打开简历复核
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> ResumeDetails(string id)
        {
            ViewBag.s = await er.ERSelectIdAsync(id);
           EngageResume EE= await er.ERSelectIdAsync(id);
            ViewBag.o = EE.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 推荐
        /// </summary>
        /// <param name="ee"></param>
        /// <returns></returns>
        public async Task<int> ERUpdate(EngageResume ee)
        {
            return await er.ERUpdateAsync(ee);
        }
        /// <summary>
        /// 打开有效简历查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ValidResume()
        {
            return View();
        }
        /// <summary>
        /// 打开有效简历查询详情
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public async Task<ActionResult> ResumeSelect(string id)
        {
            ViewBag.s = await er.ERSelectIdAsync(id);
            EngageResume EE = await er.ERSelectIdAsync(id);
            ViewBag.o = EE.human_picture;
            return View();
        }
        /// <summary>
        /// 已复核
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ActionResult ValidList(string str)
        {
            ViewBag.str = str;
            return View();
        }
    }
}