using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Dr.Herb
{
    static class Program
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
        }

        public static List<String> GetHerbListFromCrv() {

            var ListHerb = new List<String>();
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            var ListFormCsv = File.ReadAllLines("藥草清單.csv", Encoding.GetEncoding("Big5")).AsEnumerable<string>();

            ListHerb.AddRange(ListFormCsv);

            return ListHerb;
        }

        
    }
}
