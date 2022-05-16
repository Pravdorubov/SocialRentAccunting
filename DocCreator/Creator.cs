using SocialRentAccunting.Models;
using SocialRentAccunting.ViewModels;
using System.Text.RegularExpressions;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace SocialRentAccunting.DocCreator
{
    public class DocumentCreator
    {
        private const string DocumentSampleResourcesDirectory = @"C:\Documents\Resources\";
        private const string DocumentSampleOutputDirectory = @"C:\Documents\Output\";

        private static string pathToTemplate;

        public static void SetPathToTemplate(string path)
        {
            pathToTemplate = path;
        }

        static DocumentCreator()
        {
            if (!Directory.Exists(DocumentCreator.DocumentSampleOutputDirectory))
            {
                Directory.CreateDirectory(DocumentCreator.DocumentSampleOutputDirectory);
            }
        }


        /// <summary>
        /// Load a document and replace texts following a replace pattern.
        /// </summary>
        public static string CreateFromFile(Contract contract, List<Kinship> kinships)
        {
            List<string> kinsmen = contract.Tenant.Kinsmen.Select(k =>
                   $"{k.FullName} {kinships.FirstOrDefault(s => s.Id == k.KinshipId)?.Name ?? string.Empty}"
           ).ToList();

            // Load a document.
            using (var document = DocX.Load(pathToTemplate + @"\template.docx"))
            {
                document.ReplaceText("[C_Num]", contract.Number);
                document.ReplaceText("[C_Date]", contract.DateStart.ToShortDateString());
                document.ReplaceText("[L_Name]", contract.Landlord.Name);
                document.ReplaceText("[L_Head]", contract.Landlord.Head);
                document.ReplaceText("[T_FullName]", contract.Tenant.FullName);
                document.ReplaceText("[O_Num]", contract.Order.Number);
                document.ReplaceText("[O_Date]", contract.Order.Date.ToShortDateString());
                document.ReplaceText("[H_Flats]", contract.House.FlatsCount.ToString());
                document.ReplaceText("[H_Area]", contract.House.CommonArea.ToString());
                document.ReplaceText("[H_Address]", contract.House.Address);
                document.ReplaceText("[T_Kinsmen]", string.Join(",", kinsmen));

                string name = pathToTemplate + @$"\contract_{contract.Number}_{contract.DateStart.Year}.docx";
                    // Save this document to disk.
                document.SaveAs(name);
                return name;
            }
        }

        /*public static Stream CreateFromStream(Contract contract, List<Kinship> kinships)
        {
            List<string> kinsmen = contract.Tenant.Kinsmen.Select(k =>
                   $"{k.FullName} {kinships.FirstOrDefault(s => s.Id == k.KinshipId)?.Name ?? string.Empty}"
           ).ToList();

            // Load a document.
            using (var document = DocX.Load(pathToTemplate + @"\template.docx"))
            {
                document.ReplaceText("[C_Num]", contract.Number);
                document.ReplaceText("[C_Date]", contract.DateStart.ToShortDateString());
                document.ReplaceText("[L_Name]", contract.Landlord.Name);
                document.ReplaceText("[L_Head]", contract.Landlord.Head);
                document.ReplaceText("[T_FullName]", contract.Tenant.FullName);
                document.ReplaceText("[O_Num]", contract.Order.Number);
                document.ReplaceText("[O_Date]", contract.Order.Date.ToShortDateString());
                document.ReplaceText("[H_Flats]", contract.House.FlatsCount.ToString());
                document.ReplaceText("[H_Area]", contract.House.CommonArea.ToString());
                document.ReplaceText("[H_Address]", contract.House.Address);
                document.ReplaceText("[T_Kinsmen]", string.Join(",", kinsmen));

                Stream stream = new MemoryStream(document.);
                
                // Save this document to disk.
                document.SaveAs(DocumentCreator.DocumentSampleOutputDirectory + @$"contract_{contract.Number}_{contract.DateStart.Year}.docx");
            }
        }*/

        /// <summary>
        /// Load a document and replace texts with images.
        /// </summary>
        public static void ReplaceTextWithObjects()
        {
            Console.WriteLine("\tReplaceTextWithObjects()");

            // Load a document.
            using (var document = DocX.Load(DocumentCreator.DocumentSampleResourcesDirectory + @"ReplaceTextWithObjects.docx"))
            {
                // Create the image from disk and set its size.
                var image = document.AddImage(DocumentCreator.DocumentSampleResourcesDirectory + @"2018.jpg");
                var picture = image.CreatePicture(175f, 325f);

                // Do the replacement of all the found tags with the specified image and ignore the case when searching for the tags.
                document.ReplaceTextWithObject("<yEaR_IMAGE>", picture, false, RegexOptions.IgnoreCase);

                // Create the hyperlink.
                var hyperlink = document.AddHyperlink("(ref)", new Uri("https://en.wikipedia.org/wiki/New_Year"));
                // Do the replacement of all the found tags with the specified hyperlink.
                document.ReplaceTextWithObject("<year_link>", hyperlink);

                // Add a Table into the document and sets its values.
                var t = document.AddTable(1, 2);
                t.Design = TableDesign.DarkListAccent4;
                t.AutoFit = AutoFit.Window;
                t.Rows[0].Cells[0].Paragraphs[0].Append("xceed.com");
                t.Rows[0].Cells[1].Paragraphs[0].Append("@copyright 2021");
                document.ReplaceTextWithObject("<year_table>", t);

                // Save this document to disk.
                document.SaveAs(DocumentCreator.DocumentSampleOutputDirectory + @"ReplacedTextWithObjects.docx");
                Console.WriteLine("\tCreated: ReplacedTextWithObjects.docx\n");
            }
        }

        /// <summary>
        /// Add a template to a document.
        /// </summary>
        public static void ApplyTemplate()
        {
            Console.WriteLine("\tApplyTemplate()");

            // Create a new document.
            using (var document = DocX.Create(DocumentCreator.DocumentSampleOutputDirectory + @"ApplyTemplate.docx"))
            {
                // The path to a template document,
                var templatePath = DocumentCreator.DocumentSampleResourcesDirectory + @"Template.docx";

                document.DifferentOddAndEvenPages = true;

                // Apply a template to the document based on a path.
                document.ApplyTemplate(templatePath);

                // Add a paragraph at the end of the template.
                document.InsertParagraph("This paragraph is not part of the template.").FontSize(15d).UnderlineStyle(UnderlineStyle.singleLine).SpacingBefore(50d);

                // Save this document to disk.
                document.Save();
                Console.WriteLine("\tCreated: ApplyTemplate.docx\n");
            }
        }

        public static void LoadDocumentWithFilename()
        {
            using (var doc = DocX.Load(DocumentCreator.DocumentSampleResourcesDirectory + @"First.docx"))
            {
                // Add a title
                doc.InsertParagraph(0, "Load Document with File name", false).FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                // Insert a Paragraph into this document.
                var p = doc.InsertParagraph();

                // Append some text and add formatting.
                p.Append("A small paragraph was added.");

                doc.SaveAs(DocumentCreator.DocumentSampleOutputDirectory + @"LoadDocumentWithFilename.docx");
            }
        }

        public static void LoadDocumentWithStream()
        {
            using (var fs = new FileStream(DocumentCreator.DocumentSampleResourcesDirectory + @"First.docx", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var doc = DocX.Load(fs))
                {
                    // Add a title
                    doc.InsertParagraph(0, "Load Document with Stream", false).FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                    // Insert a Paragraph into this document.
                    var p = doc.InsertParagraph();

                    // Append some text and add formatting.
                    p.Append("A small paragraph was added.");

                    doc.SaveAs(DocumentCreator.DocumentSampleOutputDirectory + @"LoadDocumentWithStream.docx");
                }
            }
        }

        public static void LoadDocumentWithStringUrl()
        {
            using (var doc = DocX.Load("https://calibre-ebook.com/downloads/demos/demo.docx"))
            {
                // Add a title
                doc.InsertParagraph(0, "Load Document with string Url", false).FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                // Insert a Paragraph into this document.
                var p = doc.InsertParagraph();

                // Append some text and add formatting.
                p.Append("A small paragraph was added.");

                doc.SaveAs(DocumentCreator.DocumentSampleOutputDirectory + @"LoadDocumentWithUrl.docx");
            }
        }

    }
}
