using AmHandler.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace XKCDAPI.Libraries
{
    public class AsyncDownload
    {
        public static Task<string> VDownload(string query)
        {
            var taskCompletionSource = new TaskCompletionSource<String>();
            download d = new download(query, "GET");
            d._complete += delegate()
            {
                taskCompletionSource.SetResult(d.postdatas);
            };
            return taskCompletionSource.Task;
        }
    }
}