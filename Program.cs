using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Crawler...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("Running");
            TinTrinhLibrary.WebClient client = new TinTrinhLibrary.WebClient();
            string html= "";
            for(int i=1; i<=4; i++)
            {
                html += client.Get("https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316?p=" + i, "https://tiki.vn/bestsellers/sach-truyen-tieng-viet/c316", "");
            }

            MatchCollection BookData = Regex.Matches(html, "data-price=\"(.*?)\" data-title=\"(.*?)\" data-brand=\"(.*?)\" data-category=\"(.*?)\">", RegexOptions.Singleline);
            MatchCollection Review = Regex.Matches(html, "class=\"review\">((.*?))</p>", RegexOptions.Singleline);
            MatchCollection Description = Regex.Matches(html, "description\">(.*?)<a", RegexOptions.Singleline);

            StreamWriter File = new StreamWriter("DataSave.txt");
            File.Flush();

            int j = 0;
            int a = 0;
            foreach (Match Book in BookData)
            {

                File.Write("Giá: ");
                File.WriteLine(Book.Groups[1].Value.Trim());
                File.Write("Tên sách: ");
                File.WriteLine(Book.Groups[2].Value.Trim());
                File.Write("Tác Giả: ");
                File.WriteLine(Book.Groups[3].Value.Trim());
                File.Write("Đánh giá: ");
                File.WriteLine(Review[j].Groups[1].Value.Trim());
                File.Write("Miêu tả: ");
                File.WriteLine(Description[a].Groups[1].Value.Trim());
                File.Write("Thể loại: ");
                File.WriteLine(Book.Groups[4].Value.Trim());
                File.WriteLine("___________________________");

                j ++;
                a++;
            }

            File.Close();

            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Completed. Let's check file DataSave.txt");
            Console.ReadLine();
        }
    }
}
