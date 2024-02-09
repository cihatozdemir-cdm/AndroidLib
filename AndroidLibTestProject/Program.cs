using RegawMOD.Android;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var androidController = AndroidController.Instance;
            Console.WriteLine("Android Lib Started");

            while (!androidController.HasConnectedDevices)
            {
                Thread.Sleep(200);
            }

            Console.WriteLine("Device Connected");
            Console.WriteLine(AndroidController.Instance.GetConnectedDevice().SerialNumber);


            AndroidController.Instance.Dispose();
        }
    }
}