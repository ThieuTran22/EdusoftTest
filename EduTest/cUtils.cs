using System.Globalization;
using System.Text;

namespace EdusoftTest
{
    internal class cUtils
    {
        #region Bỏ dấu tiếng việt và dấu cách
        public static string RemoveVietnameseSignsAndSpaces(string input)
        {
            // Bước 1: Bỏ dấu tiếng Việt
            string temp = RemoveVietnameseSigns(input);

            // Bước 2: Bỏ dấu cách và viết hoa đầu mỗi từ
            string result = RemoveSpacesAndTitleCase(temp);

            return result;
        }
        public static string RemoveVietnameseSigns(string input)
        {
            string[] vietnameseSigns = new string[]
            {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
            };

            StringBuilder stringBuilder = new StringBuilder(input);

            for (int i = 1; i < vietnameseSigns.Length; i++) {
                for (int j = 0; j < vietnameseSigns[i].Length; j++) {
                    stringBuilder.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            return stringBuilder.ToString();
        }
        static string RemoveSpacesAndTitleCase(string input)
        {
            string[] words = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < words.Length; i++) {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
            }

            return string.Join("", words);
        }
        #endregion

        #region Loại bỏ ele dấu chấm hoặc chữ
        public static List<string> RemoveSpecialElements(List<string> arr)
        {
            var result = arr.Where(item => !item.Contains(".") && !ContainsLetter(item)).ToList();
            return result;
        }

        public static bool ContainsLetter(string str)
        {
            foreach (char c in str) {
                if (char.IsLetter(c)) {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Loại bỏ các phần tử ở index chẵn hoặc lẻ
        public static List<T> LayPhanTu<T>(List<T> list, bool Chan)
        {
            List<T> result = new List<T>();

            for (int i = 0; i < list.Count; i++) {
                if (Chan) {
                    if (i % 2 == 0) result.Add(list[i]);
                }
                else if (i % 2 != 0) result.Add(list[i]);
            }

            return result;
        }
        #endregion
    }
}
