namespace WebApi.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Security;
    using WebApi.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            this._sessionService = sessionService;
        }
        
        [HttpPost]
        public IActionResult LogIn([FromBody] LoginDTO loginDTO)
        {
            var credential = loginDTO.CreateCredentials();

            string newToken = this._sessionService.Auth(credential);
            if (string.IsNullOrEmpty(newToken)) 
                return BadRequest(newToken); 
                
            return Ok(newToken); 
        }
    }
}