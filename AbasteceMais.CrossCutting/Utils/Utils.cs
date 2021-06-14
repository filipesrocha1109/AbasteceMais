using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace AbasteceMais.CrossCutting.Utils
{
    public static class Utils

    {
        public static string GetEnumDescription(Enum value)
        {
            // https://codereview.stackexchange.com/questions/157871/method-that-returns-description-attribute-of-enum-value
            return value.GetType().GetMember(value.ToString()).FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? value.ToString();
        }


        public static string Encrypt(this string value)
        {
            //Aloca espaco na memória para fazer o criptografia
            MemoryStream mem = new MemoryStream();
            //--Define a chave da encryptação
            string chave = "CIS3730";
            PasswordDeriveBytes pass = new PasswordDeriveBytes(chave, Encoding.ASCII.GetBytes(chave.ToCharArray()));
            byte[] bytKey = pass.GetBytes(32);
            byte[] bytIV = pass.GetBytes(16);
            //-Define o string a ser encryptada
            byte[] byVal = Encoding.ASCII.GetBytes(value.ToCharArray());
            Rijndael rgm = RijndaelManaged.Create();
            CryptoStream cst = new CryptoStream(mem, rgm.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write);
            cst.Write(byVal, 0, byVal.Length);
            cst.FlushFinalBlock();
            byte[] bytEncoded = mem.ToArray();
            cst.Close();
            mem.Close();
            return Convert.ToBase64String(bytEncoded);
        }

        public static string Decrypt(this string value)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(value);
            //Aloca espaco na memória para fazer o criptografia
            MemoryStream mem = new MemoryStream(cipherTextBytes);
            //--Define a chave da encryptação
            string chave = "CIS3730";
            PasswordDeriveBytes pass = new PasswordDeriveBytes(chave, Encoding.ASCII.GetBytes(chave.ToCharArray()));
            byte[] bytKey = pass.GetBytes(32);
            byte[] bytIV = pass.GetBytes(16);
            Rijndael rgm = RijndaelManaged.Create();
            CryptoStream cst = new CryptoStream(mem, rgm.CreateDecryptor(bytKey, bytIV), CryptoStreamMode.Read);
            //-Define o string a ser encryptada
            byte[] byVal = Encoding.ASCII.GetBytes(value.ToCharArray());
            int length = cst.Read(byVal, 0, byVal.Length);
            cst.Close();
            mem.Close();
            return Encoding.ASCII.GetString(byVal, 0, length);
        }

        public static string EncryptCIS(this string value)
        {
            string output = null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    int DecodeKey;
                    char[] aux;
                    for (int i = 0; i < value.Length; i++)
                    {
                        DecodeKey = (i + 1) % 26;
                        aux = value.Substring(i, 1).ToCharArray();
                        output += (char)(DecodeKey + (int)aux[0]);
                    }
                }
                catch { }
            }
            return output;
        }

        public static string DecryptCIS(this string value)
        {
            string output = null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    int DecodeKey;
                    char[] aux;
                    for (int i = 0; i < value.Length; i++)
                    {
                        DecodeKey = (i + 1) % 26;
                        aux = value.Substring(i, 1).ToCharArray();
                        output += (char)((int)aux[0] - DecodeKey);
                    }
                }
                catch { }
            }
            return output;
        }

        public static string ConvertDate(this string value)
        {
            string output = null;

            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace(" PM", "").Replace(" AM", "");

                DateTime DateValue = DateTime.Parse(value);

                output = DateValue.ToString("dd/MM/yyyy");

            };

            return output;
        }
        public static string ConvertDateUS(this string value)
        {
            string output = null;

            if (!string.IsNullOrEmpty(value))
            {
                output = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
            };

            return output;
        }

    }
}

