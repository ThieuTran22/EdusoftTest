using System.Text;

namespace EduTest
{
    internal class cText
    {
        public static string FileIdMatKhauSinhVien = @"C:\EduData\SinhVien.txt";
        public static string PortalLink = @"C:\EduData\Portal.txt";

        //Get info string
        public static string ReadFromFile(string filePath)
        {
            CreatePath(filePath);
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8)) {
                return reader.ReadToEnd();
            }
        }


        // Action
        public static void WriteToFile(string filePath, string text)
        {
            CreatePath(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8)) {
                // Ghi nội dung vào file
                writer.WriteLine(text);
            }
        }
        public static void CleanContent(string filePath)
        {
            CreatePath(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, false)) {
                writer.Write("");
            }
        }
        private static void CreatePath(string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath);
            // Kiểm tra xem thư mục đã tồn tại chưa
            if (!Directory.Exists(directoryPath)) {
                // Nếu chưa tồn tại thì tạo thư mục mới
                Directory.CreateDirectory(directoryPath);
            }
            if (!File.Exists(filePath)) {
                File.Create(filePath).Close();
            }
        }

    }
}
