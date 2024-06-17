using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.TeamService;
using WebAPI.Domain.Models;
using WebAPI.DTO.TeamDTO;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeamService teamService, IMapper mapper, ILogger<TeamController> logger)
    {
        _logger = logger;
        _teamService = teamService;
        _mapper = mapper;
    }
    
    [Route("GetTeamList")]
    [HttpGet]
    public async Task<IActionResult> GetTeams()
    {
        var response = await _teamService.GetTeams();
        return Ok(response.Data);
    }

    [Route("GetTeam/{teamId}")]
    [HttpGet]
    public async Task<IActionResult> GetTeam(string teamId)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var response = await _teamService.GetTeamById(teamId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        var teamToReturn = _mapper.Map<TeamDto>(response.Data);

        return Ok(teamToReturn);
    }
    
    [Route("CreateTeam")]
    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto createTeamDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest("Cannot create team");
        }
        
        var teamToCreate = _mapper.Map<Team>(createTeamDto);
        var response = await _teamService.CreateTeam(teamToCreate);
        if (response.Data == false) {
            return BadRequest("Something went wrong");
        }
        
        var teamToReturn = _mapper.Map<TeamDto>(teamToCreate);
        return CreatedAtAction("GetTeam", new { teamId = teamToCreate.Id }, teamToReturn);
    }
    
    [Route("UpdateTeam/{teamId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateTeam(
        [FromRoute] string teamId, 
        [FromBody] UpdateTeamDto updateTeamDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }
        
        var teamToUpdate = _mapper.Map<Team>(updateTeamDto);
        if (teamToUpdate == null) {
            return BadRequest(ModelState);
        }
        teamToUpdate.Id = teamId;

        var response = await _teamService.UpdateTeam(teamToUpdate);
        
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [Route("DeleteTeam/{teamId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteTeam(string teamId)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }

        var response = await _teamService.DeleteTeam(teamId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return BadRequest("Something went wrong");
        }

        return NoContent();
    }
}
