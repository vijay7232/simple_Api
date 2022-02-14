using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoolApi.Interface;
using CoolApi.Models;

namespace CoolApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // private StudentContext _StudentContext;
        private readonly IStudentRepository  _StudentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _StudentRepository = studentRepository;
        }

        // Get
        [HttpGet]
    
        public IActionResult Get()
        {
            var students = _StudentRepository.GetStudents();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(int))]
        public IActionResult GetStudent(int id)
        {
            if(!_StudentRepository.StudentExist(id))
            {
                return NotFound();
            }

            var student = _StudentRepository.GetStudent(id);

            if(!ModelState.IsValid){
                return BadRequest();
            }

            return Ok(student);

        }

        [HttpPost]

        public IActionResult PostStudent(Student student)
        {
            _StudentRepository.PostStudent(student);

            return CreatedAtAction("postStudent", new {id = student.StudentId}, student);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateStudent(int id, Student student)
        {
            if(!_StudentRepository.StudentExist(id))
            {
                return NotFound();
            }

            if(id != student.StudentId)
            {
                return BadRequest();
            }
            
            _StudentRepository.UpdateStudent(id, student);

  
            return Ok("Successfull");
          
            
        }

        [HttpDelete]

        public IActionResult DeleteStudent(int id)
        {
            if(!_StudentRepository.StudentExist(id))
            {
                return NotFound();
            }
           bool isSuccess = _StudentRepository.DeleteStudent(id);
           if(isSuccess)
           {
               return Ok("Delete Successfully");
           }
           
           return BadRequest();

        }



/*
        // Get by id
        [HttpGet("{id}")]

        public async Task<ActionResult<Student>> GetById(int id)
        {
            var student = await _StudentContext.Students.FindAsync(id);

            if(student == null)
            {
                return NotFound();
            }
            return student;
        }


        // post request
        [HttpPost]

        public async Task<ActionResult<Student>> PostStudent(Student student_detail)
        {
            _StudentContext.Students.Add(student_detail);
            await _StudentContext.SaveChangesAsync();

            return NoContent();

            // return CreatedAtAction("getStudentDetail", new {id = student_detail.StudentId},student_detail);
        }


        // update
        [HttpPut("{id}")]

        public async Task<ActionResult<Student>> UpdateStudent(int id, Student StudentDetail)
        {
            if( id != StudentDetail.StudentId)
            {
                return BadRequest();
            }

            _StudentContext.Entry(StudentDetail).State = EntityState.Modified;
            
            try
            {
                await _StudentContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!_StudentContext.Students.Any(e => e.StudentId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // delete Method
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteStudentDetail(int id)
        {
            var student = await _StudentContext.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }

            _StudentContext.Students.Remove(student);
            await _StudentContext.SaveChangesAsync();
            return NoContent();
        }


        // destructure
        ~StudentController()
        {
            _StudentContext.Dispose();
        }
        */

    }
}