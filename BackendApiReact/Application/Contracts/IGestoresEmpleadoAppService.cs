using BackendApiReact.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiReact.Application.Contracts
{
    public interface IGestoresEmpleadoAppService
    {
        ActionResult<bool> EditStateByUser(GestorDto gestorDto);
    }
}
