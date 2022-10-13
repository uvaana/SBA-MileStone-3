using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StuAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachController : ControllerBase
    {
        private readonly StuDBContext _dbContext;
        public TeachController(StuDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Data = await _dbContext.TblStus.ToListAsync();
            return Ok(Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TblStu newTblDatum)
        {
            _dbContext.TblStus.Add(newTblDatum);
            await _dbContext.SaveChangesAsync();
            return Ok(newTblDatum);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var DataId = await _dbContext
            .TblStus.Where(_ => _.Id == id)

            .FirstOrDefaultAsync();
            return Ok(DataId);
        }


        [HttpPut]
        public async Task<IActionResult> Put(TblStu dataToUpdate)
        {
            _dbContext.TblStus.Update(dataToUpdate);
            await _dbContext.SaveChangesAsync();
            return Ok(dataToUpdate);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dataToDelete = await _dbContext.TblStus.FindAsync(id);
            if (dataToDelete == null)
            {
                return NotFound();
            }
            _dbContext.TblStus.Remove(dataToDelete);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
