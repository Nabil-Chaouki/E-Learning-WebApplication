using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace E_Learning_Prototype.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.3.1.min.js"
            ));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                "~/Scripts/adminlte.js"
            ));
            bundles.Add(new StyleBundle("~/Content/Css").Include(
                "~/Content/all.min.css",
                "~/Content/adminlte.min.css"
            )); 



        }
    }
}