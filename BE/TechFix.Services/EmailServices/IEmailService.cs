using System.Threading.Tasks;
using TechFix.Common;

namespace TechFix.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
