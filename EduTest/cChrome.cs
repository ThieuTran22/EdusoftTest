using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EdusoftTest
{
    internal class cChrome
    {
        #region Prop and init
        public ChromeDriver driver;
        public cChrome(bool headless, bool incognito)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-maximized", "--ignore-certificate-errors", "--disable-notifications",
                incognito ? "--incognito" : $"--user-data-dir=C:\\Profile",
                headless ? "--headless=new" : "no");

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            try {
                driver = new ChromeDriver(service, options);
            } catch (Exception e) {
                if (e.ToString().Contains("This version of ChromeDriver only supports")) {
                    Console.WriteLine("ChromeDriver.exe trong thư mục ToolGigs " +
                        "không cùng phiên bản với trình duyệt hoặc trình " +
                        "duyệt chưa update lên phiên bản mới nhất");
                }
            }
        }
        #endregion

        #region Navigate
        public void NavTo(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
        public void NavForward()
        {
            driver.Navigate().Forward();
        }
        public void NavBack()
        {
            driver.Navigate().Back();
        }
        public void Refresh()
        {
            driver.Navigate().Refresh();
        }
        #endregion

        #region Action
        public void OpenNewTab(string link)
        {
            driver.ExecuteScript($"window.open('{link}')");
            Thread.Sleep(2000);
        }
        public void Click(By by)
        {
            driver.ExecuteScript("arguments[0].click();", GetEle(by));
        }
        public void Click(IWebElement el)
        {
            driver.ExecuteScript("arguments[0].click();", el);
        }
        public void Send(By by, string text)
        {
            driver.FindElement(by).SendKeys(text);
        }
        public void Send(IWebElement el, string text)
        {
            el.SendKeys(text);
        }
        public void ClickReady(By by, int secondsWait)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(secondsWait));
            Click(wait.Until(ExpectedConditions.ElementToBeClickable(by)));
        }
        public void Close()
        {
            driver.Close();
            SwitchLast();
        }
        public void Quit()
        {
            driver.Quit();
        }
        #endregion

        #region Switch
        public void SwitchTab(int index)
        {
            driver.SwitchTo().Window(driver.WindowHandles[index]);
        }
        public void SwitchLast()
        {
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
        public void SwitchFrame(IWebElement iframe)
        {
            driver.SwitchTo().Frame(iframe);
        }
        public void SwitchDefault()
        {
            driver.SwitchTo().DefaultContent();
        }
        #endregion

        #region Scroll
        public void ScrollTop()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
        }
        public void ScrollBot()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }
        public void ScrollTo(int x, int y)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy({x}, {y});");
        }
        public void ScrollAllTop()
        {
            for (int i = 0; i < driver.WindowHandles.Count; i++) {
                SwitchTab(i);
                Thread.Sleep(1000);
                ScrollTop();
            }
        }
        public void ScrollAllBot()
        {
            for (int i = 0; i < driver.WindowHandles.Count; i++) {
                SwitchTab(i);
                Thread.Sleep(1000);
                ScrollBot();
            }
        }
        #endregion

        #region Get element
        public IWebElement GetEleXpath(string by, string content, string last)
        {
            return driver.FindElement(By.XPath($"*//[contains({by},'{content}')]"));
        }
        public IWebElement GetEleDesce(string by, string content, string descendantEl)
        {
            return driver.FindElement(By.XPath($"*//[contains({by},'{content}')]//descendant::{descendantEl}"));
        }
        public IWebElement GetEle(By by)
        {
            return driver.FindElement(by);
        }
        public IList<IWebElement> GetEles(By by)
        {
            return driver.FindElements(by);
        }
        #endregion

        #region Contains bool
        public bool ContainsText(string text)
        {
            return driver.FindElements(By.XPath($"//*[contains(text(),\"{text}\")]")).Count > 0;
        }
        public bool ContainsClass(string className)
        {
            return driver.FindElements(By.XPath($"//*[contains(@class,\"{className}\")]")).Count > 0;
        }
        public bool ContainsHref(string href)
        {
            return driver.FindElements(By.XPath($"//*[contains(@href,\"{href}\")]")).Count > 0;
        }
        #endregion

        #region Get info
        public string GetUrl()
        {
            return driver.Url;
        }
        public string GetUrlName()
        {
            return driver.Url.Split("/")[2];
        }
        public string GetSource()
        {
            return driver.PageSource;
        }
        public string GetText(IWebElement el)
        {
            return el.Text;
        }
        public string GetContainsHref(string subHref)
        {
            return driver.FindElements(By.XPath($"//a[contains(@href,\"{subHref}\")]"))[0].GetAttribute("href");
        }
        public int GetNumberTab()
        {
            return driver.WindowHandles.Count();
        }
        #endregion
    }
}
