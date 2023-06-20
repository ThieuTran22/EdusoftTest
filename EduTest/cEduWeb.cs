using OpenQA.Selenium;
using System.Diagnostics;

namespace EdusoftTest
{
    internal class cEduWeb : cChrome
    {
        #region Prop and contruct
        public List<IWebElement> MenuTinhNang { get; set; } = new List<IWebElement>();
        public cEduWeb(bool headless, bool incognito) : base(headless, incognito)
        {
            NavTo(cCombine.Portal);
            Thread.Sleep(3000);
        }
        public bool DangNhap(string taiKhoan, string matKhau)
        {
            /*    try {*/
            // Điền tài khoản và mật khẩu
            Send(By.XPath("//input[@formcontrolname='username']"), taiKhoan);
            Send(By.XPath("//input[@formcontrolname='password']"), matKhau);

            //Nhấp đăng nhập
            Click(By.XPath("//button[contains(text(),'Đăng nhập')]"));
            Thread.Sleep(3000);
            if (ContainsText("Đăng nhập không thành công")) {
                Console.WriteLine("Tài khoản hoặc mật khẩu không đúng!");
                Console.ReadKey();

            }
            SetMenuTinhNang();
            return true;
            /*            } catch (Exception e) {
                            Console.WriteLine("Có lỗi xảy ra khi đăng nhập: " + e);
                            return false;
                        }*/
        }
        #endregion

        #region Set element
        public void SetMenuTinhNang()
        {
            foreach (var item in driver.FindElements(By.XPath("//*[contains(@class,\"text-cus\")]//descendant::a"))) {
                if (item.Text == "") continue;
                MenuTinhNang.Add(item);
            }
        }

        #endregion

        #region Test diện rộng
        public void TestMenuTinhNang()
        {
            // Thực hiện click từng menu và đếm thời gian
            foreach (var item in MenuTinhNang) {
                try {
                    //Tạo bộ đếm thời gian
                    Stopwatch stopwatch = new Stopwatch();

                    //In thông tin và thực hiện click menu
                    Console.WriteLine("Đang thực hiện click Menu: " + item.Text + ".......");
                    stopwatch.Start();
                    Click(item);

                    //Dừng và in thông tin
                    stopwatch.Stop();
                    Console.WriteLine($"Tổng thời gian thực hiện click menu {item.Text} là: {stopwatch.Elapsed.TotalSeconds}\r\n");
                } catch (Exception) {
                    Console.WriteLine("Lỗi khi click menu: " + item.Text);
                    continue;
                }
            }
        }
        #endregion
    }
}
