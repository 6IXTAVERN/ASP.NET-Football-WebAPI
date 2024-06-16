using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.TeamService;
using WebAPI.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeamService teamService, ILogger<TeamController> logger)
    {
        _logger = logger;
        _teamService = teamService;
    }
    
    [Route("GetTeamList")]
    [HttpGet]
    public async Task<List<Team>> GetTeams()
    {
        var response = await _teamService.GetTeams();
        return response.Data!;
    }

    [Route("GetTeam/{teamId}")]
    [HttpGet]
    public async Task<Team> GetTeam(string teamId)
    {
        var response = await _teamService.GetTeamById(teamId);
        return response.Data!;
    }
    
    [Route("CreateTeam")]
    [HttpPost]
    public async Task<Team> CreateTeam([FromBody] Team team)
    {
        // {
        //     "fullName": "Stepan",
        //     "nationality": "Россия",
        //     "position": "ФРВ"
        // }
        await _teamService.CreateTeam(team);
        return team;
    }
    
    [Route("UpdateTeam/{teamId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateTeam([FromRoute] string teamId, [FromBody] Team team)
    {
        await _teamService.UpdateTeam(team);
        // if (teamId != team.Id)
        // {
        //     return BadRequest();
        // }
        // var updatedTeam = _Teamservice.UpdateTeam(team);
        // if (updatedTeam == null)
        // {
        //     return NotFound();
        // }
        return Ok(team);
        return NoContent();
    }
    
    [Route("DeleteTeam/{teamId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeam(string teamId)
    {
        await _teamService.DeleteTeam(teamId);
        return Ok(teamId);
    }
}
