namespace EdusoftTest
{
    internal class cCombine
    {
        public static string Portal { get; set; }
        public cCombine() { }
        public static void SetPortal()
        {
            Console.Write("Vui lòng nhập portal: ");
            Portal = Console.ReadLine()!;
        }

        public static void UnitTest()
        {
            cSinhVien sinhVien = new cSinhVien("002", "1234");
            //sinhVien.TestXemChuongTrinhDaoTao();
            //sinhVien.TestDanhGiaKetQuaRenLuyen("Tham gia hội thảo ngày 12/06");
            sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");
        }
    }
}
