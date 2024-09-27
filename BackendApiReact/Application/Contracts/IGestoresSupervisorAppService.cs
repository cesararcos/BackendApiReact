using BackendApiReact.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiReact.Application.Contracts
{
    public interface IGestoresSupervisorAppService
    {
        ActionResult<IEnumerable<GestorDto>> GetUserAll();
        ActionResult<IEnumerable<RolesDto>> GetTasksList();
        ActionResult<bool> EditTaskByUser(GestorDto gestorDto);
        ActionResult<IEnumerable<RolesDto>> GetStateList();
        
    }
}
