using BackendApiReact.Application.Contracts;
using BackendApiReact.Domain.Dto;
using BackendApiReact.Domain.Entities;
using BackendApiReact.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApiReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestoresController : ControllerBase
    {
        private readonly IGestoresAppService _gestoresAppService;
        private readonly IGestoresSupervisorAppService _gestoresSupervisorAppService;
        private readonly IGestoresEmpleadoAppService _gestoresEmpleadoAppService;

        public GestoresController(IGestoresAppService gestoresAppService, IGestoresSupervisorAppService gestoresSupervisorAppService, IGestoresEmpleadoAppService gestoresEmpleadoAppService)
        {
            this._gestoresAppService = gestoresAppService;
            this._gestoresSupervisorAppService = gestoresSupervisorAppService;
            this._gestoresEmpleadoAppService = gestoresEmpleadoAppService;

        }

        [HttpGet]
        [Route(nameof(GestoresController.GetRoles))]
        public ActionResult<IEnumerable<RolesDto>> GetRoles()
        {
            return _gestoresAppService.GetRoles();
        }

        [HttpPost]
        [Route(nameof(GestoresController.GetUser))]
        public ActionResult<bool> GetUser(GestorDto gestor)
        {
            return _gestoresAppService.Get(gestor);
        }

        [HttpPost]
        [Route(nameof(GestoresController.SaveUser))]
        public ActionResult<bool> SaveUser(GestorDto gestor)
        {
            return _gestoresAppService.SaveUser(gestor);
        }

        [HttpGet]
        [Route(nameof(GestoresController.GetTasks))]
        public ActionResult<IEnumerable<TasksDto>> GetTasks()
        {
            return _gestoresAppService.GetTasks();
        }

        [HttpPost]
        [Route(nameof(GestoresController.SaveTask))]
        public ActionResult<bool> SaveTask(TasksDto tasksDto)
        {
            return _gestoresAppService.SaveTask(tasksDto);
        }

        [HttpPost]
        [Route(nameof(GestoresController.EditTask))]
        public ActionResult<bool> EditTask(TasksDto tasksDto)
        {
            return _gestoresAppService.EditTask(tasksDto);
        }

        [HttpPost]
        [Route(nameof(GestoresController.DeleteTask))]
        public ActionResult<bool> DeleteTask(TasksDto tasksDto)
        {
            return _gestoresAppService.DeleteTask(tasksDto);
        }

        [HttpGet]
        [Route(nameof(GestoresController.GetUserAll))]
        public ActionResult<IEnumerable<GestorDto>> GetUserAll()
        {
            return _gestoresSupervisorAppService.GetUserAll();
        }

        [HttpGet]
        [Route(nameof(GestoresController.GetTasksList))]
        public ActionResult<IEnumerable<RolesDto>> GetTasksList()
        {
            return _gestoresSupervisorAppService.GetTasksList();
        }

        [HttpPost]
        [Route(nameof(GestoresController.EditTaskByUser))]
        public ActionResult<bool> EditTaskByUser(GestorDto gestorDto)
        {
            return _gestoresSupervisorAppService.EditTaskByUser(gestorDto);
        }

        [HttpGet]
        [Route(nameof(GestoresController.GetStateList))]
        public ActionResult<IEnumerable<RolesDto>> GetStateList()
        {
            return _gestoresSupervisorAppService.GetStateList();
        }

        [HttpPost]
        [Route(nameof(GestoresController.EditStateByUser))]
        public ActionResult<bool> EditStateByUser(GestorDto gestorDto)
        {
            return _gestoresEmpleadoAppService.EditStateByUser(gestorDto);
        }
    }
}
