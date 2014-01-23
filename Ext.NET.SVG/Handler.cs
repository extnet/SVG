using System;
using System.Web;
using System.Xml;

namespace Ext.NET.SVG
{
    /// <summary>
    /// The Handler takes an SVG string and returns an image file.
    /// </summary>
    public class Handler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string svg = context.Request["svg"],
                   type = context.Request["type"],
                   cd = null, // Content-Disposition"
                   ct = null; // ContentType

            context.Response.Clear();

            try
            {
                if (type == "image/png")
                {
                    Exporter.SaveToStreamAsPng(svg, context.Response.OutputStream);
                    cd = "attachment; filename=chart.png";
                    ct = "image/png";
                }
                else if (type == "image/jpeg")
                {
                    Exporter.SaveToStreamAsJpeg(svg, context.Response.OutputStream);
                    cd = "attachment; filename=chart.jpeg";
                    ct = "image/jpeg";
                }
                else
                {
                    throw new NotSupportedException(string.Format("The \"{0}\" type is not supported. The supported types are \"image/png\" and \"image/jpeg\".", type));
                }

                context.Response.ClearHeaders();
                context.Response.AddHeader("Content-Disposition", cd);
                context.Response.ContentType = ct;
            }
            catch (XmlException e)
            {
                context.Response.Redirect(string.Format("Help.aspx?errorMessage=An XmlException occured while parsing the submitted SVG string: \"{0}\".", e.Message));
            }
            catch (NotSupportedException e)
            {
                context.Response.Redirect(string.Format("Help.aspx?errorMessage={0}", e.Message));
            }
            finally
            {
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get 
            {
                return true;
            }
        }
    }
}
