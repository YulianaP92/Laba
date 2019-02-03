using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ImageProcessing.Commands
{
    class SortByYear : CommonTeams
    {
        public DirectoryInfo subDirectory;
        public string dateTaken;
        public override string newPath { get; set; }
        public override string path { get; set; }
        public override void NewFolder()
        {
            Console.WriteLine("Enter the full path to the folder with images");
            path = Console.ReadLine();
            newPath = $"{path}_SORT_YEAR";
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
                    {
                        dateTaken = files[i].CreationTime.ToShortDateString();
                        DateImage(dateTaken, files[i]);
                    }
                    else
                    {
                        DateImage(dateTaken, files[i]);
                    }
                }
            }
        }
        public void DateImage(string dateTaken, FileInfo files)
        {
            DateTime date = DateTime.Parse(dateTaken);
            dateTaken = date.ToShortDateString();
            if (subDirectory == null)
            {
                subDirectory = newdirectory.CreateSubdirectory($"{date.Year}");
            }
            if (subDirectory.Exists)
            {
                files.CopyTo(Path.Combine(subDirectory.FullName, $"{files.Name}.JPG"), true);
            }
        }
    }
}

