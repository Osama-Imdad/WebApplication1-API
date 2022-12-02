using Microsoft.AspNetCore.Mvc;
using WebApplication1_API.Repository;
using Microsoft.AspNetCore.Http;
using DataAccessLayers;
using WebApplication1_API.VieModel;
using Microsoft.AspNetCore.Authorization;

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
        [AllowAnonymous]
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

                EmpolyeeViewModel empolyeeView = new EmpolyeeViewModel();
                if (Id>0)
                {
                    var record = await _empolyeeRepository.GetEmployee(Id);
                    if (record != null)
                    {
                        empolyeeView.Id = record.Id;
                        empolyeeView.contactNum = record.contactNum;
                        empolyeeView.name = record.name;
                        empolyeeView.department = record.department;
                        empolyeeView.Message = "Success";
                        empolyeeView.Status = true;
                    }
                    else
                    {
                        empolyeeView.Message = "False";
                        empolyeeView.Status = false;
                    }
                    return Ok(empolyeeView);
                }
                else
                {
                    empolyeeView.Message = "False";
                    empolyeeView.Status = false;
                     return Ok(empolyeeView);
                }


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
                EmpolyeeViewModel empolyeeView = new EmpolyeeViewModel();
                if (empolyee == null)
                {
                    empolyeeView.Message = "Employee not added";
                    empolyeeView.Status = false;
                    return Ok(empolyeeView);
                }
                else
                {
                    var createdEmpolyee = await _empolyeeRepository.AddEmpoloyees(empolyee);
                    if (createdEmpolyee != null)
                    {
                        empolyeeView.Id = createdEmpolyee.Id;
                        empolyeeView.department = createdEmpolyee.department;
                        empolyeeView.name = createdEmpolyee.name;
                        empolyee.designation = createdEmpolyee.designation;
                        empolyeeView.contactNum = createdEmpolyee.contactNum;
                        empolyeeView.Status = true;
                        empolyeeView.Message = "Employee added";
                        return Ok(empolyeeView);
                    }
                    else
                    {
                        empolyeeView.Message = "Something went wrong";
                        empolyeeView.Status = false;
                        return Ok(empolyeeView);
                    }
                }
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
                EmpolyeeViewModel empolyeeView = new EmpolyeeViewModel();
                if (empolyee != null)
                {
                    var employeeUpdate = await _empolyeeRepository.GetEmployee(empolyee.Id);
                    if (employeeUpdate == null)
                    {
                        empolyeeView.Message = "Empolyee not found";
                        empolyeeView.Status = false;
                        return Ok(empolyeeView);
                    }
                    else
                    {
                        var employe = await _empolyeeRepository.UpdateEmpolyee(empolyee);
                        if (employe != null)
                        {
                            empolyeeView.Message = "Empolyee record updated";
                            empolyeeView.Status = true;
                            return Ok(empolyeeView);
                        }
                        else
                        {
                            empolyeeView.Message = "Empolyee not found";
                            empolyeeView.Status = false;
                            return Ok(empolyeeView);
                        }
                    }
                }
                else
                {
                    empolyeeView.Message = "Empolyee not found";
                    empolyeeView.Status = false;
                    return Ok(empolyeeView);
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
                EmpolyeeViewModel empolyeeView = new EmpolyeeViewModel();
                if (Id>0)
                {
                    var employeeDelete = await _empolyeeRepository.GetEmployee(Id);
                    if (employeeDelete != null)
                    {
                        var delemp = await _empolyeeRepository.DeleteEmployee(Id);
                        if (delemp != null)
                        {
                            empolyeeView.Status = true;
                            empolyeeView.Message = "Employee Deleted";
                        }
                        else
                        {
                            empolyeeView.Status = false;
                            empolyeeView.Message = "Employee Not Deleted";
                        }

                        
                    }
                    else
                    {
                        empolyeeView.Status = true;
                        empolyeeView.Message = "Something went wrong";
                    }
                    return Ok(empolyeeView);
                }
                else
                {
                    empolyeeView.Status = false;
                    empolyeeView.Message = "Something went wrong";
                    return Ok(empolyeeView);
                }
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

