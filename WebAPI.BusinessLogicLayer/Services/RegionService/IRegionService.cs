using WebAPI.Domain.Models;
using WebAPI.Domain.Response;

namespace WebAPI.BusinessLogicLayer.Services.RegionService ;

public interface IRegionService
{
    Task<IBaseResponse<Region>> GetRegionById(string regionId);
    Task<IBaseResponse<List<Region>>> GetRegions();
    Task<IBaseResponse<bool>> CreateRegion(Region region);
    Task<IBaseResponse<Region>> DeleteRegion(string regionId);
    Task<IBaseResponse<Region>> UpdateRegion(Region region);
}