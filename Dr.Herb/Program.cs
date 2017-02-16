using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;


namespace Dr.Herb
{
    public class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //Application.Run(new FormTab());
        }

        public static List<String> GetHerbListFromCrv() {

            var ListHerb = new List<String>();
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            var ListFormCsv = File.ReadAllLines("藥草清單.csv", Encoding.GetEncoding("Big5")).AsEnumerable<string>();

            ListHerb.AddRange(ListFormCsv);

            return ListHerb;
        }



        public static Dictionary<String, String> GetDicOfIMETaiwanAndKeyboard()
        {
            var dic = new Dictionary<String, String>();

            dic.Add(",", "ㄝ");
            dic.Add("-", "ㄦ");
            dic.Add(".", "ㄡ");
            dic.Add("/", "ㄥ");
            dic.Add("0", "ㄢ");
            dic.Add("1", "ㄅ");
            dic.Add("2", "ㄉ");
            dic.Add("3", "ˇ ");
            dic.Add("4", "ˋ ");
            dic.Add("5", "ㄓ");
            dic.Add("6", "ˊ ");
            dic.Add("7", "˙ ");
            dic.Add("8", "ㄚ");
            dic.Add("9", "ㄞ");
            dic.Add(";", "ㄤ");
            dic.Add("a", "ㄇ");
            dic.Add("b", "ㄖ");
            dic.Add("c", "ㄏ");
            dic.Add("d", "ㄎ");
            dic.Add("e", "ㄍ");
            dic.Add("f", "ㄑ");
            dic.Add("g", "ㄕ");
            dic.Add("h", "ㄘ");
            dic.Add("i", "ㄛ");
            dic.Add("j", "ㄨ");
            dic.Add("k", "ㄜ");
            dic.Add("l", "ㄠ");
            dic.Add("m", "ㄩ");
            dic.Add("n", "ㄙ");
            dic.Add("o", "ㄟ");
            dic.Add("p", "ㄣ");
            dic.Add("q", "ㄆ");
            dic.Add("r", "ㄐ");
            dic.Add("s", "ㄋ");
            dic.Add("t", "ㄔ");
            dic.Add("u", "ㄧ");
            dic.Add("v", "ㄒ");
            dic.Add("w", "ㄊ");
            dic.Add("x", "ㄌ");
            dic.Add("y", "ㄗ");
            dic.Add("z", "ㄈ");

            return dic;
            
        }
     


    }
}
