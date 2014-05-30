using System;
using System.Linq;

namespace PortableDevices
{
    class Program
    {
        static void Main()
        {
            var devices = new PortableDeviceCollection();
            devices.Refresh();
            var kindle = devices.First();
            kindle.Connect();

            var root = kindle.GetContents();
            foreach (var resource in root.Files)
            {
                DisplayResourceContents(resource);
            }

            PortableDeviceFolder documents = 
                (from r in ((PortableDeviceFolder)root.Files[0]).Files
                 where r.Name.Contains("documents")
                                              select r).First() as PortableDeviceFolder;

            var books = from b in documents.Files
                        where b.Name.Contains("Kindle_Users_Guide")
                        select b;

            foreach (PortableDeviceFile book in books)
            {
                Console.WriteLine(book.Id);
                kindle.DeleteFile(book);
            }

            kindle.Disconnect();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void DisplayResourceContents(PortableDeviceObject portableDeviceObject)
        {
            Console.WriteLine(portableDeviceObject.Name);
            if (portableDeviceObject is PortableDeviceFolder)
            {
                DisplayFolderContents((PortableDeviceFolder) portableDeviceObject);
            }
        }

        public static void DisplayFolderContents(PortableDeviceFolder folder)
        {
            foreach (var item in folder.Files)
            {
                Console.WriteLine(item.Id);

                if (item is PortableDeviceFolder)
                {
                    DisplayFolderContents((PortableDeviceFolder) item);
                }
            }
        }
    }
}
