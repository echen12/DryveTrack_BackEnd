using DryveTrack_BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DryveTrack_BackEnd.Services;
using DryveTrack_BackEnd.Data;

namespace DryveTrack_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DryveTrackAPIDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<IdentityUser> userManager, JwtService jwtService, DryveTrackAPIDBContext dbContext)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _dbContext = dbContext;
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<AuthUser>> GetUser(string username)
        {
            IdentityUser user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new AuthUser
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        [HttpPost("BearerToken")]
        public async Task<ActionResult<AuthenticationResponse>> CreateBearerToken(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var token = _jwtService.CreateToken(user);



            return Ok(new TestResponse
            {
                Token = token.Token,
                UserId = new Guid(user.Id)
            }
            );
        }

        [HttpPost]
        [Route("add-user")]
        public async Task<ActionResult<AuthUser>> PostUser(AuthUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authUser = new IdentityUser { UserName = user.UserName, Email = user.Email };

            var result = await _userManager.CreateAsync(
                authUser,
                user.Password
            );


            if (result.Succeeded)
            {
                var newUser = new Users { UserId = new Guid(authUser.Id), FirstName = user.FirstName, LastName = user.LastName };

                await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
            }

            else if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.Password = null;
            return CreatedAtAction("GetUser", new { username = user.UserName }, user);
        }


    }
}
