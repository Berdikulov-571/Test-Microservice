using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration.Application.UseCases.Users.Commands;
using Registration.Application.UseCases.Users.Queries;

namespace Registration.Api.Controllers
{
    [ApiController]
    [Route("api/users/")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());

            return Ok(response);
        }
    }
}