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
    /// 权限管理
    /// </summary>
    public class PowerController : Controller
    {
        UserDAO uu=new UserDAO();
        // GET: Power
        /// <summary>
        /// 打开用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult User_list()
        {
            return View();
        }
        /// <summary>
        /// 打开角色管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Right_list()
        {
            return View();
        }
        /// <summary>
        /// 打开添加用户管理
        /// </summary>
        /// <returns></returns>
        public async Task <ActionResult> User_add()
        {
            ViewBag.xia = await uu.XiaSelectAsync();
            return View();
        }
        /// <summary>
        /// 打开添加用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Right_add()
        {
            return View();
        }
            
        /// <summary>
        /// 打开编辑用户管理
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> User_edit(int id)
        {
            ViewBag.quan = await uu.SelectIDAsync(id);
            ViewBag.xia = await uu.XiaSelectAsync();
            return View();
        }

        /// <summary>
        /// 打开编辑角色管理
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Right_list_information(int id)
        {
            ViewBag.quan = await uu.SelectIDJSAsync(id);
           
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> Insert(RolesView rr)
        {
            return await uu.InsertAsync(rr);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> InsertJS(Roles rr)
        {
            return await uu.InsertJSAsync(rr);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> Update(RolesView rr)
        {
            return await uu.UpdateAsync(rr);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> UpdateJS(Roles rr)
        {
            return await uu.UpdateJSAsync(rr);
        }

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> UpdateJS1(int rid,int pid)
        {
            return await uu.UpdateJS1Async(rid,pid);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="rr"></param>
        /// <returns></returns>
        public async Task<int> Delete(int uid,int rid)
        {
            return await uu.DeleteAsync(uid,rid);
        }
        /// <summary>
        /// 查询用户管理分页
        /// </summary>
        /// <param name="fenye"></param>
        /// <returns></returns>
        public async Task<ActionResult> FY(FenYe<RolesView> fenye, string tj)
        {
            fenye = await uu.UserSelectAsync(fenye, tj);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询角色管理分页
        /// </summary>
        /// <param name="fenye"></param>
        /// <returns></returns>
        public async Task<ActionResult> JS(FenYe<Roles> fenye, string tj)
        {
            fenye = await uu.UserSelectJSAsync(fenye, tj);
            return Json(fenye, JsonRequestBehavior.AllowGet);
        }
        public  async Task< ActionResult> Jurisdiction_find()
        {
            List<Trees> jj = await uu.GetFind();
            return Json(jj, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Jurisdiction_ID(int roleID)
        {
            int[] i=await uu.Childen(roleID);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

    }
}