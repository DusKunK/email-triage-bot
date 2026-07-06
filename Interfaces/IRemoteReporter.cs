using System.Threading.Tasks;
using EmailTriageBot.Models;

namespace EmailTriageBot.Interfaces
{
    public interface IRemoteReporter
    {
        Task ReportAsync(EmailDataModel data, string context = null);
    }
}
