using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    static class CreatePDF
    {
        static Document document = new Document();
        static Dictionary<char, Image> images = new Dictionary<char, Image>();
        static string alph = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        static CreatePDF()
        {
            foreach (char ch1 in alph)
            {
                images.Add(ch1, Image.GetInstance(new FileStream(@$"lets\{ch1}.jpg", FileMode.Open)));
            }
        }
        public static void Start(string mass)
        {
            char[] chars = mass.ToCharArray();
            using (var writer = PdfWriter.GetInstance(document, new FileStream("result.pdf", FileMode.Create)))
            {
                document.Open();
                int coordsX = 50;
                int coordsY = 750;
                // do some work here
                foreach (char ch in chars)
                {
                    char ch1 = Convert.ToChar(ch.ToString().ToUpper());
                    if (!alph.Contains(ch1))
                    {
                        coordsX += 50; 
                        if (coordsX >= 550)
                        {
                            coordsY -= 60;
                            coordsX = 50;
                        }
                        continue;
                    }
                    Image logo = images.GetValueOrDefault(ch1);//iTextSharp.text.Image.GetInstance(new FileStream(@$"lets\{ch1}.jpg", FileMode.Open));
                    logo.SetAbsolutePosition(coordsX, coordsY);
                    logo.ScaleAbsolute(50, 50);
                    writer.DirectContent.AddImage(logo);
                    coordsX += 50;
                    if (coordsX >= 550)
                    {
                        coordsY -= 60;
                        coordsX = 50;
                    }
                }
                

                document.Close();
                writer.Close();
            }
        }
    }
}
