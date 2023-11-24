using Jose;
using System.Security.Cryptography.X509Certificates;

namespace employee_raffles.Services;

internal static class TokenService
{
    public static string Encode(Dictionary<string, object> payload)
    {
        var privateKey = new X509Certificate2("Helpers/Keys/key.p12", "123456").GetRSAPrivateKey();
        string token = JWT.Encode(payload, privateKey, JwsAlgorithm.RS256);
        return token;
    }
    public static string Encode(string payload)
    {
        var privateKey = new X509Certificate2("Helpers/Keys/key.p12", "123456").GetRSAPrivateKey();
        string token = JWT.Encode(payload, privateKey, JwsAlgorithm.RS256);
        return token;
    }

    public static string Decode(string token)
    {
        var privateKey = new X509Certificate2("Helpers/Keys/key.p12", "123456").GetRSAPrivateKey();
        string json = JWT.Decode(token, privateKey, JwsAlgorithm.RS256);
        return json;
    }

    public static bool Verify(string token, string secret)
    {
        var privateKey = new X509Certificate2("Helpers/Keys/key.p12", "123456").GetRSAPrivateKey();
        string data = JWT.Decode(token, privateKey, JwsAlgorithm.RS256);
        if (secret == data)
            return true;
        return false;
    }
}
