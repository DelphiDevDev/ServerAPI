using ClassLibraryAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ServerCore
{
    public static class ServerLog
    {
        public static void New()
        {
            Log.New("Server");
            Log.Header("API Server");
        }

        public static void Created(string ip, int port)
        {
            Log.Note("API Server created as Server on {0}:{1}", ip, port);
            Log.Warning("API Server starting...");
        }

        public static void Started(string ip, int port)
        {
            Log.SetCursorToPreviousLine();
            Log.ClearLine();
            Log.Warning("API Server on {0}:{1} started", ip, port);
            Log.Warning("API Server is working now...");
        }

        public static void RequestGET(string Text)
        {
            Log.Success("GET request data: {0}", Text);
            Log.Line();
        }

        public static void RequestGETlist(string fromid, string toid)
        {
            Log.Success("GET list request with parameters: fromid={0}, toid={1}", fromid, toid);
            Log.Line();
        }

        public static void RequestPOST(string Text)
        {
            Log.Success("API Server received POST request from Client: {0}", Text);
            Log.Line();
        }

        public static void UnknownRequest(string Text)
        {
            Log.Error("API Server received unknown request from Client: {0}", Text);
            Log.Line();
        }

        public static void Response(string Text)
        {
            Log.Success("API Server is sending response to Client: {0}", Text);
            Log.Line();
        }

        public static void Success(string Text)
        {
            Log.Success("Success reading file: {0}", Text);
            Log.Line();
        }

        public static void Break()
        {
            Log.Warning("Press Enter to stop API Server");
            Log.Line();
            Log.ReadLine();
        }

        public static void Stopped()
        {
            Log.Warning("API Server stopped");
        }

        public static void Error(string Text)
        {
            Log.Error(Text);
        }
    }

}