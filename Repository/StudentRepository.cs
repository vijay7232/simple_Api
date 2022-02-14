using CoolApi.Models;
using CoolApi.Interface;
using Microsoft.EntityFrameworkCore;
namespace CoolApi.Repository
{

    public class StudentRepository : IStudentRepository
    {
        private readonly StudentContext _studentContxt; 
        public StudentRepository(StudentContext context)
        {
            _studentContxt = context;
        }

        // public bool CreateStudent(int studentId, string firstName, string lastName, string city, string state)
        // {
        //     return false;
        // }


        public ICollection<Student> GetStudents()
        {
            return _studentContxt.Students.OrderBy(p => p.StudentId).ToList();
        }

        public Student GetStudent(int id)
        {
            var student = _studentContxt.Students.Where(p => p.StudentId == id).FirstOrDefault();
            if(student is not null)
            {
                return student;
            }else{
                return null!;
            }
  
        }

        // public Student GetStudent(string firstName)
        // {
        //     return _studentContxt.Students.Where(p => p.FirstName == firstName).FirstOrDefault();
        // }

        public void PostStudent(Student student)
        {
            _studentContxt.Students.Add(student);
            Save();
        }

        public void UpdateStudent(int id, Student student)
        {
         
            _studentContxt.Entry(student).State = EntityState.Modified;
            Save();

           
        }

        public bool DeleteStudent(int id)
        {
            var student = _studentContxt.Students.Find(id);
            if(student is not null)
            {
                _studentContxt.Remove(student);
                 Save();
                 return true;
            }
            else{
                return false;
            }
            
        }

        public bool StudentExist(int id)
        {
            return _studentContxt.Students.Any(p=> p.StudentId == id);
        }

        public bool Save()
        {
            var saved = _studentContxt.SaveChanges();
            return saved >0?true:false;
        }
    }
}