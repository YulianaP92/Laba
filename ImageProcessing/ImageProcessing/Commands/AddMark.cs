using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ImageProcessing.Commands
{
    class AddMark : CommonTeams
    {
        public override string newPath { get; set; }
        public override string path { get; set; }
        public override void NewFolder()
        {
            Console.WriteLine("Enter the full path to the folder with images");
            path = Console.ReadLine();
            newPath = $"{path}_ADD_MARK";
            base.NewFolder();
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"Name: {files[i].Name}\nFull Name: {files[i].FullName}\nLenght: {files[i].Length}\n");
                using (FileStream fs = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867); //PropertyTagExifDTOrig 0x9003
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    Graphics drawing = Graphics.FromImage(myImage);
                    RectangleF drawRect = new RectangleF(myImage.Width - 440, 0, 0, 0);
                    drawing.DrawString(dateTaken, new Font("Arial", 10), Brushes.Blue, drawRect);
                    myImage.Save(Path.Combine(newPath, $"{files[i].Name}"));
                }
            }
        }
    }
}
