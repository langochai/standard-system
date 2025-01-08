using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Standard
{
    public static class Language
    {
        static public string value
        {
            get { return HttpContext.Current.Request.Cookies["lang"] == null ? "vi" : HttpContext.Current.Request.Cookies["lang"].Value; }
        }
    }
}