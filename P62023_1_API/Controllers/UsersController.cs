using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P62023_1_API.Models;
using P62023_1_API.Attributes;
using P62023_1_API.ModelsDTOs;
using P62023_1_API.Tools;

namespace P62023_1_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class UsersController : ControllerBase
    {
        private readonly P620231_AutoAppoContext _context;
        public Crypto MyCryto { get; set; }
        public UsersController(P620231_AutoAppoContext context)
        {
            _context = context;
            MyCryto = new Crypto();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("ValidateUserLogin")]
        public async Task<ActionResult<User>> ValidateUserLogin(string pUserName, string pPassword)
        {
            string EncryptedPassword = MyCryto.EncriptarEnUnSentido(pPassword);
            var user = await _context.Users.SingleOrDefaultAsync(e =>
                e.Email == pUserName && e.LoginPassword == EncryptedPassword);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // GET: api/Users/GetUserData
        [HttpGet("GetUserData")]
        public ActionResult<IEnumerable <UserDTO>> GetUserData(string email)
        {
            var query = (from u in _context.Users
                         join ur in _context.UserRoles on u.UserRoleId equals ur.UserRoleId
                         join us in _context.UserStatuses on u.UserStatusId equals us.UserStatusId
                         where u.Email == email && u.UserStatusId != 2
                         select new
                         {
                             idusuario = u.UserId,
                             nombre = u.Name,
                             correo = u.Email,
                             telefono = u.PhoneNumber,
                             contra = u.LoginPassword,
                             cedula = u.CardId,
                             direccion = u.Address,
                             idrol = ur.UserRoleId,
                             roldescripcion = ur.UserRoleDescription,
                             idstatus = us.UserStatusId,
                             estadodesc = us.UserStatuDescription
                         }).ToList();
            List<UserDTO> list = new List<UserDTO>();
            foreach (var item in query)
            {
                UserDTO NewItem = new UserDTO()
                {
                    IDUsuario = item.idusuario,
                    Nombre = item.nombre,
                    Correo = item.correo,
                    Cedula = item.cedula,
                    NumeroTelefono = item.telefono,
                    Contrasenna = item.contra,
                    Direccion = item.direccion,
                    IDRol = item.idrol,
                    RolDescripcion = item.roldescripcion,
                    IDEstado = item.idstatus,
                    EstadoDescripcion = item.estadodesc
                };
                list.Add(NewItem);
            }
            if (list == null)
            {
                return NotFound();
            }
            return list;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO user)
        {
            if (id != user.IDUsuario)
            {
                return BadRequest();
            }

            string Password = "";
            if (user.Contrasenna.Length <= 60)
            {
                Password = MyCryto.EncriptarEnUnSentido(user.Contrasenna);
            }
            else 
            {
                Password = user.Contrasenna;
            }

            User NuevoUsuario = new()
            {
                UserId = user.IDUsuario,
                Name = user.Nombre,
                CardId = user.Cedula,
                PhoneNumber = user.NumeroTelefono,
                Address = user.Direccion,
                LoginPassword = Password,
                Email = user.Correo,
                UserRoleId = user.IDRol,
                UserStatusId = user.IDEstado,
                Appointments = null, 
                UserRole = null,
                UserStatus = null
            };

            _context.Entry(NuevoUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string EncriptedPassword = MyCryto.EncriptarEnUnSentido(user.LoginPassword);
            user.LoginPassword = EncriptedPassword;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
