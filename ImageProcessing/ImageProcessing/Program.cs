using System;
using ImageProcessing.Commands;

namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteCommand();
            Console.ReadKey();
        }
        public static void ExecuteCommand()
        {
            Console.WriteLine("Which of the following commands do you want to execute?:" +
              "\na-Rename the image according to the date of shooting" +
              "\nb-Adding to the image is a mark when the photo was taken" +
              "\nc-Sort images by folders by year." +
              "\nd-Sort images by folder by location");
            var variant = Console.ReadLine();
            
            switch (variant)
            {
                case "a":
                    CommonTeams renameImage = new RenameImage();
                    renameImage.NewFolder();
                    break;
                case "b":
                    CommonTeams addMark = new AddMark();
                    addMark.NewFolder();
                    break;
                case "c":
                    CommonTeams sortYear = new SortByYear();
                    sortYear.NewFolder();
                    break;
                case "d":
                    CommonTeams sortLocation = new SortingByLocation();
                    sortLocation.NewFolder();
                    break;
                default:
                    Console.WriteLine("Ñhoose the right command");
                    break;
            }
        }
    }
}
