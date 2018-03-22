using PublicShapefileService.Common.Models;

namespace PublicShapefileService.EmailClient.Services
{
    public interface IMailMessage
    {
        void SendMail(ShapefileRequest solicitant);
    }
}
