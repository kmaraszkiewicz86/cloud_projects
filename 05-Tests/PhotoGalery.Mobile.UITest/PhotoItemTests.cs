using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace PhotoGalery.Mobile.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class PhotoItemTests
    {
        IApp app;
        Platform platform;

        public PhotoItemTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void TestAddingNewItemAndRemoveIt()
        {
            Guid guid = Guid.NewGuid();
            var itemName = $"Test{guid}";

            app.WaitForElement(GetNewItemNameAppQuery);

            app.EnterText(GetNewItemNameAppQuery, itemName);
            app.Tap(c => c.Marked("AddNewItemButton"));

            WaitForNoActivityIndicatorElement();

            CheckIfCreatedElementExists(itemName);
            
            app.SwipeRightToLeft(c => GetNewCollectionViewItemNameAppQuery(c, itemName), 0.5, 500, false);

            app.Tap(c => GetNewCollectionViewItemNameAppQuery(c, itemName).Parent().Child().Button().Text("Delete"));

            WaitForNoActivityIndicatorElement();
            app.WaitForNoElement(c => GetNewCollectionViewItemNameAppQuery(c, itemName), timeout: TimeSpan.FromSeconds(1));
        }

        private AppQuery GetNewItemNameAppQuery(AppQuery appQuery)
            => appQuery.Marked("NewItemName");

        private AppQuery GetNewCollectionViewItemNameAppQuery(AppQuery appQuery, string text)
            => appQuery.All().Text(text);

        private void WaitForNoActivityIndicatorElement()
        {
            app.WaitForNoElement(c => c.Marked("ActivityIndicator"));
        }

        private void CheckIfCreatedElementExists(string itemName)
        {
            var createdElement = app.WaitForElement(c => GetNewCollectionViewItemNameAppQuery(c, itemName),
                timeout: TimeSpan.FromSeconds(1));

            Assert.AreEqual(createdElement.Count(), 1);
        }
    }
}