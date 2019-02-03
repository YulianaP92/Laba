using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace ImageProcessing.Commands
{
    class SortingByLocation : CommonTeams
    {
        public override string newPath { get; set; }
        public override string path { get; set; }
        public DirectoryInfo subDirectory;
        public override void NewFolder()
        {
            Console.WriteLine("Enter the full path to the folder with images");
            path = Console.ReadLine();
            newPath = $"{path}_SORTING_LOCATION";
            base.NewFolder();
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"Name: {files[i].Name}\nFull Name: {files[i].FullName}\nLenght: {files[i].Length}\n");

                var gps = ImageMetadataReader.ReadMetadata(files[i].FullName)
                                            .OfType<GpsDirectory>()
                                            .FirstOrDefault();
                if (gps != null)
                {
                    var location = gps.GetGeoLocation();
                    if (location == null)
                    {
                        continue;
                    }
                    var latitude = location.Latitude.ToString();
                    var longitude = location.Longitude.ToString();
                    latitude = latitude.Replace(',', '.');
                    longitude = longitude.Replace(',', '.');
                    string requestUri = $"https://geocode-maps.yandex.ru/1.x/?geocode={longitude},{latitude}";
                    WebRequest request = WebRequest.Create(requestUri);
                    WebResponse response = request.GetResponse();
                    var xdoc = XDocument.Load(response.GetResponseStream());
                    var result = GetSpecialElement(xdoc.Elements()?.ToList(), "CountryName", out var flag);
                    if (result != null)
                    {
                        subDirectory = newdirectory.CreateSubdirectory(result);
                        files[i].CopyTo(Path.Combine(subDirectory.FullName, $"{files[i].Name}.JPG"), true);
                    }
                }
            }
        }
        private string GetSpecialElement(List<XElement> element, string name, out bool flag)
        {
            if (element.Count.Equals(0))
            {
                flag = false;
                return null;
            };
            string result = null;
            flag = true;

            while (flag && result == null)
            {
                var nextElement = element.Elements();
                result = element.FirstOrDefault(x => x.Name.LocalName.Equals(name))?.Value;
                if (result == null)
                {
                    result = GetSpecialElement(nextElement.ToList(), name, out flag);
                }
            }
            flag = false;
            return result;
        }
    }
}
