using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.ManagerService;
using WebAPI.Domain.Models;
using WebAPI.DTO.ManagerDTO;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly IMapper _mapper;
    private readonly ILogger<ManagerController> _logger;

    public ManagerController(IManagerService managerService, IMapper mapper, ILogger<ManagerController> logger)
    {
        _logger = logger;
        _managerService = managerService;
        _mapper = mapper;
    }
    
    [Route("GetManagerList")]
    [HttpGet]
    public async Task<IActionResult> GetManagers()
    {
        var response = await _managerService.GetManagers();
        return Ok(response.Data);
    }

    [Route("GetManager/{managerId}")]
    [HttpGet]
    public async Task<IActionResult> GetManager(string managerId)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var response = await _managerService.GetManagerById(managerId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        var managerToReturn = _mapper.Map<ManagerDto>(response.Data);

        return Ok(managerToReturn);
    }
    
    [Route("CreateManager")]
    [HttpPost]
    public async Task<IActionResult> CreateManager([FromBody] CreateManagerDto createManagerDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest("Cannot create manager");
        }
        
        var managerToCreate = _mapper.Map<Manager>(createManagerDto);
        var response = await _managerService.CreateManager(managerToCreate);
        if (response.Data == false) {
            return BadRequest("Something went wrong");
        }
        
        var managerToReturn = _mapper.Map<ManagerDto>(managerToCreate);
        return CreatedAtAction("GetManager", new { managerId = managerToCreate.Id }, managerToReturn);
    }
    
    [Route("UpdateManager/{managerId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateManager(
        [FromRoute] string managerId, 
        [FromBody] UpdateManagerDto updateManagerDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }
        
        var managerToUpdate = _mapper.Map<Manager>(updateManagerDto);
        if (managerToUpdate == null) {
            return BadRequest(ModelState);
        }
        managerToUpdate.Id = managerId;

        var response = await _managerService.UpdateManager(managerToUpdate);
        
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [Route("DeleteManager/{managerId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteManager(string managerId)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }

        var response = await _managerService.DeleteManager(managerId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return BadRequest("Something went wrong");
        }

        return NoContent();
    }
}
