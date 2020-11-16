using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using OpenXmlPowerTools;
using FinalProject.Models;
using System.Drawing.Imaging;

namespace FinalProject.OpenXML
{
    public class ReportDisplay : IReportDisplay
    {
        public XElement ConvertToHTML(byte[] bytes)
        {
            WmlDocument wordDoc = new WmlDocument("wmldoc", bytes);
            int imageCounter = 0;

            // These settings borrowed from https://raw.githubusercontent.com/EricWhiteDev/Open-Xml-PowerTools/vNext/OpenXmlPowerToolsExamples/WmlToHtmlConverter02/WmlToHtmlConverter02.cs
            WmlToHtmlConverterSettings settings = new WmlToHtmlConverterSettings()
            {
                AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; }",
                PageTitle = "Display Report",
                FabricateCssClasses = true,
                CssClassPrefix = "pt-",
                RestrictToSupportedLanguages = false,
                RestrictToSupportedNumberingFormats = false,
                // This whole piece below is to handle images in the report
                ImageHandler = imageInfo =>
                {
                    ++imageCounter;
                    string extension = imageInfo.ContentType.Split('/')[1].ToLower();
                    ImageFormat imageFormat = null;
                    if (extension == "png")
                        imageFormat = ImageFormat.Png;
                    else if (extension == "gif")
                        imageFormat = ImageFormat.Gif;
                    else if (extension == "bmp")
                        imageFormat = ImageFormat.Bmp;
                    else if (extension == "jpeg")
                        imageFormat = ImageFormat.Jpeg;
                    else if (extension == "tiff")
                    {
                        // Convert tiff to gif.
                        extension = "gif";
                        imageFormat = ImageFormat.Gif;
                    }
                    else if (extension == "x-wmf")
                    {
                        extension = "wmf";
                        imageFormat = ImageFormat.Wmf;
                    }

                    // If the image format isn't one that we expect, ignore it,
                    // and don't return markup for the link.
                    if (imageFormat == null)
                        return null;

                    string base64 = null;
                    try
                    {
                        using MemoryStream ms = new MemoryStream();
                        imageInfo.Bitmap.Save(ms, imageFormat);
                        var ba = ms.ToArray();
                        base64 = System.Convert.ToBase64String(ba);
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return null;
                    }

                    ImageFormat format = imageInfo.Bitmap.RawFormat;
                    ImageCodecInfo codec = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == format.Guid);
                    string mimeType = codec.MimeType;

                    string imageSource = string.Format("data:{0};base64,{1}", mimeType, base64);

                    XElement img = new XElement(Xhtml.img,
                        new XAttribute(NoNamespace.src, imageSource),
                        imageInfo.ImgStyleAttribute,
                        imageInfo.AltText != null ?
                            new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
                    return img;
                }
            };

            XElement html = WmlToHtmlConverter.ConvertToHtml(wordDoc, settings);

            return html;
        }
    }
}
