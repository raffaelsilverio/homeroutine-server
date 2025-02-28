using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;


[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService){
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id){
        var user = await _userService.GetUserByIdAsync(id);
        if(user == null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody]UserUpdateRequest request){
        var success = await _userService.UpdateUser(id, request);
        if(!success) return NotFound();

        return Ok();
    }

    [HttpDelete("{id}")]
    private async Task<IActionResult> DeleteUser(Guid id) {
        var success = await _userService.DeleteUser(id);
        if(!success) return NotFound();

        return Ok();
    }
}