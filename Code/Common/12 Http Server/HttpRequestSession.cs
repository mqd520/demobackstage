using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HttpRequestSession
    {
        protected HttpServerBase server;
        protected HttpListenerContext ctx;


        public HttpRequestSession(HttpServerBase server, HttpListenerContext ctx)
        {
            this.server = server;
            this.ctx = ctx;

            server.ProcessRequest += Server_ProcessRequest;
        }

        private void Server_ProcessRequest()
        {
            OnProcessRequest();
        }

        protected virtual void OnProcessRequest()
        {

        }

        protected virtual void Response(int code, byte[] buf)
        {
            ctx.Response.StatusCode = code;
            ctx.Response.ContentLength64 = buf.Length;
            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
            //ctx.Response.OutputStream.Close();
            //ctx.Response.Close();
        }

        protected virtual void Response(int code, string text)
        {
            byte[] buf = Encoding.UTF8.GetBytes(text);
            ctx.Response.StatusCode = code;
            ctx.Response.ContentLength64 = buf.Length;
            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
            ctx.Response.OutputStream.Close();
            //ctx.Response.Close();
        }
    }
}
