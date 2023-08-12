namespace Core.Helpers
{
    public class Encryption : IEncryption
    {
        private readonly EncryptionSetting _encryptionSetting;

        public Encryption(AppSetting appSetting)
        {
            _encryptionSetting = appSetting.Encryption;
        }
        
        public string GenerateHash(string value)
        {
            using var sha384 = SHA384.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(_encryptionSetting.Prefix + value);

            int iterations = 1 << _encryptionSetting.WorkFactor;

            for (int i = 0; i < iterations; i++)
                inputBytes = sha384.ComputeHash(inputBytes);

            string hash = BitConverter.ToString(inputBytes).Replace("-", "").ToLower();
            return hash;
        }

        public bool Verify(string value, string hash)
        {
            string computedHash = GenerateHash(value);
            return computedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }

        public string Encrypt(object value)
        {
            using var security = new AesSecurity(_encryptionSetting.Password, _encryptionSetting.Salt, _encryptionSetting.IterationCount);
            return security.Encrypt(value.ToString()).Replace('/', '-');
        }

        public string Decrypt(string value)
        {
            using var security = new AesSecurity(_encryptionSetting.Password, _encryptionSetting.Salt, _encryptionSetting.IterationCount);
            return security.Decrypt(value.Replace('-', '/'));
        }

        public T Decrypt<T>(string value) => (T)Convert.ChangeType(Decrypt(value), typeof(T));

        private class AesSecurity : IDisposable
        {
            private readonly Aes _aes;

            public AesSecurity(string password, string salt, int iterationCount)
            {
                _aes = Aes.Create();

                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, Encoding.ASCII.GetBytes(salt), iterationCount, HashAlgorithmName.SHA256);
                _aes.Key = rfc2898DeriveBytes.GetBytes(_aes.KeySize / 8);
                _aes.IV = rfc2898DeriveBytes.GetBytes(_aes.BlockSize / 8);
            }

            public string Encrypt(string value)
            {
                var encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using var streamWriter = new StreamWriter(cryptoStream);

                streamWriter.Write(value);
                streamWriter.Close();

                var result = memoryStream.ToArray();
                _aes.Clear();

                return Convert.ToBase64String(result);
            }

            public string Decrypt(string value)
            {
                var decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

                using var memoryStream = new MemoryStream(Convert.FromBase64String(value));
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);

                var result = streamReader.ReadToEnd();
                _aes.Clear();

                return result;
            }

            public void Dispose()
            {
                _aes.Dispose();
            }
        }
    }
}
