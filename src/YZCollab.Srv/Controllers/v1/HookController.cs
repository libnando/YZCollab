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

        public record HookRequestDTO(string Message);
        public record HookResponseDTO(int Code);

        [HttpPost]
        [ProducesResponseType(typeof(HookResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(HookRequestDTO request)
        {
            await _hubContext.Clients.All.SendAsync("RegisterLog", $"{request.Message}");
            var response = new HookResponseDTO(new Random().Next());
            
            return Ok(response);
        }
    }
}