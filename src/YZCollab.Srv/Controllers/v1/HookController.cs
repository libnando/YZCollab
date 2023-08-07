using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using YZCollab.Srv.Hubs;

namespace YZCollab.Srv.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/hook")]
    public class HookController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public HookController(IHubContext<MessageHub> hubContext) {
            _hubContext = hubContext;
        }

        private record HookResponseDTO(string Message);

        [HttpPost("{message}")]
        [ProducesResponseType(typeof(HookResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(string message)
        {
            await _hubContext.Clients.All.SendAsync("RegisterLog", $"{message}");
            var response = new HookResponseDTO($"teste {message}");
            
            return Ok(response);
        }
    }
}