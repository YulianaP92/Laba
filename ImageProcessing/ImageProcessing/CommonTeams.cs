using System.IO;
using System.Text.RegularExpressions;

namespace ImageProcessing
{
    public class CommonTeams
    {
        public DirectoryInfo directory;
        public DirectoryInfo newdirectory;
        public static Regex r = new Regex(":");
        public FileInfo[] files;
        public virtual string newPath { get; set; }
        public virtual string path { get; set; }//"D:\Photo"
        public virtual void NewFolder()
        {
            directory = new DirectoryInfo(path);

            if (directory.Exists)
            {
                newdirectory = new DirectoryInfo(newPath);
                newdirectory.Create();
            }
            files = directory.GetFiles("*.JPG");           
        }
    }
}