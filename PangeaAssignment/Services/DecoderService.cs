namespace PangeaAssignment.Services
{
    public class DecoderService: IDecoderService
    {
        public string Base64Decode(string encodedData)
        {
            byte[] encodedBytes;
            try
            {
                 encodedBytes = System.Convert.FromBase64String(encodedData);
            }
            catch
            {
                throw new ArgumentException("Incorect base64 value");
            }

            return System.Text.Encoding.UTF8.GetString(encodedBytes);
        }

        public string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);

        }
    }
}
