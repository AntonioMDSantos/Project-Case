using System.Threading.Tasks;

namespace CaseBack.Utils.Application.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}