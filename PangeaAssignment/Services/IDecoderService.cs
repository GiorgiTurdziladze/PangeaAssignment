namespace PangeaAssignment.Services
{
    public interface IDecoderService
    {
        string Base64Decode(string encodedData);
        string Base64Encode(string text);
    }
}
