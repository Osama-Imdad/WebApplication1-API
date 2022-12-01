using System.ComponentModel.DataAnnotations;

namespace DataAccessLayers

{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
    public class DepartmentResponse<T>
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Empolyee> data { get; set; }
    }
}
