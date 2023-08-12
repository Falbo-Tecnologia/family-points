namespace Core.Interfaces.Helpers;

public interface IEncryption
{
    string GenerateHash(string value);
    bool Verify(string value, string hash);
    string Encrypt(object value);
    string Decrypt(string value);
    T Decrypt<T>(string value);
}
