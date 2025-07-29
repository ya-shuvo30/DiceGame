using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

public class HmacGenerator
{
    public static byte[] ComputeHMAC(byte[] key, byte[] message)
    {
        var hmac = new HMac(new Sha3Digest(256));
        hmac.Init(new KeyParameter(key));
        hmac.BlockUpdate(message, 0, message.Length);
        byte[] result = new byte[hmac.GetMacSize()];
        hmac.DoFinal(result, 0);
        return result;
    }

    public static string ToHex(byte[] bytes) => BitConverter.ToString(bytes).Replace("-", "");
}