using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryAPI;

namespace ServerCore
{
    class Program
    {
        static void Main(string[] args)
        {
//        start:

            string ip = "127.0.0.1";
            int port = 8080;

            ServerLog.New();

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://" + ip + ":" + port + "/");
            ServerLog.Created(ip, port);

            listener.Start();
            ServerLog.Started(ip, port);

            Task.Run(() =>
            {
                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;

                    string ResponseString = "";
                    if (request.HttpMethod == "GET")
                    {
                        ResponseString = "GET request received";
                        ServerLog.Request(request.Headers.ToString());
                        ServerLog.Response(ResponseString);
                    }
                    else if (request.HttpMethod == "POST")
                    {
                        byte[] data;
                        using (var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                        }
                        ResponseString = Encoding.UTF8.GetString(data);
                        ServerLog.Request(request.Headers.ToString());
                        ServerLog.Response(ResponseString);
                    }
                    else
                    {
                        ResponseString = "Unsupported method: " + request.HttpMethod;
                        ServerLog.Request(request.Headers.ToString());
                        ServerLog.Error(ResponseString);
                    }

                    byte[] buffer = Encoding.UTF8.GetBytes(ResponseString);
                    response.ContentType = "text/html";
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.Close();
                }
            });

            ServerLog.Break();

            listener.Stop();
            ServerLog.Stopped();
        }
    }
}
