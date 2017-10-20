using System.Net.Http;
using System.Web.Http;
using CodeFirst.Models;
using CodeFirst.Models.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CodeFirst.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private readonly ApplicationUserManager _appUserManager = null;
        private readonly ApplicationRoleManager _appRoleManager;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }
        protected ApplicationUserManager AppUserManager
        {
            get
            {
                return _appUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public BaseApiController(ApplicationRoleManager appRoleManager)
        {
            _appRoleManager = appRoleManager;
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory != null) return _modelFactory;
                _modelFactory = new ModelFactory(Request, AppUserManager);
                return _modelFactory;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
    }
}
