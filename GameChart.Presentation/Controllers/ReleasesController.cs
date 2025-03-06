using Entities.Exceptions.BadRequest;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/releases")]
    [ApiController]
    public class ReleasesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ReleasesController(IServiceManager service) => _service = service;


    }
}