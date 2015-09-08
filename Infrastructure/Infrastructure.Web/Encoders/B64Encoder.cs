using System;
using System.Text;

namespace Infrastructure.Web.Encoders
{
    public class Base64Encoder : IEncoder
    {
        public string Encode(string value)
        {
            return  Encoding.ASCII.GetString(Convert.FromBase64String(value));
        }

        public string Decode(string value)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
        }
    }
}
