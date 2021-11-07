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

            kindle.TransferContentToDevice(
                @"d:\temp\Kindle_Users_Guide.azw",
                @"g:\documents");

            kindle.Disconnect();

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
