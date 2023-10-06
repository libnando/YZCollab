using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YZCollab.Srv.Hubs;
using YZCollab.Srv.Services;

namespace YZCollab.Srv.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/hook")]
    public class HookController : ControllerBase
    {
        private readonly IMessageHubService _messageHubService;

        public HookController(IMessageHubService messageHubService) {
            _messageHubService = messageHubService;
        }

        public record HookRequestDTO(string Message);
        public record HookResponseDTO(int Code);

        [HttpPost]
        [ProducesResponseType(typeof(HookResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(HookRequestDTO request)
        {
            await _messageHubService.RegisterLogAsync($"{request.Message}");

            var response = new HookResponseDTO(new Random().Next());
            
            return Ok(response);
        }
    }
}