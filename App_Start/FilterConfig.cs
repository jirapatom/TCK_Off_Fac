﻿using System.Web;
using System.Web.Mvc;

namespace TCK_Off_Fac
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
