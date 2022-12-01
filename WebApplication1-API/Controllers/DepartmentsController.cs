using DataAccessLayers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1_API.Repository;

namespace WebApplication1_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentsController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }

        [HttpGet]
        public ActionResult GetDepartments()
        {
            try
            {
                var departments = departmentRepository.GetDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetDepartmentById(int id)
        {
            try
            {
                var departments = departmentRepository.GetDepartmentById(id);
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult AddDepartment(Departments department)
        {
            try
            {
                var data = departmentRepository.AddDepartment(department);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }


        [HttpPost]
        public ActionResult UpdateDepartment(Departments department)
        {
            try
            {
                var data = departmentRepository.UpdateDepartment(department);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                var data = departmentRepository.DeleteDepartment(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }


    }
}
