using System;

namespace PortableDevices
{
    class Program
    {
        static void Main()
        {
            var collection = new PortableDeviceCollection();

            collection.Refresh();

            foreach(var device in collection)
            {
                device.Connect();
                Console.WriteLine(device.FriendlyName);

                var folder = device.GetContents();
                foreach(var item in folder.Files)
                {
                    DisplayObject(device, item);                  
                }

                device.Disconnect();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        public static void DisplayObject(PortableDevice device, PortableDeviceObject portableDeviceObject)
        {
            Console.WriteLine(portableDeviceObject.Name);
            if (portableDeviceObject is PortableDeviceFolder)
            {
                DisplayFolderContents(device, (PortableDeviceFolder) portableDeviceObject);
            }
        }

        public static void DisplayFolderContents(PortableDevice device, PortableDeviceFolder folder)
        {
            foreach (var item in folder.Files)
            {
                Console.WriteLine(item.Id);

                if (item is PortableDeviceFolder)
                {
                    DisplayFolderContents(device, (PortableDeviceFolder) item);
                }
                if (item is PortableDeviceFile)
                {
                    device.DownloadFile((PortableDeviceFile)item, @"c:\kindle\");
                }
            }
        }
    }
}
