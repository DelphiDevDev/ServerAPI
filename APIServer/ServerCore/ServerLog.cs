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
            Log.Note("API server created as Server on {0}:{1}", ip, port);
            Log.Warning("API server starting...");
        }

        public static void Started(string ip, int port)
        {
            Log.SetCursorToPreviousLine();
            Log.ClearLine();
            Log.Warning("API server on {0}:{1} started", ip, port);
            Log.Warning("API server works...");
        }

        public static void Request(string Text)
        {
            Log.Success("API server received request from Client: {0}", Text);
            Log.Line();
        }

        public static void Response(string Text)
        {
            Log.Success("API server is sending responce to Client: {0}", Text);
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