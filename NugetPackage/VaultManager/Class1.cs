using System.Net.Http;

namespace VaultManager
{
    public class VaultProxy
    {
        /// <summary>
        /// Accessing the vault with the given address for secret using an http client instance
        /// Returns the secret (string) if vault is accessable or returns null
        /// Http client disposes once action completes or exception throws
        /// </summary>
        public static string VaultUrl = "";
        private static readonly HttpClient _httpClient = new HttpClient();


        public static async Task<string> RetrieveSecretFromRemoteVaultAsync()
        {
            try
            {
                //string VaultUrl = "http://" + VaultHostName + ":" + VaultHostPort;
                var response = await _httpClient.GetAsync(VaultUrl);
                response.EnsureSuccessStatusCode();
                string secret = await response.Content.ReadAsStringAsync();
                _httpClient.Dispose();
                return secret;
            }
            catch (HttpRequestException)
            {
                _httpClient.Dispose();
                return null;

            }
        }

    }
}
