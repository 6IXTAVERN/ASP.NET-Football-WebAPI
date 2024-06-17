using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.RegionService;
using WebAPI.Domain.Models;
using WebAPI.DTO.RegionDTO;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RegionController : ControllerBase
{
    private readonly IRegionService _regionService;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionController> _logger;

    public RegionController(IRegionService regionService, IMapper mapper, ILogger<RegionController> logger)
    {
        _logger = logger;
        _regionService = regionService;
        _mapper = mapper;
    }
    
    [Route("GetRegionList")]
    [HttpGet]
    public async Task<IActionResult> GetRegions()
    {
        var response = await _regionService.GetRegions();
        return Ok(response.Data);
    }

    [Route("GetRegion/{regionId}")]
    [HttpGet]
    public async Task<IActionResult> GetRegion(string regionId)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var response = await _regionService.GetRegionById(regionId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        var regionToReturn = _mapper.Map<RegionDto>(response.Data);

        return Ok(regionToReturn);
    }
    
    [Route("CreateRegion")]
    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto createRegionDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest("Cannot create region");
        }
        
        var regionToCreate = _mapper.Map<Region>(createRegionDto);
        var response = await _regionService.CreateRegion(regionToCreate);
        if (response.Data == false) {
            return BadRequest("Something went wrong");
        }
        
        var regionToReturn = _mapper.Map<RegionDto>(regionToCreate);
        return CreatedAtAction("GetRegion", new { regionId = regionToCreate.Id }, regionToReturn);
    }
    
    [Route("UpdateRegion/{regionId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateRegion(
        [FromRoute] string regionId, 
        [FromBody] UpdateRegionDto updateRegionDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }
        
        var regionToUpdate = _mapper.Map<Region>(updateRegionDto);
        if (regionToUpdate == null) {
            return BadRequest(ModelState);
        }
        regionToUpdate.Id = regionId;

        var response = await _regionService.UpdateRegion(regionToUpdate);
        
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [Route("DeleteRegion/{regionId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteRegion(string regionId)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }

        var response = await _regionService.DeleteRegion(regionId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return BadRequest("Something went wrong");
        }

        return NoContent();
    }
}
