using JMeDQgddW9.Security;
using Microsoft.AspNetCore.Mvc;

namespace JMeDQgddW9.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiAuthorize]
    public abstract class BaseController : ControllerBase
    {
    }
}