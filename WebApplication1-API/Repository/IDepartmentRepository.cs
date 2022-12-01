using DataAccessLayers;
using WebApplication1_API.VieModel;

namespace WebApplication1_API.Repository
{
    public interface IDepartmentRepository
    {
        List<Departments> GetDepartments();
        DepartmentsViewMode GetDepartmentById(int id);
        DepartmentsViewMode AddDepartment(Departments Department);
        DepartmentsViewMode UpdateDepartment(Departments Department);
        bool DeleteDepartment(int id);
    }
}
