using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Classes;           
using System.Threading.Tasks;

namespace WebApplication1.Controllers    
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DBContext _context;

        public UsersController(DBContext context)
        {
            _context = context;
        }

        [HttpGet("users")]                     
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return Ok(await _context.User.AsNoTracking().ToListAsync());
        }

        [HttpGet("abonents")]                 
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Abonent>>> GetAllAbonents()
        {
            return Ok(await _context.Abonent.AsNoTracking().ToListAsync());
        }
        [HttpGet("packages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Abonent>>> GetAllPackages()
        {
            return Ok(await _context.Package.AsNoTracking().ToListAsync());
        }
        [HttpGet("agreements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Abonent>>> GetAllAgreements()
        {
            return Ok(await _context.Agreement.AsNoTracking().ToListAsync());
        }
        [HttpGet("history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Abonent>>> GetAllhistorys()
        {
            return Ok(await _context.History.AsNoTracking().ToListAsync());
        }

        [HttpPost("pusers")]
        public async Task<ActionResult> InsertUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);

          
            await _context.SaveChangesAsync();

           
            return Ok(await _context.User.AsNoTracking().ToListAsync());
        }

        [HttpPut("{id}")]

        public async Task<ActionResult> UpdateUser(int id, [FromBody] User updateuser)
        {
            if(id!=updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.User_LastName = updateuser.User_LastName;
            user.User_MidName = updateuser.User_MidName;
            user.User_Name = updateuser.User_Name;
            user.Role = updateuser.Role;
            user.Hire_date = updateuser.Hire_date;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteUser(int id, [FromBody] User updateuser)
        {
            if (id != updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.User.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpPost("pabonent")]
        public async Task<ActionResult> InsertAbonent([FromBody] Abonent user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Abonent.Add(user);


            await _context.SaveChangesAsync();


            return Ok(await _context.History.AsNoTracking().ToListAsync());
        }

        [HttpPut("p{id}")]

        public async Task<ActionResult> UpdateAbonent(int id, [FromBody] Abonent updateuser)
        {
            if (id != updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Abonent.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Abonent_Login = updateuser.Abonent_Login;
            user.Abonent_MidName = updateuser.Abonent_MidName;
            user.Abonent_LastName = updateuser.Abonent_LastName;
            user.Abonent_name = updateuser.Abonent_name;
            user.Abonent_Idcard = updateuser.Abonent_Idcard;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("p{id}")]

        public async Task<ActionResult> DeleteAbonent(int id, [FromBody] Abonent updateuser)
        {
            if (id != updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Abonent.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Abonent.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
        private async Task<bool> UserExists(int id)
        {
            return await _context.User.AnyAsync(e => e.ID == id);
        }

        [HttpPost("ppackage")]
        public async Task<ActionResult> InsertPackage([FromBody] Package user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Package.Add(user);


            await _context.SaveChangesAsync();


            return Ok(await _context.History.AsNoTracking().ToListAsync());
        }

        [HttpPut("t{id}")]

        public async Task<ActionResult> UpdatePackage(int id, [FromBody] Package updateuser)
        {
            if (id != updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Package.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.Package_price = updateuser.Package_price;
            user.Package_num = updateuser.Package_num;
            user.Package_name = updateuser.Package_name;
            user.Package_descriptiion = updateuser.Package_descriptiion;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        [HttpDelete("t{id}")]

        public async Task<ActionResult> DeleteUser(int id, [FromBody] Package updateuser)
        {
            if (id != updateuser.ID)
            {
                return BadRequest("ID missmatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _context.Package.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.Package.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
    }
}