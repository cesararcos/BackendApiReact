using BackendApiReact.Application.Contracts;
using BackendApiReact.Domain.Dto;
using BackendApiReact.Domain.Entities;
using BackendApiReact.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendApiReact.Application
{
    public class GestoresEmpleadoAppService : IGestoresEmpleadoAppService
    {
        private readonly GestoresDbContext Context;
        public GestoresEmpleadoAppService(GestoresDbContext context)
        {
            Context = context;
        }

        public ActionResult<bool> EditStateByUser(GestorDto gestorDto)
        {
            if (string.IsNullOrEmpty(gestorDto.state))
                return false;

            var findTask = Context?.Users?.FirstOrDefault(x => x.Id == Convert.ToInt32(gestorDto.id)) ?? null;

            if (findTask == null)
                return false;

            int stateId = Context.States.FirstOrDefault(x => x.Description == gestorDto.state).Id;

            Users user = new()
            {
                Id = Convert.ToInt32(gestorDto.id),
                Usuario = gestorDto.usuario,
                Contrasena = gestorDto.contrasena,
                Rol = gestorDto.rol,
                TaskId = findTask.TaskId,
                State = stateId
            };

            Context.Entry(findTask).CurrentValues.SetValues(user);
            Context.Entry(findTask).State = EntityState.Modified;
            Context.SaveChanges();

            return true;
        }
    }
}
