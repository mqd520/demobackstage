using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

using AutoFacUtils;
using DemoBackStage.Entity;
using DemoBackStage.IRepository;

using DemoBackStage.Web.Areas.System.Models;

namespace DemoBackStage.Web.Areas.System.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Index(UserLoginLogQueryModel param)
        {
            return View();
        }
    }
}