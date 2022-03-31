using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ch04.Examples
{
    public class AesHelp
    {
        /// <summary>使用AES算法加密字符串</summary>
        public static byte[] EncryptString(string str, byte[] key, byte[] iv)
        {
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                using (StreamWriter sw = new StreamWriter(cs))
                {
                    sw.Write(str);
                }
                encrypted = ms.ToArray();
                cs.Close();
                ms.Close();
            }
            return encrypted;
        }

        /// <summary>使用AES算法解密字符串</summary>
        public static string DescrptString(byte[] data, byte[] key, byte[] iv)
        {
            string str = null;
            using (Aes aesAlg = Aes.Create())
            {
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);
                MemoryStream ms = new MemoryStream(data);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using (StreamReader sr = new StreamReader(cs))
                {
                    str = sr.ReadToEnd();
                }
                cs.Close();
                ms.Close();
            }
            return str;
        }

        /// <summary>根据提供的密码生成Key和IV</summary>
        public static void GenKeyIV(string password, out byte[] key, out byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                key = new byte[aes.Key.Length];
                byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
                for (int i = 0; i < pwdBytes.Length; i++)
                {
                    key[i] = pwdBytes[i];
                }
                iv = new byte[aes.IV.Length];
                for (int i = 0; i < pwdBytes.Length; i++)
                {
                    iv[i] = pwdBytes[i];
                }
            }
        }

        /// <summary>随机生成Key和IV</summary>
        public static void GenKeyIV(out byte[] key, out byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                key = aes.Key;
                iv = aes.IV;
            }
        }
    }
}
