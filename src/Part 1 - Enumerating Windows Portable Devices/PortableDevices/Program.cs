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
                //Console.WriteLine(device.DeviceId);

                device.Connect();
                Console.WriteLine(device.FriendlyName);
                device.Disconnect();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
    }
}
