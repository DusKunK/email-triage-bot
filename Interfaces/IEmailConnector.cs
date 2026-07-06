using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailTriageBot.Interfaces
{
    public interface IEmailConnector
    {
        Task<IEnumerable<Models.EmailDataModel>> FetchNewEmailsAsync();
    }
}
