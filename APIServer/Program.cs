using System;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClassLibraryAPI;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;

namespace ServerCore
{
    class Program
    {
        static void Main(string[] args)
        {
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
                    string urlPath = context.Request.Url.AbsolutePath;
                    string JSONString = "";

                    if (request.HttpMethod == "GET")
                    {
                        if (urlPath == "/list")
                        {
                            var listParams = context.Request.QueryString;
                            if (listParams != null)
                            {
                                string fromid = listParams["fromid"] ?? ""; 
                                string toid = listParams["toid"] ?? "";
                                if (fromid != "" && toid != "") {
                                    ServerLog.RequestGETlist(fromid, toid);
                                    ResponseString = JSONUtils.GETResponseJSON(fromid, toid);
                                }
                                else
                                {
                                    ResponseString = JSONUtils.BadRequestJSON(1);
                                }
                            }
                            else
                            {
                                ResponseString = JSONUtils.BadRequestJSON(1);
                            }
                        }
                        else
                        {
                            ResponseString = JSONUtils.BadRequestJSON(2);
                        }
                        ServerLog.RequestGET(request.Headers.ToString());
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
                        ServerLog.RequestPOST(request.Headers.ToString());
                        ServerLog.Response(ResponseString);
                    }
                    else
                    {
                        ResponseString = JSONUtils.BadRequestJSON(3) + request.HttpMethod;
                        ServerLog.UnknownRequest(request.Headers.ToString());
                        ServerLog.Error(ResponseString);
                    }

                    response.ContentType = "application/json";
                    byte[] buffer = Encoding.UTF8.GetBytes(ResponseString);
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
