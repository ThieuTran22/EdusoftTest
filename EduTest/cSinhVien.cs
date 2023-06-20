using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace EdusoftTest
{
    internal class cSinhVien : cEduWeb
    {
        #region Prop, construct 
        public string MaSinhVien { get; set; }
        public string router = "xemthongbao\r\nctdt\r\nmontienquyet\r\ndangkymonhoc\r\nrutmonhoc\r\ndknguyenvong\r\nsvxemhocbong\r\nhocphi\r\nthanhtoanonline\r\ninphieunoptien\r\nhoadondientu\r\ntkb-tuan\r\ntkb-hocky\r\nxemfilebaigiang\r\nsvdangkythilai\r\nlichthi\r\ndiem\r\nsvxemconno\r\nsvxemdiemtlkv\r\ndanhgiarenluyensv\r\nsvdkkhoaluantn\r\ndangkyxettotnghiep\r\nsvxemtientotnghiep\r\nxemkqtotnghiep\r\ndanhgia\r\ndkgiaychungnhan\r\nxemgcndadangky\r\ndkbangdiemsv\r\nsvnhapngoaitru\r\nsvnhapthuongtru\r\ncapnhatlylich\r\nsvnhapbhyt\r\ndkkytucxa\r\nxemktxdadangky\r\ndkphongchucnang\r\ntinhtrangkhoama\r\nxemphongchucnangdadangky\r\ngopykien\r\ntracuuvbcc";
        public string MatKhau { get; set; }
        public cSinhVien() : base(false, true) { }
        public cSinhVien(string maSinhVien, string matKhau) : base(false, true)
        {
            MaSinhVien = maSinhVien;
            MatKhau = matKhau;
            DangNhap(maSinhVien, matKhau);
        }
        #endregion

        #region Support
        public void ChuyenMenuThanhHam()
        {
            int i = 0;
            foreach (var item in MenuTinhNang) {
                Console.WriteLine($"// {item.Text}\r\n" +
                    $"public bool Test{cUtils.RemoveVietnameseSignsAndSpaces(item.Text)}()\r\n" +
                    "{\r\n" +
                    $"NavTo(\"https://sp.aqtech.edu.vn/svtt.Netweb/#/\" + router.Split(\"\\r\\n\")[{i}]);\r\n" +
                    $"if (KhongTimThayDuLieu()) return false;\r\n" +
                    $"Console.WriteLine($\"Đang test menu: {item.Text}\");\r\n" +
                    $"return true;\r\n" +
                    "}\r\n");
                i++;
            }
        }
        public bool KiemTraXuLy()
        {
            Click(GetEle(By.XPath("//button[contains(text(),\"Lưu\")]")));
            try {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(text(),\"Xử lý thành công\")]")));
            } catch (Exception) {
                Console.WriteLine("Xử lý thất bại");
                return false;
            }
            Console.WriteLine("Xử lý thành công");
            return true;
        }
        public bool KhongTimThayDuLieu()
        {
            Thread.Sleep(2000);
            if (ContainsText("Không tìm thấy dữ liệu")) {
                Console.WriteLine("Không có dữ liệu test ở menu này");
                return true;
            }
            return false;
        }
        #endregion

        #region TestChung
        public void InCacMenuKhongCoDuLieu()
        {
            Console.WriteLine("Các menu sau không có dữ liệu: ");
            foreach (var item in MenuTinhNang) {
                ScrollBot();
                Thread.Sleep(2000);
                item.Click();
                Thread.Sleep(2000);
                if (ContainsText("Hiện tại bạn đã nghỉ")) {
                    Click(By.XPath("//button[contains(text(),\"Đóng\")]"));
                }
                Thread.Sleep(3500);
                if (ContainsText("Không tìm thấy dữ liệu"))
                    Console.WriteLine(item.Text);
            }
        }
        #endregion

        #region Test menu
        // Thông báo từ ban quản trị
        public bool TestThongBaoTuBanQuanTri()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[0]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Thông báo từ ban quản trị");
            return true;
        }

        // Xem chương trình đào tạo
        public bool TestXemChuongTrinhDaoTao()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[1]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem chương trình đào tạo");

            // Tạo thông số
            int viewThanhPhan = 0;
            int viewTaiLieu = 0;
            int tongSoLoi = 0;
            Thread.Sleep(2000);

            // Test view thành phần
            foreach (var item in GetEles(By.XPath("//td[contains(@class,\"clickable\")]"))) {
                try {
                    Click(item);
                    if (!ContainsText("Tên thành phần"))
                        Console.WriteLine("Không hiển thị popup");
                    else
                        Click(By.XPath("//button[contains(@aria-label,\"Close\")]"));
                    viewThanhPhan++;
                } catch (Exception e) {
                    Console.WriteLine($"Bị lỗi khi click view thành phần thứ {viewThanhPhan}: " + e);
                    tongSoLoi++;
                }
            }

            // Test view tài liệu
            foreach (var item in GetEles(By.XPath("//span[contains(@class,\"clickable\")]"))) {
                try {
                    Click(item);
                    if (!ContainsText("Tác giả/ Đồng tác giả"))
                        Console.WriteLine("Không hiển thị popup");
                    else
                        Click(By.XPath("//button[contains(@aria-label,\"Close\")]"));
                    viewTaiLieu++;
                } catch (Exception e) {
                    Console.WriteLine($"Bị lỗi khi click ở nút tài liệu thứ {viewTaiLieu}: " + e);
                    tongSoLoi++;
                }
            }

            //In kết quả
            Console.WriteLine("Hoàn thành!");
            Console.WriteLine("Tổng số view thành phần đã test là: " + viewThanhPhan);
            Console.WriteLine("Tổng số view thành phần đã test là: " + viewTaiLieu);
            Console.WriteLine("Tổng số lỗi: " + tongSoLoi);
            return true;
        }
        // Xem môn học tiên quyết
        public bool TestXemMonHocTienQuyet()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[2]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem môn học tiên quyết");
            return true;
        }

        // Đăng ký môn học
        public bool TestDangKyMonHoc()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[3]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký môn học");
            return true;
        }

        // Rút môn học đã đăng ký
        public bool TestRutMonHocDaDangKy()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[4]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Rút môn học đã đăng ký");
            return true;
        }

        // Đăng ký môn nguyện vọng
        public bool TestDangKyMonNguyenVong()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[5]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký môn nguyện vọng");
            return true;
        }

        // Xem học bổng
        public bool TestXemHocBong()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[6]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem học bổng");
            return true;
        }

        // Xem học phí
        public bool TestXemHocPhi()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[7]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem học phí");
            return true;
        }

        // Đóng học phí
        public bool TestDongHocPhi()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[8]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đóng học phí");
            return true;
        }

        // In phiếu nộp tiền
        public bool TestInPhieuNopTien()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[9]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: In phiếu nộp tiền");
            return true;
        }

        // Hóa đơn điện tử
        public bool TestHoaDonDienTu()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[10]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Hóa đơn điện tử");
            return true;
        }

        // Xem thời khóa biểu tuần
        public bool TestXemThoiKhoaBieuTuan()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[11]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem thời khóa biểu tuần");
            return true;
        }

        // Xem thời khóa biểu học kỳ
        public bool TestXemThoiKhoaBieuHocKy()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[12]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem thời khóa biểu học kỳ");
            return true;
        }

        // Xem file bài giảng
        public bool TestXemFileBaiGiang()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[13]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem file bài giảng");
            return true;
        }

        // Đăng ký thi lại
        public bool TestDangKyThiLai()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[14]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký thi lại");
            return true;
        }

        // Xem lịch thi
        public bool TestXemLichThi()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[15]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem lịch thi");
            return true;
        }

        // Xem điểm
        public bool TestXemDiem()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[16]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem điểm");
            return true;
        }

        // Xem môn & chứng chỉ còn nợ
        public bool TestXemMonVaChungChiConNo()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[17]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem môn & chứng chỉ còn nợ");
            return true;
        }

        // Xem điểm tích lũy kỳ vọng
        public bool TestXemDiemTichLuyKyVong()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[18]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem điểm tích lũy kỳ vọng");
            return true;
        }

        // Đánh giá kết quả rèn luyện
        public bool TestDanhGiaKetQuaRenLuyen(string hoatDongGhiChu)
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[19]);
            //if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đánh giá kết quả rèn luyện");
            Thread.Sleep(3000);

            //Khởi tạo 2 list là điểm tối đa và ô điểm đánh giá
            List<string> listDiemToiDa = new List<string>();
            List<IWebElement> listODanhGia = GetEles(By.XPath("//input[contains(@id,\"diem_tu_danh_gia\")]")).ToList();

            //Lấy số điểm đánh giá tối đa thành list
            foreach (var item in GetEles(By.XPath("//td[@class=\"align-middle\"]"))) {
                listDiemToiDa.Add(item.Text);
            }
            listDiemToiDa = cUtils.RemoveSpecialElements(listDiemToiDa);

            for (int i = 0; i < listODanhGia.Count(); i++) {
                listODanhGia[i].Clear();
                Send(listODanhGia[i], listDiemToiDa[i]);
            }

            //Ghi hoạt động
            if (!string.IsNullOrEmpty(hoatDongGhiChu)) {
                GetEle(By.XPath("//textarea[@formcontrolname=\"ghi_chu_sv\"]")).Clear();
                Send(GetEle(By.XPath("//textarea[@formcontrolname=\"ghi_chu_sv\"]")), hoatDongGhiChu);
            }

            return KiemTraXuLy();
        }

        // Đăng ký khóa luận tốt nghiệp
        public bool TestDangKyKhoaLuanTotNghiep()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[20]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký khóa luận tốt nghiệp");
            return true;
        }

        // Đăng ký xét tốt nghiệp
        public bool TestDangKyXetTotNghiep(bool TatCaCo, string SoDienThoai, string GhiChu)
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[21]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký xét tốt nghiệp");

            List<IWebElement> listRadio = new List<IWebElement>();
            foreach (var item in GetEles(By.XPath("//input[@type=\"radio\"]"))) {
                listRadio.Add(item);
            }

            // Check Vào các điều kiện xét tốt nghiệp
            if (TatCaCo) {
                var le = cUtils.LayPhanTu(listRadio, true);
                foreach (var item in le) {
                    Click(item);
                }
            }
            else {
                var chan = cUtils.LayPhanTu(listRadio, false);
                foreach (var item in chan) {
                    Click(item);
                }
            }

            // Cập nhật số điện thoại
            IWebElement soDienThoai = GetEle(By.XPath("//input[@class=\'form-control ng-untouched ng-pristine ng-valid\']"));
            soDienThoai.Clear();
            Send(soDienThoai, SoDienThoai);

            // Ghi chú thông tin
            IWebElement elghiChu = GetEle(By.XPath("//textarea[@class=\"form-control ng-untouched ng-pristine ng-valid\"]"));
            elghiChu.Clear();
            Send(elghiChu, GhiChu);

            return KiemTraXuLy();
        }

        // Xem kết quả tiền tốt nghiệp
        public bool TestXemKetQuaTienTotNghiep()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[22]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem kết quả tiền tốt nghiệp");
            return true;
        }

        // Xem kết quả xét tốt nghiệp
        public bool TestXemKetQuaXetTotNghiep()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[23]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem kết quả xét tốt nghiệp");
            return true;
        }

        // Khảo sát đánh giá
        public bool TestKhaoSatDanhGia()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[24]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Khảo sát đánh giá");
            return true;
        }

        // Đăng ký cấp giấy chứng nhận
        public bool TestDangKyCapGiayChungNhan(string thongtin, string ykien)
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[25]);
            //if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký cấp giấy chứng nhận");
            Thread.Sleep(2000);

            GetEle(By.XPath("//span[contains(@class,\"ng-arrow-wrapper\")]")).Click();
            GetEle(By.XPath("//div[contains(text(),' Mẫu giấy chứng nhận vay vốn ')]")).Click();
            Send(By.XPath("//textarea[@formcontrolname='thong_tin_GiayCN']"), thongtin);
            //GetEle(By.XPath("//span[contains(@class,\"ng-star-inserted\")]")).Click();
            Send(By.XPath("//textarea[@formcontrolname='y_kien_nhan_xet']"), ykien);
            GetEle(By.XPath("//button[contains(text(),'Lưu')]")).Click();
            Console.WriteLine($"Lưu thành công");

            Thread.Sleep(1000);
            GetEle(By.XPath("//span[contains(@class,\"ng-arrow-wrapper\")]")).Click();
            GetEle(By.XPath("//div[contains(text(),' Mẫu giấy chứng nhận vay vốn ')]")).Click();
            Send(By.XPath("//textarea[@formcontrolname='thong_tin_GiayCN']"), thongtin);
            //GetEle(By.XPath("//span[contains(@class,\"ng-star-inserted\")]")).Click();
            Send(By.XPath("//textarea[@formcontrolname='y_kien_nhan_xet']"), ykien);
            GetEle(By.XPath("//button[contains(text(),'Nhập mới')]")).Click();
            Console.WriteLine($"Làm mới thành công");

            Thread.Sleep(1000);
            GetEle(By.XPath("//td[contains(@class,\"align-middle clickable text-center ng-star-inserted\")]")).Click();
            Thread.Sleep(1000);
            GetEle(By.XPath("//button[contains(text(),'Xác nhận')]")).Click();
            Console.WriteLine($"Xóa thành công");


            Console.WriteLine($"Hoàn thành menu");


            return true;
        }

        // Xem giấy chứng nhận đã đăng ký
        public bool TestXemGiayChungNhanDaDangKy()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[26]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem giấy chứng nhận đã đăng ký");
            return true;
        }

        // Đăng ký cấp bảng điểm
        public bool TestDangKyCapBangDiem()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[27]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký cấp bảng điểm");
            return true;
        }

        // Cập nhật thông tin ngoại trú
        public bool TestCapNhatThongTinNgoaiTru()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[28]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Cập nhật thông tin ngoại trú");
            return true;
        }

        // Cập nhật thông tin thường trú
        public bool TestCapNhatThongTinThuongTru()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[29]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Cập nhật thông tin thường trú");
            return true;
        }

        // Cập nhật thông tin lý lịch
        public bool TestCapNhatThongTinLyLich()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[30]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Cập nhật thông tin lý lịch");
            return true;
        }

        // Cập nhật thông tin BHYT
        public bool TestCapNhatThongTinBhyt()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[31]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Cập nhật thông tin BHYT");
            return true;
        }

        // Đăng ký ký túc xá
        public bool TestDangKyKyTucXa()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[32]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký ký túc xá");
            return true;
        }

        // Xem phòng ký túc xá đã đăng ký
        public bool TestXemPhongKyTucXaDaDangKy()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[33]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem phòng ký túc xá đã đăng ký");
            return true;
        }

        // Đăng ký phòng chức năng
        public bool TestDangKyPhongChucNang()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[34]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Đăng ký phòng chức năng");
            return true;
        }

        // Tình trạng khóa mã sinh viên
        public bool TestTinhTrangKhoaMaSinhVien()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[35]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Tình trạng khóa mã sinh viên");
            return true;
        }

        // Xem phòng chức năng đăng ký
        public bool TestXemPhongChucNangDangKy()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[36]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Xem phòng chức năng đăng ký");
            return true;
        }

        // Gửi ý kiến cho ban quản lý
        public bool TestGuiYKienChoBanQuanLy(string chuDe, string noiDung)
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[37]);
           /* if (KhongTimThayDuLieu()) return false;*/
            Console.WriteLine($"Đang test menu: Gửi ý kiến cho ban quản lý");
            Thread.Sleep(2000);
            // Dien chu de
            Send(By.XPath("//input[@formcontrolname='chu_de']"),chuDe);
            // Dien noi dung
            //driver.FindElement(By.XPath("//div[@role=\"textbox\"]")).SendKeys("ádasdsadsadqbjqwje");
            Send(By.XPath("//div[@role=\"textbox\"]"),noiDung);
            // Click gui gop y 
            GetEle(By.XPath("//button[contains(text(),'Gửi góp ý')]")).Click();
            // Kiem tra noi dung da gop y
            Console.WriteLine("Số góp ý đã kiểm tra");
            //Dùng descendant để tìm biến con trong 1 biến cha
            var tableNoiDungGopY = GetEles(By.XPath("//tbody//descendant::tr"));
      
            Console.WriteLine(tableNoiDungGopY.Count);
            return true;
           
        }

        // Tra cứu văn bằng chứng chỉ
        public bool TestTraCuuVanBangChungChi()
        {
            NavTo("https://sp.aqtech.edu.vn/svtt.Netweb/#/" + router.Split("\r\n")[38]);
            if (KhongTimThayDuLieu()) return false;
            Console.WriteLine($"Đang test menu: Tra cứu văn bằng chứng chỉ");
            return true;
        }
        #endregion
    }
}
