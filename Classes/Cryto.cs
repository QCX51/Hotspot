
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Copyright (C) Alain Eus. Rivera
/// ...*.*.*.::: QCX51 :::.*.*.*...
/// </summary>

namespace Classes
{
    internal static class Cryto
    {
        private static RijndaelManaged RijndaelMgd
        {
            get { return new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7, KeySize = 256, BlockSize = 256 }; }
        }

        private static RijndaelManaged CryptoTransform(string Password)
        {
            byte[] SaltKey;
            RijndaelManaged RijndaelMgd = Cryto.RijndaelMgd;
            using (SHA256 SHA256CSP = new SHA256CryptoServiceProvider())
            { SaltKey = SHA256CSP.ComputeHash(Encoding.Unicode.GetBytes(Password)); }
            using (Rfc2898DeriveBytes DeriveKey = new Rfc2898DeriveBytes(Password, SaltKey))
            { RijndaelMgd.Key = DeriveKey.GetBytes(32); RijndaelMgd.IV = DeriveKey.GetBytes(32); }
            return RijndaelMgd;
        }
        public static byte[] Encrypt(byte[] Data, string Password)
        {
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateEncryptor())
            using (MemoryStream MemStream = new MemoryStream())
            using (CryptoStream CS = new CryptoStream(MemStream, ICrypto, CryptoStreamMode.Write))
            { CS.Write(Data, 0, Data.Length); CS.FlushFinalBlock(); return MemStream.ToArray(); }
        }
        public static byte[] Decrypt(byte[] Data, string Password)
        {
            using (MemoryStream Decrypted = new MemoryStream())
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateDecryptor())
            using (Stream MemStream = new MemoryStream(Data))
            using (CryptoStream CS = new CryptoStream(MemStream, ICrypto, CryptoStreamMode.Read))
            { CS.CopyTo(Decrypted); return Decrypted.ToArray(); }
        }
        public static void EncryptFile(string FilePath, string Password)
        {
            using (FileStream IFile = File.Open(FilePath, FileMode.Open))
            using (FileStream OFile = File.Create(string.Concat(FilePath, ".rmgd")))
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateEncryptor())
            using (CryptoStream CS = new CryptoStream(OFile, ICrypto, CryptoStreamMode.Write))
            { IFile.CopyTo(CS); }
            File.Delete(FilePath);
        }
        public static void DecryptFile(string FilePath, string Password)
        {
            using (FileStream IFile = File.Open(FilePath, FileMode.Open))
            using (FileStream OFile = File.Create(FilePath.Replace(".rmgd", "")))
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateDecryptor())
            using (CryptoStream CS = new CryptoStream(IFile, ICrypto, CryptoStreamMode.Read))
            { CS.CopyTo(OFile); }
            File.Delete(FilePath);
        }
        public static string EncryptText(string PlainText, string Password)
        {
            byte[] EncryptedText;
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateEncryptor())
            using (MemoryStream MemStream = new MemoryStream())
            using (CryptoStream CS = new CryptoStream(MemStream, ICrypto, CryptoStreamMode.Write))
            using (StreamWriter SW = new StreamWriter(CS) { AutoFlush = true })
            { SW.Write(PlainText); SW.Close(); EncryptedText = MemStream.ToArray(); }
            return Convert.ToBase64String(EncryptedText);
        }
        public static string DecryptText(string Base64Data, string Password)
        {
            string DecryptedText;
            using (RijndaelManaged RijndaelMgd = CryptoTransform(Password))
            using (ICryptoTransform ICrypto = RijndaelMgd.CreateDecryptor())
            using (MemoryStream MemStream = new MemoryStream(Convert.FromBase64String(Base64Data)))
            using (CryptoStream CS = new CryptoStream(MemStream, ICrypto, CryptoStreamMode.Read))
            using (StreamReader SR = new StreamReader(CS))
            { DecryptedText = SR.ReadToEnd(); }
            return DecryptedText;
        }
    }
}
