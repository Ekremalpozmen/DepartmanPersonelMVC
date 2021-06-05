using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace PersonelMVCUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/scripts").Include(
               "~/Scripts/jquery-1.8.0.min.js",
               "~/Scripts/jquery.unobtrusive-ajax.min.js",
               "~/Scripts/bootstrap.min.js",
               "~/Scripts/DataTables/jquery.dataTables.min.js",
               "~/Scripts/DataTables/dataTables.bootstrap.min.js",
               "~/Scripts/custom.js"
                ));
        }
    }
}