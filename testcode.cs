using Microsoft.Playwright;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;

namespace SauceDemo_Tests
{
    [TestFixture]
    [AllureNUnit]
    public class SauceDemoTestSuite
    {
        private IBrowser browser;
        private IPage page;
        private IBrowserContext context;
        private IPlaywright playwright;
        private const string baseUrl = "https://www.saucedemo.com/v1/";

        [SetUp]
        public async Task Setup()
        {
            playwright = await Playwright.CreateAsync();
            var testName = TestContext.CurrentContext.Test.Name;

            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 1000
            });

            context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                RecordVideoDir = $"Test_recording/{testName}/",
                RecordVideoSize = new RecordVideoSize { Width = 1280, Height = 720 },
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
                RecordHarPath = $"Test_recording/{testName}.har"
            });

            page = await context.NewPageAsync();
            page.SetDefaultTimeout(60000); // 60 seconds default
            await page.GotoAsync(baseUrl);
        }

        private async Task Login(string username, string password)
        {
            await page.FillAsync("#user-name", username);
            await page.FillAsync("#password", password);
            await page.ClickAsync("#login-button");
        }

        private async Task TakeScreenshotAsync(string fileName)
        {
            var screenshotPath = $"Screenshots/{fileName}.jpeg";
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = screenshotPath,
                Type = ScreenshotType.Jpeg,
                Quality = 90
            });

            Console.WriteLine($"[INFO] Screenshot saved to: {screenshotPath}");
        }

        [Test]
        [AllureTag("TC-001")]
        [AllureOwner("Swadhin Patnaik")]
        [AllureDescription("Add 3 cheapest items, remove second cheapest, and verify cart/checkout details")]
        public async Task TC001_AddLeastExpensiveItems_And_Checkout()
        {
            await Login("standard_user", "secret_sauce");
            await page.SelectOptionAsync(".product_sort_container", "lohi");

            var items = await page.QuerySelectorAllAsync(".inventory_item");
            var prices = await Task.WhenAll(items.Select(async item => new
            {
                Element = item,
                Price = float.Parse((await item.QuerySelectorAsync(".inventory_item_price")).InnerTextAsync().Result.Replace("$", ""))
            }));

            var sortedItems = prices.OrderBy(p => p.Price).Take(3).ToList();
            foreach (var item in sortedItems)
                await (await item.Element.QuerySelectorAsync("button")).ClickAsync();

            await page.ClickAsync(".shopping_cart_link");
            await page.Locator(".cart_item").Nth(1).Locator("button").ClickAsync();

            await page.ClickAsync(".btn_action.checkout_button");
            await page.FillAsync("#first-name", "Swadhin");
            await page.FillAsync("#last-name", "Patnaik");
            await page.FillAsync("#postal-code", "123456");
            await page.ClickAsync(".btn_primary.cart_button");

            var totalText = await page.InnerTextAsync(".summary_total_label");
            if (!totalText.Contains("Total"))
                throw new Exception("Checkout total label missing.");

            await TakeScreenshotAsync("TC001_CheckoutOverview");

            await page.ClickAsync(".btn_action.cart_button");
            bool confirmationVisible = await page.IsVisibleAsync(".complete-header");
            if (!confirmationVisible)
                throw new Exception("Order confirmation header not visible.");

            await TakeScreenshotAsync("TC001_OrderConfirmation");
        }

        [Test]
        [AllureTag("TC-002")]
        [AllureOwner("Swadhin Patnaik")]
        [AllureDescription("Navigate to 3rd highest priced product and verify name & price match")]
        public async Task TC002_Verify_3rdHighest_StandardUser()
        {
            await Login("standard_user", "secret_sauce");
            await page.SelectOptionAsync(".product_sort_container", "hilo");

            var items = await page.QuerySelectorAllAsync(".inventory_item");
            var thirdItem = items[2];

            var expectedName = await (await thirdItem.QuerySelectorAsync(".inventory_item_name")).InnerTextAsync();
            var expectedPrice = await (await thirdItem.QuerySelectorAsync(".inventory_item_price")).InnerTextAsync();

            await (await thirdItem.QuerySelectorAsync(".inventory_item_name")).ClickAsync();

            var detailName = await page.InnerTextAsync(".inventory_details_name");
            var detailPrice = await page.InnerTextAsync(".inventory_details_price");

            if (expectedName != detailName)
                throw new Exception($"Product name mismatch. Expected: {expectedName}, Got: {detailName}");

            if (expectedPrice != detailPrice)
                throw new Exception($"Product price mismatch. Expected: {expectedPrice}, Got: {detailPrice}");

            await TakeScreenshotAsync("TC002_StandardUser_ProductDetail");
        }

        [Test]
        [AllureTag("TC-003")]
        [AllureOwner("Swadhin Patnaik")]
        [AllureDescription("As problem_user, verify 3rd highest priced product's name & price match")]
        public async Task TC003_Verify_3rdHighest_ProblemUser()
        {
            await Login("problem_user", "secret_sauce");
            await page.SelectOptionAsync(".product_sort_container", "hilo");

            var items = await page.QuerySelectorAllAsync(".inventory_item");
            var thirdItem = items[2];

            var expectedName = await (await thirdItem.QuerySelectorAsync(".inventory_item_name")).InnerTextAsync();
            var expectedPrice = await (await thirdItem.QuerySelectorAsync(".inventory_item_price")).InnerTextAsync();

            await (await thirdItem.QuerySelectorAsync(".inventory_item_name")).ClickAsync();

            var detailName = await page.InnerTextAsync(".inventory_details_name");
            var detailPrice = await page.InnerTextAsync(".inventory_details_price");

            if (expectedName != detailName)
            {
                Console.WriteLine($"[INFO] Expected visual bug: Name mismatch.\nInventory: {expectedName}\nDetails: {detailName}");
                await TakeScreenshotAsync("TC003_ProblemUser_NameMismatch");
            }
            else
            {
                Console.WriteLine("[INFO] No name mismatch – unexpected for problem_user.");
            }

            if (expectedPrice != detailPrice)
            {
                Console.WriteLine($"[INFO] Expected visual bug: Price mismatch.\nInventory: {expectedPrice}\nDetails: {detailPrice}");
                await TakeScreenshotAsync("TC003_ProblemUser_PriceMismatch");
            }
            else
            {
                Console.WriteLine("[INFO] No price mismatch – unexpected for problem_user.");
            }
        }

        [TearDown]
        public async Task Teardown()
        {
            await context.CloseAsync();
            await browser.CloseAsync();
        }

        private class AllureDescriptionAttribute : Attribute
        {
            public AllureDescriptionAttribute(string v)
            {
            }
        }
    }
}
