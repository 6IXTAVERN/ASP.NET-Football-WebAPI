using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.PlayerService;
using WebAPI.Domain.Models;
using WebAPI.DTO.PlayerDTO;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(IPlayerService playerService, IMapper mapper, ILogger<PlayerController> logger)
    {
        _logger = logger;
        _playerService = playerService;
        _mapper = mapper;
    }
    
    [Authorize]
    [Route("GetPlayerList")]
    [HttpGet]
    public async Task<IActionResult> GetPlayers()
    {
        var response = await _playerService.GetPlayers();
        return Ok(response.Data);
    }

    [Authorize]
    [Route("GetPlayer/{playerId}")]
    [HttpGet]
    public async Task<IActionResult> GetPlayer(string playerId)
    {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        var response = await _playerService.GetPlayerById(playerId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        var playerToReturn = _mapper.Map<PlayerDto>(response.Data);

        return Ok(playerToReturn);
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("CreatePlayer")]
    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto createPlayerDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest("Cannot create player");
        }
        
        var playerToCreate = _mapper.Map<Player>(createPlayerDto);
        var response = await _playerService.CreatePlayer(playerToCreate);
        if (response.Data == false) {
            return BadRequest("Something went wrong");
        }
        
        var playerToReturn = _mapper.Map<PlayerDto>(playerToCreate);
        return CreatedAtAction("GetPlayer", new { playerId = playerToCreate.Id }, playerToReturn);
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("UpdatePlayer/{playerId}")]
    [HttpPut]
    public async Task<IActionResult> UpdatePlayer(
        [FromRoute] string playerId, 
        [FromBody] UpdatePlayerDto updatePlayerDto)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }
        
        var playerToUpdate = _mapper.Map<Player>(updatePlayerDto);
        if (playerToUpdate == null) {
            return BadRequest(ModelState);
        }
        playerToUpdate.Id = playerId;

        var response = await _playerService.UpdatePlayer(playerToUpdate);
        
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return NotFound();
        }
        
        return NoContent();
    }
    
    [Authorize(Roles = "Administrator")]
    [Route("DeletePlayer/{playerId}")]
    [HttpDelete]
    public async Task<IActionResult> DeletePlayer(string playerId)
    {
        if (ModelState.IsValid == false) {
            return BadRequest(ModelState);
        }

        var response = await _playerService.DeletePlayer(playerId);
        if (response.StatusCode == Domain.Response.StatusCode.NotFound) {
            return BadRequest("Something went wrong");
        }

        return NoContent();
    }
}