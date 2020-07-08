using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Services;
using Boticario.WebApi.ViewModel;
using Boticario.Service.Helpers;

namespace Boticario.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RevendedorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRevendedorService _service;
        public RevendedorController(IRevendedorService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] RevendedorViewModel model)
        {
            var response = await _service.InsertAsync(_mapper.Map<Revendedor>(model));
            return Ok();
        }
    }
}
