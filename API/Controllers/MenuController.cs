using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BLL.Servicios.Contrato;
using DTO;
using API.Utilidad;
using BLL.Servicios;
using MODEL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List(int IdUsuario, int IdUsuariorole)
        {
            var rsp = new Reponse<List<MenuDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _menuService.List(IdUsuario, IdUsuariorole);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
