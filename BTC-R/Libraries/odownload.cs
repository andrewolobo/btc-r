using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Web;

namespace AmHandler.Libraries
{
    public class download
    {
        public string[] postdata;
        public delegate void EventHandler();
        public event EventHandler _complete;
        public string data;
        public string postdatas;
        public Action function;
        public enum method { GET, POST }

        public download(string url, string method)
        {
            HttpWebRequest readRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (method != null)
            {
                readRequest.Method = method;
            }
            System.Diagnostics.Debug.WriteLine("Request Made");
            readRequest.BeginGetResponse(new AsyncCallback(responseStreamCallback), readRequest);
        }
        public download(string url, string method, Dictionary<String,String> headers)
        {
            HttpWebRequest readRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (method != null)
            {
                readRequest.Method = method;
            }
            foreach (var header in headers)
            {
                readRequest.Headers[header.Key] = header.Value;
            }

            readRequest.BeginGetResponse(new AsyncCallback(responseStreamCallback), readRequest);
        }
        public download(string url, string data, Action function)
        {
            this.function = function;
            HttpWebRequest readRequest = (HttpWebRequest)HttpWebRequest.Create(url);

            readRequest.Method = "POST";


            readRequest.Host = "http://www.rottentomatoes.com";
            readRequest.Accept = "application/json, text/javascript, */*; q=0.01";

            byte[] postBytes = Encoding.UTF8.GetBytes(data);
            Stream postStream = readRequest.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Close();

            System.Diagnostics.Debug.WriteLine("Request Handled");

            WebResponse response = readRequest.GetResponse();

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            postStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(postStream);

            string responseFromServer = reader.ReadToEnd();

            System.Diagnostics.Debug.WriteLine(responseFromServer);
            if (_complete == null)
            {
                _complete += new EventHandler(function);
            }
            _complete.Invoke();
        }
        public download(string url, string method, string data, Action function)
        {
            this.function = function;
            this.data = data;
            HttpWebRequest readRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (method != null)
            {
                readRequest.Method = method;
            }

            readRequest.ContentType = "application/json";

            System.Diagnostics.Debug.WriteLine("Request Handled");
            readRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), readRequest);
        }
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webRequest = (HttpWebRequest)asynchronousResult.AsyncState;

            var postStream = webRequest.EndGetRequestStream(asynchronousResult);

            System.Diagnostics.Debug.WriteLine(data);

            byte[] byteArray = Encoding.UTF8.GetBytes(data);

            postStream.Write(byteArray, 0, data.Length);

            postStream.Close();

            webRequest.BeginGetResponse(new AsyncCallback(responseStreamCallback), webRequest);
        }
        public download(string url, string method, bool thread)
        {
            HttpWebRequest readRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (method != null)
            {
                readRequest.Method = method;
            }
            System.Diagnostics.Debug.WriteLine("Request Made");
            readRequest.BeginGetResponse(new AsyncCallback(responseStreamCallback), readRequest);
        }
        private void responseStreamCallback(IAsyncResult ia)
        {
            HttpWebRequest response = (HttpWebRequest)ia.AsyncState;
            try
            {
                using (HttpWebResponse resp = (HttpWebResponse)response.EndGetResponse(ia))
                {
                    Stream stream = (Stream)resp.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    System.Diagnostics.Debug.WriteLine("Reading Stream");
                    while (!(reader.EndOfStream))
                    {
                        postdatas = reader.ReadToEnd();
                    }
                }


                System.Diagnostics.Debug.WriteLine("Callback Complete");
                if (_complete == null)
                {
                    _complete += new EventHandler(function);
                }
                _complete.Invoke();
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                if (_complete == null)
                {
                    _complete += new EventHandler(function);
                }
                _complete.Invoke();
            }


            //return s;
        }
        private void requestStreamCallback(IAsyncResult ia)
        {
            HttpWebRequest request = (HttpWebRequest)ia.AsyncState;
            Stream stream = request.EndGetRequestStream(ia);
            postdatas = "";
            System.Diagnostics.Debug.WriteLine("Saving Data");
            for (int i = 0; i < postdata.Length; i++)
            {
                if (i != postdata.Length - 1)
                {
                    postdatas += postdata[i] + "&";
                }
                else
                {
                    postdatas += postdata[i];
                }

            }

            //string postdata = "post=1&post2=2&post3=3";
            byte[] bytearray = Encoding.UTF8.GetBytes(postdatas);
            stream.Write(bytearray, 0, bytearray.Length);
            stream.Close();
            System.Diagnostics.Debug.WriteLine("Stream Closed");
            request.BeginGetResponse(new AsyncCallback(responseStreamCallback), request);

        }

        void respond()
        {
            System.Diagnostics.Debug.WriteLine(postdatas);
        }

    }


}
