using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLogicLayer.Services.PlayerService;
using WebAPI.Domain.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger)
    {
        _logger = logger;
        _playerService = playerService;
    }
    
    [Route("GetPlayerList")]
    [HttpGet]
    public async Task<List<Player>> GetPlayers()
    {
        var response = await _playerService.GetPlayers();
        return response.Data!;
    }

    [Route("GetPlayer/{playerId}")]
    [HttpGet]
    public async Task<Player> GetPlayer(string playerId)
    {
        var response = await _playerService.GetPlayerById(playerId);
        return response.Data!;
    }
    
    [Route("CreatePlayer")]
    [HttpPost]
    public async Task<Player> CreatePlayer([FromBody] Player player)
    {
        // {
        //     "fullName": "Stepan",
        //     "nationality": "Россия",
        //     "position": "ФРВ"
        // }
        await _playerService.CreatePlayer(player);
        return player;
    }
    
    [Route("UpdatePlayer/{playerId}")]
    [HttpPut]
    public async Task<IActionResult> UpdatePlayer([FromBody] Player player)
    {
        await _playerService.UpdatePlayer(player);
        // if (playerId != player.Id)
        // {
        //     return BadRequest();
        // }
        // var updatedPlayer = _playerService.UpdatePlayer(player);
        // if (updatedPlayer == null)
        // {
        //     return NotFound();
        // }
        return Ok(player);
        return NoContent();
    }
    
    [Route("DeletePlayer/{playerId}")]
    [HttpDelete]
    public async Task<IActionResult> DeletePlayer(string playerId)
    {
        await _playerService.DeletePlayer(playerId);
        return Ok(playerId);
    }
}