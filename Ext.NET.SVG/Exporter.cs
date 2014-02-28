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
        public static void SaveToStream(string svg, Stream stream, ImageFormat format)
        {
            XmlDocument xd = new XmlDocument();
            xd.XmlResolver = null;
            xd.LoadXml(svg);
            SvgDocument svgGraph = SvgDocument.Open(xd);
            Bitmap originalImage = svgGraph.Draw();

            if (format == ImageFormat.Jpeg)
            {
                Bitmap b = new Bitmap(originalImage.Width, originalImage.Height);
                b.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(originalImage, 0, 0);
                }

                originalImage = b;
            }

            using (originalImage)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    originalImage.Save(ms, format);
                    ms.WriteTo(stream);
                }
            }
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
