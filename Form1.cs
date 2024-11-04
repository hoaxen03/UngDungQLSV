using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace UngDungQLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string currentTableName;

        private void UpdateTableName(string tableName)
        {
            currentTableName = tableName;
            lblTableName.Text = "Quản lý: " + currentTableName; // Hiển thị tên bảng trên label
        }

        private void LoadTableData()
        {
            DataAccessLayer dal = new DataAccessLayer();
            DataTable dt = new DataTable(); // Khởi tạo dt với DataTable

            switch (cmbTables.SelectedItem.ToString())
            {
                case "Students":
                    dt = dal.GetAllStudents(); // Phương thức lấy tất cả sinh viên
                    UpdateTableName("Sinh viên"); // Cập nhật tên bảng
                    break;
                case "Departments":
                    dt = dal.GetAllDepartments(); // Phương thức lấy tất cả khoa
                    UpdateTableName("Khoa"); // Cập nhật tên bảng
                    break;
                case "Courses":
                    dt = dal.GetAllCourses(); // Phương thức lấy tất cả khóa học
                    UpdateTableName("Khóa học"); // Cập nhật tên bảng
                    break;
                case "Enrollments":
                    dt = dal.GetAllEnrollments(); // Phương thức lấy tất cả đăng ký
                    UpdateTableName("Đăng ký"); // Cập nhật tên bảng
                    break;
                default:
                    dt = new DataTable(); // Khởi tạo một DataTable trống
                    break;
            }

            dataGridView.DataSource = dt; // Gán DataTable cho DataGridView
            DataTable dtDepartments = dal.GetDepartments();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thêm các bảng vào ComboBox
            cmbTables.Items.Add("Students");
            cmbTables.Items.Add("Departments");
            cmbTables.Items.Add("Courses");
            cmbTables.Items.Add("Enrollments");
            cmbSearchCriteria.Items.Add("Tên");
            cmbSearchCriteria.Items.Add("Mã sinh viên");
            cmbSearchCriteria.Items.Add("Khoa");
            cmbSearchCriteria.Items.Add("Khóa học");
            cmbSearchCriteria.Items.Add("Điểm");

            cmbSearchCriteria.SelectedIndex = 0; // Chọn mặc định là tiêu chí đầu tiên

            cmbTables.SelectedIndex = 0; // Mặc định chọn Students
            LoadTableData(); // Tải dữ liệu cho bảng được chọn
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTableData(); // Tải lại dữ liệu cho bảng được chọn
        }
        private void btnManageStudents_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng hiện tại có phải là bảng "Sinh viên" không
            if (currentTableName != "Sinh viên")
            {
                MessageBox.Show("Vui lòng chọn bảng 'Sinh viên' trước khi sử dụng chức năng này.", "Lỗi truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không cho phép truy cập form quản lý sinh viên nếu không phải bảng sinh viên
            }

            // Nếu đúng là bảng sinh viên, mở form quản lý sinh viên
            QLBangSV studentForm = new QLBangSV();
            studentForm.StudentDataChanged += LoadTableData; // Đăng ký sự kiện
            studentForm.Show();
        }
        private void btnManageDepartments_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng hiện tại có phải là bảng "Khoa" không
            if (currentTableName != "Khoa")
            {
                MessageBox.Show("Vui lòng chọn bảng 'Khoa' trước khi sử dụng chức năng này.", "Lỗi truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không cho phép truy cập form quản lý khoa nếu không phải bảng khoa
            }

            // Nếu đúng là bảng khoa, mở form quản lý khoa
            QLBangKhoa departmentForm = new QLBangKhoa();
            departmentForm.DepartmentDataChanged += LoadTableData; // Đăng ký sự kiện
            departmentForm.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text;
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string searchCriteria = cmbSearchCriteria.SelectedItem.ToString();
                DataAccessLayer dal = new DataAccessLayer();
                DataTable dt = null;

                switch (searchCriteria)
                {
                    case "Tên":
                        dt = dal.SearchByStudentName(keyword);
                        break;
                    case "Mã sinh viên":
                        dt = dal.SearchByStudentID(keyword);
                        break;
                    case "Khoa":
                        dt = dal.SearchByDepartmentName(keyword);
                        break;
                    case "Khóa học":
                        dt = dal.SearchByCourseName(keyword);
                        break;
                    case "Điểm":
                        dt = dal.SearchByGrade(keyword);
                        break;
                    default:
                        MessageBox.Show("Vui lòng chọn tiêu chí tìm kiếm hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Kiểm tra kết quả tìm kiếm
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào cho từ khóa \"" + keyword + "\" với tiêu chí \"" + searchCriteria + "\".", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dataGridView1.DataSource = dt; // Hiển thị kết quả tìm kiếm trong DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnManageCourses_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng hiện tại có phải là bảng "Khóa học" không
            if (currentTableName != "Khóa học")
            {
                MessageBox.Show("Vui lòng chọn bảng 'Khóa học' trước khi sử dụng chức năng này.", "Lỗi truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không cho phép truy cập form quản lý khóa học nếu không phải bảng khóa học
            }

            // Nếu đúng là bảng khóa học, mở form quản lý khóa học
            QLBangKhoaHoc courseForm = new QLBangKhoaHoc();
            courseForm.CourseDataChanged += LoadTableData; // Đăng ký sự kiện
            courseForm.Show();
        }
        private void btnManageGrades_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng hiện tại có phải là bảng "Điểm số" không
            if (currentTableName != "Đăng ký")
            {
                MessageBox.Show("Vui lòng chọn bảng 'Điểm số' trước khi sử dụng chức năng này.", "Lỗi truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Không cho phép truy cập form quản lý điểm số nếu không phải bảng điểm số
            }

            // Nếu đúng là bảng điểm số, mở form quản lý điểm số
            QLDiemSo gradeForm = new QLDiemSo();
            gradeForm.GradeDataChanged += LoadTableData; // Đăng ký sự kiện
            gradeForm.Show();
        }


        private void btnShowReport_Click(object sender, EventArgs e)
        {
            BCSVTheoKhoa reportForm = new BCSVTheoKhoa();

            reportForm.Show();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            BCDiemTheoKhoaHoc reportForm = new BCDiemTheoKhoaHoc();

            reportForm.Show();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            BCSLSVTheoKhoa reportForm = new BCSLSVTheoKhoa();

            reportForm.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            TKSLKHTheoKhoa reportForm = new TKSLKHTheoKhoa();

            reportForm.Show();



        }
    }
}

