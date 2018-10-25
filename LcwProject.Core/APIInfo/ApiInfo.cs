using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace LcwProject.Core.APIInfo
{
    public class ApiInfo
    {
        public static string APIURL {
            get
            {
                var URL = HostingEnvironment.MapPath("~/APIInfo/ApiInfo.txt");
                return File.ReadAllText(URL);
            }
        }
    }
}