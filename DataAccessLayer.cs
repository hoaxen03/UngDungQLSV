using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UngDungQLSV
{
    public class DataAccessLayer
    {
        private string connectionString = "Server=HOAXEN\\ZEROTWO;Database=StudentManagement;Integrated Security=True;TrustServerCertificate=True";

        // Thêm sinh viên
        public void AddStudent(string firstName, string lastName, DateTime dateOfBirth, string gender, int departmentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (FirstName, LastName, DateOfBirth, Gender, DepartmentID) VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @DepartmentID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Sửa thông tin sinh viên
        public void UpdateStudent(int studentId, string firstName, string lastName, DateTime dateOfBirth, string gender, int departmentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, Gender = @Gender, DepartmentID = @DepartmentID WHERE StudentID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Xóa sinh viên
        public void DeleteStudent(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE StudentID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Tìm kiếm sinh viên theo tên hoặc mã sinh viên
        public DataTable SearchAll(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, s.Gender,
                d.DepartmentName, c.CourseName, e.Grade
            FROM Students s
            LEFT JOIN Departments d ON s.DepartmentID = d.DepartmentID
            LEFT JOIN Enrollments e ON s.StudentID = e.StudentID
            LEFT JOIN Courses c ON e.CourseID = c.CourseID
            WHERE 
                s.FirstName LIKE @keyword OR 
                s.LastName LIKE @keyword OR
                s.StudentID LIKE @keyword OR
                d.DepartmentName LIKE @keyword OR
                c.CourseName LIKE @keyword OR
                e.Grade LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        // Lấy danh sách tất cả sinh viên
        public DataTable GetAllStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Lấy danh sách tất cả khoa
        public DataTable GetAllDepartments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DepartmentID, DepartmentName FROM Departments"; // Câu truy vấn lấy ID và tên khoa
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Phương thức lấy tất cả đăng ký
        public DataTable GetAllEnrollments()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Enrollments", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        // Phương thức lấy tất cả khóa học
        public DataTable GetAllCourses()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public DataTable SearchByStudentName(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, s.Gender, d.DepartmentName
            FROM Students s
            LEFT JOIN Departments d ON s.DepartmentID = d.DepartmentID
            WHERE s.FirstName LIKE @keyword OR s.LastName LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }

        public DataTable SearchByStudentID(string studentID)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students WHERE StudentID = @StudentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Sử dụng SqlParameter để tránh SQL Injection
                    command.Parameters.AddWithValue("@StudentID", studentID);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public DataTable SearchByDepartmentName(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT d.DepartmentID, d.DepartmentName
            FROM Departments d
            WHERE d.DepartmentName LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public DataTable SearchByCourseName(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT c.CourseID, c.CourseName, d.DepartmentName
            FROM Courses c
            LEFT JOIN Departments d ON c.DepartmentID = d.DepartmentID
            WHERE c.CourseName LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public DataTable SearchByGrade(string keyword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT e.StudentID, s.FirstName, s.LastName, c.CourseName, e.Grade
            FROM Enrollments e
            LEFT JOIN Students s ON e.StudentID = s.StudentID
            LEFT JOIN Courses c ON e.CourseID = c.CourseID
            WHERE e.Grade LIKE @keyword";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;
            }
        }
        public void AddDepartment(string departmentName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Departments (DepartmentName) VALUES (@DepartmentName)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentName", departmentName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateDepartment(int departmentId, string departmentName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Departments SET DepartmentName = @DepartmentName WHERE DepartmentID = @DepartmentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentName", departmentName);
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteDepartment(int departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Departments WHERE DepartmentID = @DepartmentID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddCourse(string courseName, object departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Courses (CourseName, DepartmentID) VALUES (@CourseName, @DepartmentID)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateCourse(int courseId, string courseName, object departmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Courses SET CourseName = @CourseName, DepartmentID = @DepartmentID WHERE CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@CourseName", courseName);
                    command.Parameters.AddWithValue("@DepartmentID", departmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteCourse(int courseId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void AddGrade(int studentId, int courseId, int grade)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Enrollments (StudentID, CourseID, Grade) VALUES (@StudentID, @CourseID, @Grade)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.Parameters.AddWithValue("@Grade", grade);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateGrade(int enrollmentId, int grade)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Enrollments SET Grade = @Grade WHERE EnrollmentID = @EnrollmentID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnrollmentID", enrollmentId);
                    command.Parameters.AddWithValue("@Grade", grade);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteGrade(int enrollmentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Enrollments WHERE EnrollmentID = @EnrollmentID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnrollmentID", enrollmentId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public DataTable GetCourses()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CourseID, CourseName FROM Courses";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
            }
            return dataTable;
        }
        public DataTable GetDepartments()
        {
            DataTable dt = new DataTable();
            string query = "SELECT DepartmentID, DepartmentName FROM Departments"; // Câu lệnh SQL lấy danh sách khoa

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable SearchByDepartmentName1(string departmentName)
        {
            DataTable dtStudents = new DataTable();

            string query = @"
        SELECT s.StudentID, s.FirstName, s.LastName, s.DateOfBirth, s.Gender, d.DepartmentName
        FROM Students s
        INNER JOIN Departments d ON s.DepartmentID = d.DepartmentID
        WHERE d.DepartmentName LIKE @DepartmentName";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", "%" + departmentName + "%");

                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtStudents);
                }
            }

            return dtStudents;
        }


    }
}
