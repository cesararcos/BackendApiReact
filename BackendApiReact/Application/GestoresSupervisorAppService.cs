using BackendApiReact.Application.Contracts;
using BackendApiReact.Domain.Dto;
using BackendApiReact.Domain.Entities;
using BackendApiReact.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApiReact.Application
{
    public class GestoresSupervisorAppService : IGestoresSupervisorAppService
    {
        private readonly GestoresDbContext Context;
        public GestoresSupervisorAppService(GestoresDbContext context)
        {
            Context = context;
        }

        public ActionResult<IEnumerable<GestorDto>> GetUserAll()
        {
            IEnumerable<Users> users = Context.Users.Include(x => x.Task).Include(x => x.StateNavigation).ToList();
            IEnumerable<GestorDto> gestoresList = users.Select(u => new GestorDto()
            {
                id = u.Id.ToString(),
                usuario = u.Usuario,
                contrasena = u.Contrasena,
                rol = u.Rol,
                task = u.Task?.Description ?? string.Empty,
                state = u.StateNavigation?.Description ?? string.Empty
            }).ToList();

            return gestoresList.ToList();
        }

        public ActionResult<IEnumerable<RolesDto>> GetTasksList()
        {
            IEnumerable<Tasks> tasks = Context.Tasks.Where(x => x.Active == true).ToList();
            IEnumerable<RolesDto> rolesList = tasks.Select(u => new RolesDto()
            {
                value = u.Id,
                label = u.Description
            }).ToList();

            return rolesList.ToList();
        }

        public ActionResult<bool> EditTaskByUser(GestorDto gestorDto)
        {
            if (string.IsNullOrEmpty(gestorDto.task) || string.IsNullOrEmpty(gestorDto.state))
                return false;

            var findTask = Context?.Users?.FirstOrDefault(x => x.Id == Convert.ToInt32(gestorDto.id)) ?? null;

            if (findTask == null)
                return false;

            int taskId = Context.Tasks.FirstOrDefault(x => x.Description == gestorDto.task).Id;
            int stateId = Context.States.FirstOrDefault(x => x.Description == gestorDto.state).Id;

            Users user = new()
            {
                Id = Convert.ToInt32(gestorDto.id),
                Usuario = gestorDto.usuario,
                Contrasena = gestorDto.contrasena,
                Rol = gestorDto.rol,
                TaskId = taskId,
                State = stateId
            };

            Context.Entry(findTask).CurrentValues.SetValues(user);
            Context.Entry(findTask).State = EntityState.Modified;
            Context.SaveChanges();

            return true;
        }

        public ActionResult<IEnumerable<RolesDto>> GetStateList()
        {
            IEnumerable<States> states = Context.States.ToList();
            IEnumerable<RolesDto> rolesList = states.Select(u => new RolesDto()
            {
                value = u.Id,
                label = u.Description
            }).ToList();

            return rolesList.ToList();
        }
    }
}
