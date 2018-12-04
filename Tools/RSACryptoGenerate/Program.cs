using System;
using System.Security.Cryptography;

namespace RSACryptoGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            {
                Console.WriteLine("PublicKey:");
                Console.WriteLine(Convert.ToBase64String(provider.ExportCspBlob(false)));
                Console.WriteLine("PrivateKey:");
                Console.WriteLine(Convert.ToBase64String(provider.ExportCspBlob(true)));
                Console.ReadKey();
            }
        }
    }
}
