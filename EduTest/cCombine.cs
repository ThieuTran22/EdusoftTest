namespace EdusoftTest
{
    internal class cCombine
    {

        public cCombine() { }

        public static void UnitTest()
        {
            cSinhVien sinhVien = new cSinhVien("002", "1234");
            //sinhVien.TestXemChuongTrinhDaoTao();
            //sinhVien.TestDanhGiaKetQuaRenLuyen("Tham gia hội thảo ngày 12/06");
            sinhVien.TestDangKyXetTotNghiep(true, "0984123123", "ghi chú");
        }
    }
}
