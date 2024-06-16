using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.ManagerService;
using WebAPI.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ManagerController : ControllerBase
{
    private readonly IManagerService _managerService;
    private readonly ILogger<ManagerController> _logger;

    public ManagerController(IManagerService managerService, ILogger<ManagerController> logger)
    {
        _logger = logger;
        _managerService = managerService;
    }
    
    [Route("GetManagerList")]
    [HttpGet]
    public async Task<List<Manager>> GetManagers()
    {
        var response = await _managerService.GetManagers();
        return response.Data!;
    }

    [Route("GetManager/{managerId}")]
    [HttpGet]
    public async Task<Manager> GetManager(string managerId)
    {
        var response = await _managerService.GetManagerById(managerId);
        return response.Data!;
    }
    
    [Route("CreateManager")]
    [HttpPost]
    public async Task<Manager> CreateManager([FromBody] Manager manager)
    {
        // {
        //     "fullName": "Stepan",
        //     "nationality": "Россия",
        //     "position": "ФРВ"
        // }
        await _managerService.CreateManager(manager);
        return manager;
    }
    
    [Route("UpdateManager/{managerId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateManager([FromBody] Manager manager)
    {
        await _managerService.UpdateManager(manager);
        // if (managerId != manager.Id)
        // {
        //     return BadRequest();
        // }
        // var updatedManager = _Managerservice.UpdateManager(manager);
        // if (updatedManager == null)
        // {
        //     return NotFound();
        // }
        return Ok(manager);
        return NoContent();
    }
    
    [Route("DeleteManager/{managerId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteManager(string managerId)
    {
        await _managerService.DeleteManager(managerId);
        return Ok(managerId);
    }
}
