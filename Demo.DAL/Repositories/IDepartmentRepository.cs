
namespace Demo.DAL.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll(bool withTracking = false);
        Department? GetById(int id);
        int Insert(Department department);
        int Remove(Department department);
        int Update(Department department);
    }
}