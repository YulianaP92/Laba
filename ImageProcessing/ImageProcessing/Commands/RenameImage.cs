using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Drawing.Imaging;

namespace ImageProcessing.Commands
{
    class RenameImage : CommonTeams
    {
        public override string newPath { get; set; }
        public override string path { get; set; }
        public string dateTaken;
        public override void NewFolder()
        {
            Console.WriteLine("Enter the full path to the folder with images");
            path = Console.ReadLine();
            newPath = $"{path}_RENAME";
            base.NewFolder();
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"Name: {files[i].Name}\nFull Name: {files[i].FullName}\nLenght: {files[i].Length}\n");
                using (FileStream fs = new FileStream(files[i].FullName, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs))
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867); //PropertyTagExifDTOrig 0x9003
                    dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    if (dateTaken == null)
                        dateTaken = files[i].CreationTime.ToShortDateString();
                    else
                    {
                        DateTime date = DateTime.Parse(dateTaken);
                        dateTaken = date.ToShortDateString();
                    }
                }
                files[i].CopyTo(Path.Combine(newPath, $"{files[i].Name}_{dateTaken}.JPG"),true);
            }
        }
    }
}
