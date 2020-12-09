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
using System.Text;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;

namespace FinalProject.OpenXML
{
    public class ReportOps : IReportOps
    {
        public XElement ConvertToHTML(byte[] bytes)
        {
            WmlDocument wordDoc = new WmlDocument("wmldoc", bytes);
            int imageCounter = 0;

            // These settings borrowed from https://raw.githubusercontent.com/EricWhiteDev/Open-Xml-PowerTools/vNext/OpenXmlPowerToolsExamples/WmlToHtmlConverter02/WmlToHtmlConverter02.cs
            WmlToHtmlConverterSettings settings = new WmlToHtmlConverterSettings()
            {
                // Tweaked the CSS to fix some things, specifically the padding-top and overflow-wrap settings
                AdditionalCss = "body { margin: 1cm auto; max-width: 20cm; padding: 0; padding-top: 70px; overflow-wrap: anywhere}",
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

        public Dictionary<string, string> CreateSearchIndex(byte[] bytes)
        {
            // Create a list of strings to store headings and report content
            List<string> searchIndex = new List<string>();
            List<string> headings = new List<string>();

            // Create a dictionary so we have one value to return
            Dictionary<string, string> returnValues = new Dictionary<string, string>();

            // Read bytes to MemoryStream, so the OpenXML SDK can work with it directly
            using MemoryStream mem = new MemoryStream();
            mem.Write(bytes, 0, (int)bytes.Length);

            // Open as a WordprocessingDocument so we can traverse document elements
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(mem, true))
                {
                    // Get the main portions of the doc to work with, as this is where the content we're interested in is stored
                    MainDocumentPart mainDoc = wordDoc.MainDocumentPart;
                    Body bodyDoc = mainDoc.Document.Body;

                    // Pull all text from each Paragraph element.  This is where most content will be stored
                    IEnumerable<Paragraph> paragraphs = bodyDoc.Descendants<Paragraph>();
                    foreach (Paragraph p in paragraphs)
                    {
                        // Look in the Paragraph Style portion for headings so we can show these to the user without opening each report
                        ParagraphStyleId style = p.Descendants<ParagraphStyleId>().FirstOrDefault();
                        if (style != null)
                        {
                            if (style.Val.Value.Contains("Heading") && style.Val.Value != "TableHeading")
                            {
                                StringBuilder headingValue = new StringBuilder();
                                IEnumerable<Text> headingPortion = p.Descendants<Text>();
                                foreach (Text t in headingPortion)
                                {
                                    if (!string.IsNullOrEmpty(t.Text))
                                    {
                                        headingValue.Append(t.Text);
                                    }
                                }


                                if (!string.IsNullOrEmpty(headingValue.ToString()))
                                {
                                    // This section adds space indents for each level of heading, so it shows as a nice visual
                                    // heirarchical view in the tool tip
                                    if (style.Val.Value == "Heading2")
                                    {
                                        headingValue.Insert(0, "    ");
                                    }
                                    else if (style.Val.Value == "Heading3")
                                    {
                                        headingValue.Insert(0, "        ");
                                    }
                                    else if (style.Val.Value == "Heading4")
                                    {
                                        headingValue.Insert(0, "                ");
                                    }

                                    headings.Add(headingValue.ToString());
                                }
                            }
                        }
                        StringBuilder stringBuilder = new StringBuilder();
                        IEnumerable<Text> texts = p.Descendants<Text>();
                        foreach (Text t in texts)
                        {
                            if (!string.IsNullOrEmpty(t.Text))
                            {
                                stringBuilder.Append(t.Text.ToLower());
                            }
                        }
                        searchIndex.Add(stringBuilder.ToString());
                    }

                    // Pull all text from within table elements
                    IEnumerable<Table> tables = bodyDoc.Descendants<Table>();
                    foreach (Table table in tables)
                    {
                        IEnumerable<Paragraph> tableparagraphs = table.Descendants<Paragraph>();
                        foreach (Paragraph p in tableparagraphs)
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            IEnumerable<Text> texts = p.Descendants<Text>();
                            foreach (Text t in texts)
                            {
                                if (!string.IsNullOrEmpty(t.Text))
                                {
                                    stringBuilder.Append(t.Text.ToLower());
                                }
                            }
                            searchIndex.Add(stringBuilder.ToString());
                        }
                    }
                }
                returnValues.Add("headings", string.Join(Environment.NewLine, headings));
                returnValues.Add("content", string.Join(" ", searchIndex));

                return returnValues;

            }
            catch (UriFormatException)  // For rare cases where a hyperlink within the doc is malformed
            {

                throw;
            }
            
        }
    }
}
