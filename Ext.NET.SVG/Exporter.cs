using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using Svg;
using System.IO;

namespace Ext.NET.SVG
{
    /// <summary>
    /// The class to export an SVG string to images and save to a Stream as images.
    /// </summary>
    public static class Exporter
    {
        public static Bitmap ToBitmap(string svg) 
        {
            XmlDocument xd = new XmlDocument();
            xd.XmlResolver = null;
            xd.LoadXml(svg);
            SvgDocument svgGraph = SvgDocument.Open(xd);
            Bitmap image = svgGraph.Draw();

            return image;
        }

        public static void SaveToStream(string svg, Stream stream, ImageFormat format)
        {
            Bitmap image = Exporter.ToBitmap(svg);
            image.Save(stream, format);
        }

        public static void SaveToStreamAsPng(string svg, Stream stream)
        {
            Exporter.SaveToStream(svg, stream, ImageFormat.Png);
        }

        public static void SaveToStreamAsJpeg(string svg, Stream stream)
        {
            Exporter.SaveToStream(svg, stream, ImageFormat.Jpeg);
        }
    }
}
