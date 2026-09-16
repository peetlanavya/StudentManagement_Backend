using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentRepo.Data;
using StudentRepo.Interfaces;
using StudentRepo.models;

namespace StudentRepo.Services
{
    public class StudentManager : IStudentManager
    {
        private readonly StudentDbContext _context;

        public StudentManager(StudentDbContext context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            try
            {
                if (student == null)
                    throw new CustomException("Student object is null");

                if (string.IsNullOrWhiteSpace(student.Name))
                    throw new CustomException("Name cannot be empty");

                if (student.Age <= 0)
                    throw new CustomException("Age must be greater than 0");

                if (string.IsNullOrWhiteSpace(student.Email))
                    throw new CustomException("Email cannot be empty");

                if (string.IsNullOrWhiteSpace(student.PhoneNumber))
                    throw new CustomException("Phone number cannot be empty");

                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_AddStudent @Name, @Age, @Email, @PhoneNumber",
                    new SqlParameter("@Name", student.Name),
                    new SqlParameter("@Age", student.Age),
                    new SqlParameter("@Email", student.Email),
                    new SqlParameter("@PhoneNumber", student.PhoneNumber)
                );
            }
            catch (SqlException ex)
            {
                throw new CustomException("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new CustomException("Unexpected error: " + ex.Message);
            }
        }

        // 🔥🔥🔥 FIX IS HERE 🔥🔥🔥
        public List<Student> GetStudents()
        {
            try
            {
                var result = _context.Students
                    .FromSqlRaw("EXEC dbo.sp_GetStudents")
                    .AsNoTracking()
                    .ToList();

                // ✅ MANUAL MAPPING → removes EF column dependency
                return result.Select(s => new Student
                {
                    Id = s.Id,
                    Name = s.Name,
                    Age = s.Age,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error fetching students: " + ex.Message);
            }
        }

        public Student GetStudentById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException("Invalid student ID");

                var student = _context.Students
                    .FromSqlRaw("EXEC dbo.sp_GetStudentById @Id",
                        new SqlParameter("@Id", id))
                    .AsEnumerable()
                    .FirstOrDefault();

                return student;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error fetching student by ID: " + ex.Message);
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                if (student == null || student.Id <= 0)
                    throw new CustomException("Invalid student data");

                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_UpdateStudent @Id, @Name, @Age, @Email, @PhoneNumber",
                    new SqlParameter("@Id", student.Id),
                    new SqlParameter("@Name", student.Name ?? (object)DBNull.Value),
                    new SqlParameter("@Age", student.Age),
                    new SqlParameter("@Email", student.Email ?? (object)DBNull.Value),
                    new SqlParameter("@PhoneNumber", student.PhoneNumber ?? (object)DBNull.Value)
                );
            }
            catch (Exception ex)
            {
                throw new CustomException("Error updating student: " + ex.Message);
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                if (id <= 0)
                    throw new CustomException("Invalid student ID");

                _context.Database.ExecuteSqlRaw(
                    "EXEC dbo.sp_DeleteStudent @Id",
                    new SqlParameter("@Id", id)
                );
            }
            catch (SqlException ex)
            {
                throw new CustomException("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new CustomException("Unexpected error: " + ex.Message);
            }
        }
    }
}