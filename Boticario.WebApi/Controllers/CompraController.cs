using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boticario.Domain.Entities;
using Boticario.Domain.Interfaces.Services;
using Boticario.WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace Boticario.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICompraService _service;
        public CompraController(ICompraService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ComprasViewModel model)
        {
            var response = await _service.InsertAsync(_mapper.Map<Compra>(model), model.CpfRevendedor);
            return Ok();
        }

        [HttpGet]
        public async Task<IEnumerable<ComprasCalcViewModel>> List()
        {
            return _mapper.Map<IEnumerable<ComprasCalcViewModel>>(await _service.ListAsync());
        }

        [HttpGet("CashBack")]
        public async Task<IActionResult> CashBack(string cpf)
        {
            return Ok(await _service.GetCashBackAsync(cpf));
        }
    }
}
