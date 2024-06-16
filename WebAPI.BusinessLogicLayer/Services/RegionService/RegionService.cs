using WebAPI.DataAccessLayer.Repositories.RegionRepository;
using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.RegionService ;

public class RegionService : IRegionService
{
    private readonly IRegionRepository _regionRepository;

    public RegionService(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<IBaseResponse<Region>> GetRegionById(string regionId)
    {
        try
        {
            var entity = await _regionRepository.GetById(regionId);
            // TODO: проверить на null
            return new BaseResponse<Region>("Регион найден", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>(
                $"[GetRegionById] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<List<Region>>> GetRegions()
    {
        try
        {
            var entities = await _regionRepository.GetAll();
            
            return new BaseResponse<List<Region>>(
                description: "Получены существующие регионы",
                statusCode: StatusCode.Ok,
                data: entities.ToList());
        }
        catch (Exception ex)
        {
            return new BaseResponse<List<Region>>(
                description: $"[GetRegions] : {ex.Message}",
                statusCode: StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Region>> CreateRegion(Region region)
    {
        try
        {
            await _regionRepository.Create(region);
            return new BaseResponse<Region>("Регион создан", StatusCode.Ok, region);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>(
                $"[CreateRegion] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Region>> UpdateRegion(string regionId, Region region)
    {
        try
        {
            var entity = await _regionRepository.GetById(regionId);
            if (entity == null)
            {
                return new BaseResponse<Region>("Регион не найден", StatusCode.NotFound, null);
            }
            // TODO: тут должна быть реализована правильная логика обновления полей из UpdateRegionDto
            entity.Name = region.Name;
            entity = await _regionRepository.Update(entity);
            return new BaseResponse<Region>("Регион изменен", StatusCode.Ok, entity);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>(
                $"[UpdateRegion] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }

    public async Task<IBaseResponse<Region>> DeleteRegion(string regionId)
    {
        try
        {
            await _regionRepository.Delete(regionId);
            return new BaseResponse<Region>("Регион удален", StatusCode.Ok);
        }
        catch (Exception ex)
        {
            return new BaseResponse<Region>(
                $"[DeleteRegion] : {ex.Message}",
                StatusCode.InternalServerError);
        }
    }
}