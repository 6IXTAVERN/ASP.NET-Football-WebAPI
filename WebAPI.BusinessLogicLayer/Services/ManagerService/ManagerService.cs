using WebAPI.DataAccessLayer.Repositories.ManagerRepository;
using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.ManagerService ;

public class ManagerService : IManagerService
{
    private readonly IManagerRepository _managerRepository;

    public ManagerService(IManagerRepository managerRepository)
    {
        _managerRepository = managerRepository;
    }

    public async Task<IBaseResponse<Manager>> GetManagerById(string managerId)
    {
        try
        {
            var entity = await _managerRepository.GetById(managerId);
            // TODO: проверить на null
            return new BaseResponse<Manager>("Тренер найден", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Manager>(
                $"[GetManagerById] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<Manager>>> GetManagers(string? contextSearch = null)
    {
        try
        {
            var entities = await _managerRepository.GetAll(contextSearch);
            
            return new BaseResponse<List<Manager>>(
                description: "Получены существующие тренеры",
                statusCode: StatusCode.Ok,
                data: entities.ToList());
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Manager>>(
                description: $"[GetManagers] : {ex.Message}",
                statusCode: StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<bool>> CreateManager(Manager manager)
    {
        try
        {
            await _managerRepository.Create(manager);
            return new BaseResponse<bool>("Тренер создан", StatusCode.Ok, true);
        }
        catch (Exception ex)
        {
            return new BaseResponse<bool>(
                $"[CreateManager] : {ex.Message}",
                StatusCode.InternalServerError, false);
        }
    }

    public async Task<IBaseResponse<Manager>> UpdateManager(Manager manager)
    {
        try
        {
            await _managerRepository.Update(manager);
            return new BaseResponse<Manager>("Тренер изменен", StatusCode.Ok, manager);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Manager>(
                $"[UpdateManager] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Manager>> DeleteManager(string managerId)
    {
        try
        {
            await _managerRepository.Delete(managerId);
            return new BaseResponse<Manager>("Тренер удален", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Manager>(
                $"[DeleteManager] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
}
