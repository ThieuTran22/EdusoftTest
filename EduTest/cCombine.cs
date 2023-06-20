using EduTest;

namespace EdusoftTest
{
    internal class cCombine
    {
        public static string Portal { get; set; }
        public static int PhanHe { get; set; } // 1 = Sinh Viên, 2 = giáo viên
        public static int KieuTest { get; set; } // 1 = 1 sinh viên, 2 = Tất cả sinh viên theo file


        public cCombine() { }
        public static void SetPortal()
        {
            if (cText.ReadFromFile(cText.PortalLink) == "") cText.WriteToFile(cText.PortalLink, "https://sp.aqtech.edu.vn/svtt.netweb/#/home");
            Portal = cText.ReadFromFile(cText.PortalLink);
        }
        public static void SetPhanHe()
        {
            Console.WriteLine("Vui lòng chọn phân hệ test: ");
            Console.WriteLine("1. Sinh Viên");
            Console.WriteLine("2. Giáo viên");
            Console.Write("Mời nhập phân hệ theo số: ");
            PhanHe = int.Parse(Console.ReadLine()!);
            Console.Clear();
            if (PhanHe == 1) Console.WriteLine("Bạn đang thực hiện test menu cho phân hệ sinh viên");
            else Console.WriteLine("Bạn đang thực hiện test menu cho phân hệ giáo viên");
        }

        public static void ChonKieuTest()
        {
            Console.WriteLine("Vui lòng chọn loại test: ");
            Console.WriteLine("1. Cho 1 sinh viên");
            Console.WriteLine("2. Test tất cả sinh viên theo file");
            Console.Write("Mời nhập loại test: ");
            KieuTest = int.Parse(Console.ReadLine()!);
        }
        public static void TestTheoSinhVien()
        {
            Console.WriteLine("Bạn đang thực hiện test menu cho 1 sinh viên");
            Console.Write("Vui lòng nhập mã sinh viên: ");
            string ma = Console.ReadLine()!;
            Console.Write("Vui lòng nhập mật khẩu: ");
            string matKhau = Console.ReadLine()!;
            cSinhVien sinhVien = new cSinhVien(ma, matKhau);

            sinhVien.TestXemChuongTrinhDaoTao();
            sinhVien.TestDanhGiaKetQuaRenLuyen("ssss");
            sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");
        }
        public static void TestTheoFile()
        {
            Console.WriteLine("Bạn đang thực hiện test menu cho nhiều sinh viên theo file");
            string listSinhVien = cText.ReadFromFile(cText.FileIdMatKhauSinhVien);
            for (int i = 1; i < listSinhVien.Split("#").Length; i++) {
                string item = listSinhVien.Split("#")[i];
                cSinhVien sinhVien = new cSinhVien(item.Split("-")[0], item.Split("-")[1]);
                Console.WriteLine("Đang thực hiện test sinh viên có mã là: " + sinhVien.MaSinhVien);
                sinhVien.TestXemChuongTrinhDaoTao();
                sinhVien.TestDanhGiaKetQuaRenLuyen("ssss");
                sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");
            }


        }
        public static void UnitTest()
        {

            /*            cSinhVien sinhVien = new cSinhVien("002", "1234");
                        //sinhVien.TestXemChuongTrinhDaoTao(); sss
                        //sinhVien.TestDanhGiaKetQuaRenLuyen("Tham gia hội thảo ngày 12/06");
                        sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");

                        *//*
                                    cSinhVien sinhVien = new cSinhVien("002", "1234");
                                    //sinhVien.TestXemChuongTrinhDaoTao();
                                    //sinhVien.TestDanhGiaKetQuaRenLuyen("Tham gia hội thảo ngày 12/06");
                                    sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");*/

            SetPortal();
            SetPhanHe();
            ChonKieuTest();
            if (KieuTest == 1) TestTheoSinhVien();
            else if (KieuTest == 2) TestTheoFile();

        }
    }
}
