using BackendApiReact.Application.Contracts;
using BackendApiReact.Domain.Dto;
using BackendApiReact.Domain.Entities;
using BackendApiReact.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BackendApiReact.Application
{
    public class GestoresAppService : IGestoresAppService
    {
        private readonly GestoresDbContext Context;
        public GestoresAppService(GestoresDbContext context)
        {
            Context = context;
        }

        public ActionResult<IEnumerable<RolesDto>> GetRoles()
        {
            IEnumerable<Roles> roles = Context.Roles.ToList();
            IEnumerable<RolesDto> rolesList = roles.Select(u => new RolesDto()
            {
                value = u.Value,
                label = u.Label
            }).ToList();
            
            return rolesList.ToList();
        }

        public ActionResult<bool> Get(GestorDto gestor)
        {
            Users? users = Context.Users.FirstOrDefault(x => x.Usuario == gestor.usuario && x.Contrasena == gestor.contrasena);

            if (users?.Id > 0)
                return true;

            return false;
        }

        public ActionResult<bool> SaveUser(GestorDto gestor)
        {
            if (string.IsNullOrEmpty(gestor.usuario) || string.IsNullOrEmpty(gestor.contrasena))
            {
                return false;
            }

            int userLast = Context?.Users?.OrderByDescending(x => x.Id)?.FirstOrDefault()?.Id ?? 0;

            //if (userLast == 0)
            //    return false;

            Users user = new()
            {
                Usuario = gestor.usuario,
                Contrasena = gestor.contrasena,
                Id = userLast + 1,
                Rol = gestor.rol
            };

            Context?.Users.Add(user);
            Context?.SaveChanges();

            return true;
        }

        public ActionResult<IEnumerable<TasksDto>> GetTasks()
        {
            IEnumerable<Tasks> tasks = Context.Tasks.ToList();
            IEnumerable<TasksDto> taskList = tasks.Select(u => new TasksDto()
            {
                Id = u.Id.ToString(),
                Description = u.Description,
                Active = u.Active
            }).ToList();

            return taskList.ToList();
        }

        public ActionResult<bool> SaveTask(TasksDto tasksDto)
        {   
            int taskLast = Context?.Tasks?.OrderByDescending(x => x.Id)?.FirstOrDefault()?.Id ?? 0;

            Tasks task = new()
            {
                Id = taskLast + 1,
                Description = tasksDto.Description,
                Active = true
            };

            Context?.Tasks.Add(task);
            Context?.SaveChanges();

            return true;
        }

        public ActionResult<bool> EditTask(TasksDto tasksDto)
        {
            var findId = Context?.Tasks?.FirstOrDefault(x => x.Id == Convert.ToInt32(tasksDto.Id)) ?? null;

            if (findId == null)
                return false;

            Tasks task = new()
            {
                Id = Convert.ToInt32(tasksDto.Id),
                Description = tasksDto.Description,
                Active = tasksDto.Active
            };

            Context.Entry(findId).CurrentValues.SetValues(task); 
            Context.Entry(findId).State = EntityState.Modified;
            Context.SaveChanges();

            return true;
        }

        public ActionResult<bool> DeleteTask(TasksDto tasksDto)
        {
            var findId = Context?.Tasks?.FirstOrDefault(x => x.Id == Convert.ToInt32(tasksDto.Id)) ?? null;

            if (findId == null)
                return false;

            Tasks task = new()
            {
                Id = Convert.ToInt32(tasksDto.Id),
                Description = tasksDto.Description,
                Active = false
            };

            

            Context.Entry(findId).CurrentValues.SetValues(task);
            Context.Entry(findId).State = EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
        
        
    }
}
