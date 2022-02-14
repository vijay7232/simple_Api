
using CoolApi.Models;
namespace CoolApi.Interface
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudents();
        Student GetStudent(int id);
        bool StudentExist(int id);
        void PostStudent(Student student);
        void UpdateStudent(int id, Student student);
        bool DeleteStudent(int id);
        bool Save();
    }
}