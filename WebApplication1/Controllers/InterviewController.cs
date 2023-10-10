﻿using DAO;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;


namespace WebApplication1.Controllers
{
    public class InterviewController : Controller
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
        /// <summary>
        /// 打开面试结果登记
        /// </summary>
        /// <returns></returns>
        public ActionResult InterviewList()
        {
            return View();
        }
        /// <summary>
        /// 打开登记
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> InterviewRegister(string id)
        {
            ViewBag.s = await er.ERSelectIdAsync(id);
            EngageResume EE = await er.ERSelectIdAsync(id);
            ViewBag.o = EE.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public async Task<int> EIAdd(EngageResume ei)
        {
            return await er.EIInsertAsync(ei);
        }
        /// <summary>
        /// 打开筛选
        /// </summary>
        /// <returns></returns>
        public ActionResult InterviewResume()
        {
            return View();
        }
        /// <summary>
        /// 打开面试结果列表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public ActionResult SiftList(string str)
        {
            ViewBag.str = str;
            return View();
        }
        /// <summary>
        /// 查询面试结果
        /// </summary>
        /// <param name="fen"></param>
        /// <param name="tj"></param>
        /// <returns></returns>
        public async Task<ActionResult> EIFenye(FenYe<EngageResume> fen,string tj)
        {
            fen = await er.EISelectAsync(fen, tj);
            return Json(fen, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 打开筛选
        /// </summary>
        /// <param name="ii"></param>
        /// <returns></returns>
        public async Task<ActionResult> InterviewSift(string ii,string id)
        {
            ViewBag.i = await er.EISelectIdAsync(ii);
            EngageResume EE = await er.ERSelectIdAsync(id);
            ViewBag.o = EE.human_picture;
            //ViewBag.s = await er.EISelectIdAsync(ii);
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 面试筛选
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public async Task<int> EIUpdate(EngageResume ei)
        {
            return await er.EIUpdateAsync(ei);
        }
        /// <summary>
        /// 打开录用申请列表
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterList()
        {
            return View();
        }
        /// <summary>
        /// 打开录用申请
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Register(string ii)
        {
            ViewBag.i = await er.EIIdAsync(ii);
            EngageResume EE = await er.ERSelectIdAsync(ii);
            ViewBag.o = EE.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        /// <summary>
        /// 删除简历
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> ERDelete(string id)
        {
            return await er.ERDeleteAsync(id);
        }
        /// <summary>
        /// 打开录用审批列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckList()
        {
            return View();
        }
        /// <summary>
        /// 打开录用审批
        /// </summary>
        /// <param name="ii"></param>
        /// <returns></returns>
        public async Task<ActionResult> Check(string ii)
        {
            ViewBag.i = await er.EIIdAsync(ii);
            EngageResume EE = await er.ERSelectIdAsync(ii);
            ViewBag.o = EE.human_picture;
            return View();
        }
        /// <summary>
        /// 打开录用查询列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return View();
        }
        /// <summary>
        /// 打开详细信息
        /// </summary>
        /// <param name="ii"></param>
        /// <returns></returns>
        public async Task<ActionResult> Details(string ii)
        {
            ViewBag.i = await er.EIIdAsync(ii);
            EngageResume EE = await er.ERSelectIdAsync(ii);
            ViewBag.o = EE.human_picture;
            User user = (User)Session["user"];
            ViewBag.u = user;
            return View();
        }
        public async Task<int> Update(EngageResume ei)
        {
            return await er.EE(ei);
        }
        public async Task<int> UU(EngageResume ei)
        {
            return await er.II(ei);
        }
        //发邮箱
        public ActionResult Create(string name)
        {
            try
            {
                WebMail.SmtpServer = "smtp.qq.com ";
                WebMail.SmtpPort = 25;
                WebMail.EnableSsl = true;
                WebMail.UserName = "2946068721@qq.com";
                WebMail.Password = "qkonaqkcbjqhdeae";
                WebMail.From = "2946068721@qq.com";
                WebMail.Send(name, "欢迎您的入职", "人事部门");
                return Content("成功");
            }
            catch (Exception)
            {
                return Content("失败");
            }
        }

        public async Task<int> OO(EngageResume ei)
        {
            return await er.IO(ei);
        }
    }
}