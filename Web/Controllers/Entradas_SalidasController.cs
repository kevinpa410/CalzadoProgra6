using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;

namespace Web.Controllers
{
    public class Entradas_SalidasController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Gestion_Entradas_Salidas> lista = null;
            try
            {
                IServiceEntradas_Salidas _SeviceEntradas_Salidas = new ServiceEntradas_Salidas();
                lista = _SeviceEntradas_Salidas.GetEntradas_Salidas();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }


        // GET: Autor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Autor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Autor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Autor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Autor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}