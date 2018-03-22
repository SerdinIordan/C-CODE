namespace PublicShapefileService.EmailClient.Services
{
    public interface ISmtpClient
    {
        void ReadInformationsFromWebConfig();
        void AssignInformationsToClient();
    }
}
