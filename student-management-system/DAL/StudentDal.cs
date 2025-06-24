using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student_management_system.Models;
namespace student_management_system.DAL
{
    internal class StudentDal
    {
        public DataTable GetAllStudents()
        {
            string query = @"SELECT student_id, first_name, last_name,
                        date_of_birth, gender, email, phone, enrollment_date
                        FROM students ORDER BY last_name, first_name";

            return DbHelper.GetData(query);
        }


        public Student GetStudentById(int studentId)
        {
            string query = @"SELECT * FROM students WHERE student_id = @studentId";

            var parameter = new MySqlParameter("@studentId", MySqlDbType.Int32)
            {
                Value = studentId
            };


            DataTable dt = DbHelper.GetData(query, parameter);
            if (dt.Rows.Count == 0) return null;


            DataRow row = dt.Rows[0];
            return new Models.Student
            {
                StudentId = Convert.ToInt32(row["student_id"]),
                FirstName = row["first_name"].ToString(),
                LastName = row["last_name"].ToString(),
                DateOfBirth = Convert.ToDateTime(row["date_of_birth"]),
                Gender = row["gender"].ToString(),
                Address = row["address"].ToString(),
                Email = row["email"].ToString(),
                Phone = row["phone"].ToString(),
                EnrollmentDate = Convert.ToDateTime(row["enrollment_date"]),
                CreatedAt = Convert.ToDateTime(row["created_at"])
            };
        }


        public int AddStudent(Student student)
        {
            string query = @"INSERT INTO students
                        (first_name, last_name, date_of_birth, gender,
                         address, email, phone, enrollment_date)
                        VALUES (@firstName, @lastName, @dob, @gender,
                                @address, @email, @phone, @enrollmentDate);
                        SELECT LAST_INSERT_ID();";


            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@firstName", MySqlDbType.VarChar) { Value = student.FirstName },
            new MySqlParameter("@lastName", MySqlDbType.VarChar) { Value = student.LastName },
            new MySqlParameter("@dob", MySqlDbType.Date) { Value = student.DateOfBirth },
            new MySqlParameter("@gender", MySqlDbType.Enum) { Value = student.Gender },
            new MySqlParameter("@address", MySqlDbType.VarChar) { Value = student.Address ?? (object)DBNull.Value },
            new MySqlParameter("@email", MySqlDbType.VarChar) { Value = student.Email ?? (object)DBNull.Value },
            new MySqlParameter("@phone", MySqlDbType.VarChar) { Value = student.Phone ?? (object)DBNull.Value },
            new MySqlParameter("@enrollmentDate", MySqlDbType.Date) { Value = student.EnrollmentDate }
            };


            return Convert.ToInt32(DbHelper.ExecuteScalar(query, parameters));
        }


        public bool UpdateStudent(Models.Student student)
        {
            string query = @"UPDATE students SET
                         first_name = @firstName,
                         last_name = @lastName,
                         date_of_birth = @dob,
                         gender = @gender,
                         address = @address,
                         email = @email,
                         phone = @phone,
                         enrollment_date = @enrollmentDate
                         WHERE student_id = @studentId";


            var parameters = new MySqlParameter[]
            {
            new MySqlParameter("@studentId", MySqlDbType.Int32) { Value = student.StudentId },
                // Other parameters same as AddStudent...
            };


            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }


        public bool DeleteStudent(int studentId)
        {
            string query = "DELETE FROM students WHERE student_id = @studentId";
            var parameter = new MySqlParameter("@studentId", MySqlDbType.Int32)
            {
                Value = studentId
            };
            return DbHelper.ExecuteNonQuery(query, parameter) > 0;
        }

    }
}
