using DataAccessLayers;
using WebApplication1_API.DataContext;
using WebApplication1_API.VieModel;

namespace WebApplication1_API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        
        private readonly Application_DBContext dbContext;

        public DepartmentRepository(Application_DBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public List<Departments> GetDepartments()
        {

            var list = dbContext.Departments.ToList();
            return list;
        }
        public DepartmentsViewMode GetDepartmentById(int id)
        {
            DepartmentsViewMode departmentsViewMode = new DepartmentsViewMode();


            var department = dbContext.Departments.FirstOrDefault(x => x.DepartmentId == id);
            if (department != null)
            {
                departmentsViewMode.DepartmentId= department.DepartmentId;
                departmentsViewMode.DepartmentName = department.DepartmentName;
                departmentsViewMode.Message = "Success";
                departmentsViewMode.Status = true;
               
            }
            else
            {
                departmentsViewMode.Message = "Not Success";
                departmentsViewMode.Status = false;


            }
            return departmentsViewMode;
        }
        public DepartmentsViewMode AddDepartment(Departments Department)
        {
            DepartmentsViewMode departmentsViewMode = new DepartmentsViewMode();

            
            if (Department != null)
            {
                dbContext.Departments.Add(Department);
                dbContext.SaveChanges();

                departmentsViewMode.Message = "Success";
                departmentsViewMode.Status = true;

            }
            else
            {
                departmentsViewMode.Message = "Not Success";
                departmentsViewMode.Status = false;


            }
            return departmentsViewMode;

            
        }
        public DepartmentsViewMode UpdateDepartment(Departments Department)
        {
            
            DepartmentsViewMode departmentsViewMode = new DepartmentsViewMode();


            if (Department != null)
            {
                dbContext.Departments.Update(Department);
                dbContext.SaveChanges();

                departmentsViewMode.Message = "Success";
                departmentsViewMode.Status = true;

            }
            else
            {
                departmentsViewMode.Message = "Not Success";
                departmentsViewMode.Status = false;


            }
            return departmentsViewMode;
        }
        public bool DeleteDepartment(int id)
        {
            Departments departments= dbContext.Departments.Find(id);
            if (departments == null)
            {
                return false;
            }

            dbContext.Departments.Remove(departments);
            dbContext.SaveChanges();
            return true;
        }

    }
}
