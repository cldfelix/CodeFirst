using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using CodeFirst.Models;
using CodeFirst.Models.Infrastructure;

namespace CodeFirst.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : BaseApiController
    {
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(AppUserManager.Users.ToList().Select(u => TheModelFactory.Create(u)));
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await AppUserManager.FindByIdAsync(id);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                Nome = createUserModel.Nome,
                Sobrenome = createUserModel.Sobrenome,
                Nivel = 3,
                DataDeCriacao = DateTime.Now.Date,
            };

            var addUserResult = await AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            var locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }


        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {

            var appUser = await AppUserManager.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await AppUserManager.GetRolesAsync(appUser.Id);

            var rolesNotExists = rolesToAssign.Except(AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Any())
            {

                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            var removeResult = await AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            var addResult = await AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (addResult.Succeeded) return Ok();
            ModelState.AddModelError("", "Failed to add user roles");
            return BadRequest(ModelState);
        }

        public UsuariosController(ApplicationRoleManager appRoleManager) : base(appRoleManager)
        {
        }
    }
}
