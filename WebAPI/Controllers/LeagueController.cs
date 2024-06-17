using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.LeagueService;
using WebAPI.Domain.Models;
using WebAPI.DTO.LeagueDTO;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LeagueController : ControllerBase
{
    private readonly ILeagueService _leagueService;
    private readonly IMapper _mapper;
    private readonly ILogger<LeagueController> _logger;

    public LeagueController(ILeagueService leagueService, IMapper mapper, ILogger<LeagueController> logger)
    {
        _logger = logger;
        _leagueService = leagueService;
        _mapper = mapper;
    }
    
    [Route("GetLeagueList")]
    [HttpGet]
    public async Task<IActionResult> GetLeagues([FromQuery] string? contextSearch = null)
    {
        var response = await _leagueService.GetLeagues(contextSearch);
        return Ok(response.Data);
    }

    [Route("GetLeague/{leagueId}")]
    [HttpGet]
    public async Task<IActionResult> GetLeague(string leagueId)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var response = await _leagueService.GetLeagueById(leagueId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        var leagueToReturn = _mapper.Map<LeagueDto>(response.Data);

        return Ok(leagueToReturn);
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("CreateLeague")]
    [HttpPost]
    public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto createLeagueDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest("Cannot create league");
        }
        
        var leagueToCreate = _mapper.Map<League>(createLeagueDto);
        var response = await _leagueService.CreateLeague(leagueToCreate);
        if (response.Data == false) {
            return BadRequest("Something went wrong");
        }
        
        var leagueToReturn = _mapper.Map<LeagueDto>(leagueToCreate);
        return CreatedAtAction("GetLeague", new { leagueId = leagueToCreate.Id }, leagueToReturn);
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("UpdateLeague/{leagueId}")]
    [HttpPut]
    public async Task<IActionResult> UpdateLeague(
        [FromRoute] string leagueId, 
        [FromBody] UpdateLeagueDto updateLeagueDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }
        
        var leagueToUpdate = _mapper.Map<League>(updateLeagueDto);
        if (leagueToUpdate == null) {
            return BadRequest(ModelState);
        }
        leagueToUpdate.Id = leagueId;

        var response = await _leagueService.UpdateLeague(leagueToUpdate);
        
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("DeleteLeague/{leagueId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteLeague(string leagueId)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }

        var response = await _leagueService.DeleteLeague(leagueId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return BadRequest("Something went wrong");
        }

        return NoContent();
    }
}
