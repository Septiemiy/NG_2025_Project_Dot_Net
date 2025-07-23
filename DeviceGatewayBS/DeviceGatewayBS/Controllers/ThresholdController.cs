using DAL_Core.Enums;
using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/threshold")]
[ApiController]
public class ThresholdController : Controller
{
    private readonly IThresholdService _thresholdService;
    private readonly ILogger<ThresholdController> _logger;

    public ThresholdController(IThresholdService thresholdService, ILogger<ThresholdController> logger)
    {
        _thresholdService = thresholdService;
        _logger = logger;
    }

    [HttpPost("createThreshold")]
    public async Task<IActionResult> CreateThresholdAsync([FromBody] ThresholdDTO thresholdDTO)
    {
        if(thresholdDTO == null)
        {
            _logger.LogError("[ERROR][DeviceGateway]Threshold data is null.");
            return BadRequest("Threshold data is null.");
        }

        if (!await _thresholdService.isThresholdExists(thresholdDTO))
        {
            _logger.LogInformation("[INFO][DeviceGateway]Creating new threshold with name: {ThresholdCondition}", thresholdDTO.ThresholdCondition);
            if (await _thresholdService.SendThresholdAsync(thresholdDTO))
            {
                thresholdDTO.Status = ConditionsStatus.Online;
                var thresholdId = await _thresholdService.AddThresholdAsync(thresholdDTO);

                if (thresholdId == Guid.Empty)
                {
                    _logger.LogError("[ERROR][DeviceGateway]An error occurred while saving the threshold.");
                    return StatusCode(500, "An error occurred while saving the threshold.");
                }

                _logger.LogInformation("[INFO][DeviceGateway]Threshold stored in database with ID: {ThresholdId}", thresholdId);

                return Ok(thresholdDTO);
            }
            else
            {
                _logger.LogError("[ERROR][DeviceGateway]Failed to send threshold to devices.");
                return StatusCode(500, "Failed to send threshold to devices.");
            }
        }
        else
        {
            _logger.LogError("[ERROR][DeviceGateway]Threshold already exists with name and value: {ThresholdCondition}, {Value}", thresholdDTO.ThresholdCondition, thresholdDTO.Value);
            return StatusCode(500, "Threshold already exists.");
        }
    }
}
