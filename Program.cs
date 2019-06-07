using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace trajectoryJSON
{
    class Program
    {
        public static void Read()
        {
            // 读取文件的源路径及其读取流
           // string strReadFilePath = @"../../data/ReadLog.txt";
            string strReadFilePath = @"F:/重点研发计划/wxq_Research/轨迹数据/proj.txt";
            StreamReader srReadFile = new StreamReader(strReadFilePath);
            List<string> list = new List<string>();

            while (!srReadFile.EndOfStream)
            {
                string strReadLine = srReadFile.ReadLine(); //读取每行数据
                //string[] record = Regex.Split(strReadLine, "\\s+", RegexOptions.IgnoreCase);
                list.Add(strReadLine);
            }
            // 关闭读取流文件
            srReadFile.Close();

           
            JArray trajectoriesArray = new JArray();

            JObject trajectory = new JObject();
            trajectory.Add(new JProperty("vendor", 0));
            JArray trajectoryArray = new JArray();

            for (int i=0; i<list.Count-1;i++)
            {
                string[] record0 = Regex.Split(list[i], "\\s+", RegexOptions.IgnoreCase);
                string[] record1 = Regex.Split(list[i+1], "\\s+", RegexOptions.IgnoreCase);
                trajectoryArray.Add(new JArray(new JValue(Convert.ToDouble(record0[2])), new JValue(Convert.ToDouble(record0[1])), new JValue(Convert.ToInt16(record0[3]))));
               
                if (record0[0] != record1[0]) {
                    trajectory.Add(new JProperty("segments", trajectoryArray));
                    trajectoriesArray.Add(trajectory);
                    trajectory = new JObject();
                    trajectory.Add(new JProperty("vendor", 0));
                    trajectoryArray = new JArray();
                }

            }


            // 获取当前程序所在路径，并将要创建的文件命名为info.json 
            // 获取当前程序所在路径，并将要创建的文件命名为info.json 
            string fp = System.Windows.Forms.Application.StartupPath + "\\proj0606.json";
            if (!File.Exists(fp))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }

            //string fp = System.Windows.Forms.Application.StartupPath + "\\info.json";
            File.WriteAllText(fp, Newtonsoft.Json.JsonConvert.SerializeObject(trajectoriesArray));
           // Console.Write(trajectoriesArray);

            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Read();
            
           
          
           
        }
    }
}
