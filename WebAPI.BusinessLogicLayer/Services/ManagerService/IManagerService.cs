using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.ManagerService ;

public interface IManagerService
{
    Task<IBaseResponse<Manager>> GetManagerById(string managerId);
    Task<IBaseResponse<List<Manager>>> GetManagers();
    Task<IBaseResponse<Manager>> CreateManager(Manager manager);
    Task<IBaseResponse<Manager>> DeleteManager(string managerId);
    Task<IBaseResponse<Manager>> UpdateManager(Manager manager);
}