using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace PhotoGalery.Mobile.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .Debug()
                    .InstalledApp("com.companyname.photogalery.mobile")
                    //.ApkFile(@"com.companyname.photogalery.mobile-Signed.apk")
                    //.DeviceSerial("emulator-5554")
                    .StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}