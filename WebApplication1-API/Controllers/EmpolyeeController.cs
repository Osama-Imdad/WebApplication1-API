using Microsoft.AspNetCore.Mvc;
using WebApplication1_API.Repository;
using Microsoft.AspNetCore.Http;
using DataAccessLayers;

namespace WebApplication1_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpolyeeController : ControllerBase
    {
        private readonly IEmpolyeeRepository _empolyeeRepository;
        public EmpolyeeController(IEmpolyeeRepository empolyeeRepository)
        {
            _empolyeeRepository = empolyeeRepository;
        }

        //Getting All Records 
        [HttpGet]
        public async Task<ActionResult> GetEmpolyee()
        {
            try
            {
                return Ok(await _empolyeeRepository.GetAllEmpoloyees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
        //Geting record By Id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Empolyee>> GetEmpolyee(int Id)
        {
            try

            {
                var record = await _empolyeeRepository.GetEmployee(Id);
                if (record == null)
                {
                    return NotFound();
                }
                return record;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }

        //Creating New record
        [HttpPost()]
        public async Task<ActionResult<Empolyee>> CreateEmpolyee(Empolyee empolyee)
        {
            try
            {
                if(empolyee == null)
                {
                    return BadRequest();
                }
                var createdEmpolyee= await _empolyeeRepository.AddEmpoloyees(empolyee);
                return CreatedAtAction(nameof(GetEmpolyee), new { Id = createdEmpolyee.Id }, createdEmpolyee);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
        //updating Record
        
        [HttpPost("UpdateEmployee")]
        public async Task<ActionResult<Empolyee>> UpdateEmpolyee(Empolyee empolyee)
        {
            try
            {
                if (empolyee != null)
                {
                    var employeeUpdate = await _empolyeeRepository.GetEmployee(empolyee.Id);
                    if (employeeUpdate == null)
                    {
                        return NotFound($"Empolyee not found");
                    }
                    else
                    {
                        return await _empolyeeRepository.UpdateEmpolyee(empolyee);
                    }
                }
                else
                {
                    return BadRequest("Id Mismatch");
                }
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
        //Delete Empolyee 
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<Empolyee>> DeleteEmployee(int Id)
        {
            try
            {
                var employeeDelete = await _empolyeeRepository.GetEmployee(Id);
                if(employeeDelete == null)
                {
                    return NotFound($"Empolyee Id={Id} not Found");
                }
                return await _empolyeeRepository.DeleteEmployee(Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
        [HttpGet("{search}")]
        public async Task<ActionResult<Empolyee>> Search(string name)
        {
            try
            {
              var data =await  _empolyeeRepository.SearchEmpoloyeeByName(name);
                if(data.Any())
                {
                    return Ok(data);
                }
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error In Retrving record from Db");
            }
        }
    }
}

