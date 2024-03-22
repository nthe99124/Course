using Microsoft.AspNetCore.Mvc;
using CourseProject.Model.ViewModel;

namespace CourseProject.API.Controller.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IRestOutput _res;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(IRestOutput res, IHttpContextAccessor httpContextAccessor)
        {
            _res = res;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Protected Method

        #endregion
    }
}
