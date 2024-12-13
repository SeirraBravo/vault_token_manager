using System.Text;
using System.Security.Cryptography;

namespace DpapiCache
{
    public class CacheHelper
    {
        //
        /// <summary>
        /// The absolute path of the drive (root folder of the application )where the encrypted token will be saved
        /// example : some path/filename.txt
        /// </summary>
        public static string CacheFilePath = "";
        /// <summary>
        /// Encrypts the given data using DPAPI. Saves the file in the given location for consuming the offline access token
        /// </summary>
        /// <param name="secret">Secret from the vault proxy.</param>
        /// <param name="scope">The scope for data protection (CurrentUser or LocalMachine).</param>
        /// <returns>Encrypted data as a base64-encoded string.</returns>
        public static string Encrypt(string secret, DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            if (string.IsNullOrEmpty(secret))
                throw new ArgumentNullException(nameof(secret));

            byte[] plainBytes = Encoding.UTF8.GetBytes(secret);
            byte[] encryptedBytes = ProtectedData.Protect(plainBytes, null, scope);
            File.WriteAllBytes(CacheFilePath, encryptedBytes);

            return Convert.ToBase64String(encryptedBytes);
        }

       
    }
}
