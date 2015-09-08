namespace Infrastructure.Web.Encoders
{
    public interface IEncoder
    {
        string Encode(string value);
        string Decode(string value);
    }
}
