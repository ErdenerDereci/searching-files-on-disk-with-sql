using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using testWeb.BekendClasslari;

namespace testWeb.Controllers
{
    public class HomeController : Controller
    {

        
        // GET: Home
        public ActionResult Index(string Search,string radio)
        {
            
            if (radio == "Normal Arama" || radio == null  )
            {
                DosyaAra x = new DosyaAra();
                if (Search == null || Search == "")
                {
                    for (int i = 0; i < x.baslangictaYukle().Length; i++)
                    {
                        ViewData[i.ToString()] = x.baslangictaYukle()[i];
                    }
                }
                else
                {
                    x.Ara(@"D:/", "*" + Search + "*");

                    for (int i = 0; i < x.arananDosyalarVeKlasorler.Count; i++)
                    {
                        ViewData[i.ToString()] = x.arananDosyalarVeKlasorler[i];
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Sql Aramasi aktif degil')</script>");
            }
            
            
            return View();
        }
    }
}