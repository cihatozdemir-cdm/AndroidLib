using RegawMOD.Android;

namespace MyApp
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

            AdbCommand adbCmd = Adb.FormAdbCommand("logcat");
            Adb.ExecuteAdbCommand(adbCmd);

            return;
            using (StringReader r = new StringReader(Adb.ExecuteAdbCommand(adbCmd)))
            {
                var output = r.ReadToEnd();
                Console.WriteLine(output);
            }


            AndroidController.Instance.Dispose();
        }
    }
}