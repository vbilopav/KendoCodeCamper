namespace Infrastructure.Web.Encoders
{
    public class PlainTextEncoder : IEncoder
    {
        public string Encode(string value)
        {
            return value;
        }

        public string Decode(string value)
        {
            return value;
        }
    }
}
