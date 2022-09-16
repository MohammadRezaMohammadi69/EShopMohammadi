using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace Utilities
{
    public class AESEncryption
    {
        //https://github.com/piyapan039285/Multi-platform-AES-Encryption
        //https://firebasestorage.googleapis.com/v0/b/multi-platform-aes-encryption.appspot.com/o/demo.html?alt=media&token=051a8034-ff34-47ce-82f4-2e0f30880ecb
        public string Key { get; set; }
        public AESEncryption(string Key)
        {
            this.Key = Key;
        }

        /// <summary>
        /// تولید کلید برای کد کدکداری
        /// </summary>
        /// <param name="byteLength"></param>
        /// <returns></returns>
        public static string generateRandomHex(int byteLength = 32)
        {
            int stringLength = byteLength * 2;

            string alphabet = "abcdef0123456789";
            string s = "";

            Random rnd = new Random();

            for (int i = 0; i < stringLength; i++)
            {
                int r = rnd.Next(0, alphabet.Length);

                s += alphabet[r];
            }

            //prevent NULL byte block.
            s = s.Replace("00", "11");

            return s;
        }
        /// <summary>
        /// کد کردن رشته
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string EncryptData(string text)
        {
            checkKey(Key);

            //generate random IV (16 bytes)
            string hexIV = generateRandomHex(16);

            //convert plainText to hex string.
            byte[] bytesData = System.Text.Encoding.UTF8.GetBytes(text);
            string hexStr = dataToHexString(bytesData);

            string cipherHexStr = __encryptData(hexStr, Key, hexIV);

            string hmacHexKey = generateRandomHex(16);
            string hmacHexStr = __computeHMAC(hexIV, cipherHexStr, Key, hmacHexKey);

            string encryptedHexStr = hexIV + hmacHexKey + hmacHexStr + cipherHexStr;
            return encryptedHexStr;
        }
        /// <summary>
        /// دیکد کردن رشته رمزگذاری شده
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public string DecryptData(string hexStr)
        {
            checkKey(Key);
            string plainText = null;

            if (hexStr.Length > 128)
            {
                string hexIV = hexStr.Substring(0, 32);
                string hmacHexKey = hexStr.Substring(32, 32);
                string hmacHexStr = hexStr.Substring(64, 64);
                string cipherHexStr = hexStr.Substring(128);

                string computedHmacHexStr = __computeHMAC(hexIV, cipherHexStr, Key, hmacHexKey);

                if (computedHmacHexStr.ToLower() == hmacHexStr.ToLower())
                {
                    string decryptedStr = __decryptData(cipherHexStr, Key, hexIV);

                    byte[] data = dataFromHexString(decryptedStr);
                    plainText = System.Text.Encoding.UTF8.GetString(data);
                }
            }

            return plainText;
        }

        private static byte[] dataFromHexString(string hexString)
        {
            hexString = hexString.Trim();
            hexString = hexString.Replace(" ", "");

            byte[] data = Enumerable.Range(0, hexString.Length / 2)
                                    .Select(x => Convert.ToByte(hexString.Substring(x * 2, 2), 16))
                                    .ToArray();

            return data;
        }

        private static string dataToHexString(byte[] data)
        {
            string hexString = String.Concat(Array.ConvertAll(data, x => x.ToString("X2")));

            return hexString;
        }

        private static void checkKey(string hexKey)
        {
            hexKey = hexKey.Trim();
            hexKey = hexKey.Replace(" ", "");
            hexKey = hexKey.ToLower();

            if (hexKey.Length != 64)
            {
                throw new Exception("key length is not 256 bit (64 hex characters)");
            }

            int i;
            for (i = 0; i < hexKey.Length; i += 2)
            {
                if (hexKey[i] == '0' && hexKey[i + 1] == '0')
                {
                    throw new Exception("key cannot contain zero byte block");
                }
            }
        }

        private static string __computeHMAC(string hexIV, string cipherHexStr, string hexKey, string hmacHexKey)
        {
            hexKey = hexKey.Trim();
            hexKey = hexKey.Replace(" ", "");
            hexKey = hexKey.ToLower();

            hmacHexKey = hmacHexKey.ToLower();

            string hexString = hexIV + cipherHexStr + hexKey;
            hexString = hexString.ToLower();

            byte[] data = System.Text.Encoding.UTF8.GetBytes(hexString);
            byte[] hmacKey = System.Text.Encoding.UTF8.GetBytes(hmacHexKey);

            HMACSHA256 hmac = new HMACSHA256(hmacKey);
            byte[] hashbytes = hmac.ComputeHash(data);
            string hashHexStr = dataToHexString(hashbytes);

            return hashHexStr;
        }

        public static string __encryptData(string hexString, string hexKey, string hexIV)
        {
            byte[] data = dataFromHexString(hexString);
            byte[] key = dataFromHexString(hexKey);
            byte[] iv = dataFromHexString(hexIV);

            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var encryptor = aes.CreateEncryptor(key, iv);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            byte[] encryptData = memoryStream.ToArray();

            string encryptHexData = dataToHexString(encryptData);
            return encryptHexData;
        }

        private static string __decryptData(string hexString, string hexKey, string hexIV)
        {
            byte[] data = dataFromHexString(hexString);
            byte[] key = dataFromHexString(hexKey);
            byte[] iv = dataFromHexString(hexIV);

            var aes = Aes.Create();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            var decryptor = aes.CreateDecryptor(key, iv);

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            byte[] decryptData = memoryStream.ToArray();

            string decryptHexData = dataToHexString(decryptData);
            return decryptHexData;
        }
    }
}
