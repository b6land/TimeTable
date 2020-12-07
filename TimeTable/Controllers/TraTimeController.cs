using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeTable.Models;

namespace TimeTable.Controllers
{
    public class TraTimeController : ApiController
    {
        DownloadHelper downloadHelper = null;

        public TraTimeController()
        {
            downloadHelper = new DownloadHelper();
        }


        public string Get(int code)
        {
            string result = "";
            if(downloadHelper.DownloadTraTimeTable(out result) == false)
            {
                result = "下載失敗";
            }
            return result;
        }
    }
}
