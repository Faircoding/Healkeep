using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Healkeep;

namespace Healkeep
{
    public class MyBaseController : Controller
    {
        //Here I have created this for execute each time any controller load ( inherit this) load
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie LangCookie = Request.Cookies["culture"];
            if (LangCookie != null)
            {
                lang = LangCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = SiteLanguages.GetDefaultLanguage();
                }
            }

            new SiteLanguages().SetLanguage(lang);

            return base.BeginExecuteCore(callback, state);
        }
    }
}