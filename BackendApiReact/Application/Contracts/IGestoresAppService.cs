using BackendApiReact.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiReact.Application.Contracts
{
    public interface IGestoresAppService
    {
        ActionResult<bool> Get(GestorDto gestor);
        ActionResult<IEnumerable<RolesDto>> GetRoles();
        ActionResult<bool> SaveUser(GestorDto gestor);
        ActionResult<IEnumerable<TasksDto>> GetTasks();
        ActionResult<bool> SaveTask(TasksDto tasksDto);
        ActionResult<bool> EditTask(TasksDto tasksDto);
        ActionResult<bool> DeleteTask(TasksDto tasksDto);
        
    }
}
