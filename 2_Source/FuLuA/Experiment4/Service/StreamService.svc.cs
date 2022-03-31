using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class StreamService : IStreamService
    {
        public static List<string> filesInfo;
        public static string path;

        public List<string> GetFilesInfo()
        {
            if (filesInfo == null)
            {
                filesInfo = new List<string>();
                //注意将DownloadFiles文件夹下的文件属性设置为“内容”和“如果较新则复制”
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DownloadFiles");
                DirectoryInfo di = new DirectoryInfo(path);
                var q = di.EnumerateFiles();
                foreach (var v in q)
                {
                    //格式：文件名，以字节为单位的文件长度
                    filesInfo.Add(string.Format("{0},{1}", v.Name, v.Length));
                }
            }
            return filesInfo;
        }

        public Stream DownloadStream(string fileName)
        {
            string filePath = System.IO.Path.Combine(path, fileName);
            //注意在Web.config文件中将includeExceptionDetailInFaults设置为"true"
            //才能在客户端看到抛出的异常
            try
            {
                FileStream imageFile = File.OpenRead(filePath);
                return imageFile;
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        public bool UploadStream(Stream stream)
        {
            //this implementation places the uploaded file
            //in the current directory and calls it "uploadedfile"
            //with no file extension
            string filePath = Path.Combine(
                System.Environment.CurrentDirectory,
                ".\\UploadedFiles\\img2.jpg");
            FileStream outstream = null;
            try
            {
                Console.WriteLine("Saving to file {0}", filePath);
                outstream = File.Open(filePath, FileMode.Create, FileAccess.Write);
                //read from the input stream in 4K chunks
                //and save to output stream
                const int bufferLen = 4096; //4KB
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = stream.Read(buffer, 0, bufferLen)) > 0)
                {
                    Console.Write(".");
                    outstream.Write(buffer, 0, count);
                }

                Console.WriteLine();
                Console.WriteLine("File {0} saved", filePath);

                return true;
            }
            catch (IOException ex)
            {
                Console.WriteLine(
                    String.Format("An exception was thrown while opening or writing to file {0}", filePath));
                Console.WriteLine("Exception is: ");
                Console.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                if (outstream != null)
                {
                    outstream.Close();
                }
                stream.Close();
            }
        }
    }
}
