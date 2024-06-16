using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.RegionService;
using WebAPI.Domain.DTO.Region;
using WebAPI.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RegionController : ControllerBase
{
    private readonly IRegionService _regionService;
    private readonly ILogger<RegionController> _logger;

    public RegionController(IRegionService regionService, ILogger<RegionController> logger)
    {
        _logger = logger;
        _regionService = regionService;
    }
    
    [Route("GetRegionList")]
    [HttpGet]
    public async Task<List<Region>> GetRegions()
    {
        var response = await _regionService.GetRegions();
        return response.Data!;
    }

    [Route("GetRegion/{regionId}")]
    [HttpGet]
    public async Task<Region> GetRegion(string regionId)
    {
        var response = await _regionService.GetRegionById(regionId);
        return response.Data!;
    }
    
    [Route("CreateRegion")]
    [HttpPost]
    public async Task<Region> CreateRegion([FromBody] CreateRegionDto regionDto)
    {
        var response = await _regionService.CreateRegion(regionDto);
        return response.Data!;
    }
    
    [Route("UpdateRegion/{regionId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateRegion(string regionId, [FromBody] UpdateRegionDto regionDto)
    {
        var response = await _regionService.UpdateRegion(regionId, regionDto);
        // if (regionId != region.Id)
        // {
        //     return BadRequest();
        // }
        // var updatedRegion = _Regionservice.UpdateRegion(region);
        // if (updatedRegion == null)
        // {
        //     return NotFound();
        // }
        return Ok(response.Data);
        return NoContent();
    }
    
    [Route("DeleteRegion/{regionId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteRegion(string regionId)
    {
        await _regionService.DeleteRegion(regionId);
        return Ok(regionId);
    }
}
