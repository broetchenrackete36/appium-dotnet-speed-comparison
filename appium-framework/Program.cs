using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Diagnostics;
using System.Threading;

namespace appium_framework
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine("Starting to measure speed...");

            DriverOptions driverOptions = new AppiumOptions();
            driverOptions.AddAdditionalCapability("platformName", "android");
            driverOptions.AddAdditionalCapability("platformVersion", "10");
            driverOptions.AddAdditionalCapability("automationName", "Appium");
            driverOptions.AddAdditionalCapability("deviceName", "Android Emulator");
            driverOptions.AddAdditionalCapability("unlockType", "pin");
            driverOptions.AddAdditionalCapability("unlockKey", "0000");
            driverOptions.AddAdditionalCapability("appPackage", "com.android.settings");
            driverOptions.AddAdditionalCapability("appActivity", ".Settings");
            driverOptions.AddAdditionalCapability("autoGrantPermissions", "true");
            driverOptions.AddAdditionalCapability("noSign", "true");

            Console.WriteLine("Starting Android-Driver!");
            double driverStartTime = watch.ElapsedMilliseconds;
            var androidDriver = new AndroidDriver<AppiumWebElement>(new Uri("http://localhost:4723/wd/hub"), driverOptions, TimeSpan.FromMinutes(5));
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds - driverStartTime}> ms to start the driver...");
            Console.WriteLine("Getting UDID...");
            double udidStartTime = watch.ElapsedMilliseconds;
            string androidUdid = androidDriver.Capabilities.GetCapability("deviceUDID").ToString().ToLower();
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds - udidStartTime}> ms to get the udid <{androidUdid}>...");

            watch.Stop();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            watch.Start();

            Console.WriteLine("Getting all Elements as a collection");
            double collectionStartTime = watch.ElapsedMilliseconds;
            var coll = androidDriver.FindElementsByXPath("//*");
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds - collectionStartTime}> ms to get the collection of elements...");

            Console.WriteLine("Getting all heights of the elements");
            double heightStartTime = watch.ElapsedMilliseconds;
            foreach (AppiumWebElement item in coll)
            {
                double elementHeightStartTime = watch.ElapsedMilliseconds;
                var height = item.Size.Height;
                Console.WriteLine($"It took <{watch.ElapsedMilliseconds - elementHeightStartTime}> ms to get the following height <{height}> for item with id <{item.Id}>");
            }
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds - heightStartTime}> ms to get the heights of all elements...");

            Console.WriteLine("Stopping the driver...");
            double quitStartTime = watch.ElapsedMilliseconds;
            androidDriver.Quit();
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds - quitStartTime}> ms to stop the driver...");

            watch.Stop();
            Console.WriteLine($"It took <{watch.ElapsedMilliseconds}> ms to complete the run...");
            Console.WriteLine("Enter any key to close the application...");
            Console.Read();
        }
    }
}