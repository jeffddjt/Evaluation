﻿

using Evaluation.CustomAttributes;
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.Controllers
{
    [BHAuthitication]
    public class DutiesController : Controller
    {
        private IDutiesService dutiesService;
        public DutiesController()
        {
            this.dutiesService = ServiceLocator.Instance.GetRef<IDutiesService>();
        }
        public ActionResult DutiesManage()
        {
            IList<DutiesDataObject> dutiesList = this.dutiesService.GetList();
            ViewData["dutiesList"] = dutiesList;
            return View();
        }
        public void SaveDuties(DutiesDataObject duty)
        {
            this.dutiesService.Update(duty);
        }
        public void DeleteDuties(DutiesDataObject duty)
        {
            this.dutiesService.RemoveById(duty.ID);
        }
    }
}