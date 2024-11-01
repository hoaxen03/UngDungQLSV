using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngDungQLSV
{
    public partial class QLBangKhoaHoc : Form
    {
        public QLBangKhoaHoc()
        {
            InitializeComponent();
        }
        public event Action CourseDataChanged;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                // Thêm khóa học vào cơ sở dữ liệu
                dal.AddCourse(txtCourseName.Text, cmbDepartment.SelectedValue);
                MessageBox.Show("Thêm khóa học thành công");
                CourseDataChanged?.Invoke(); // Gọi sự kiện sau khi thêm thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                // Cập nhật thông tin khóa học
                dal.UpdateCourse(Convert.ToInt32(txtCourseID.Text), txtCourseName.Text, cmbDepartment.SelectedValue);
                MessageBox.Show("Cập nhật khóa học thành công");
                CourseDataChanged?.Invoke(); // Gọi sự kiện sau khi cập nhật thành công
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa khóa học này?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataAccessLayer dal = new DataAccessLayer();
                    dal.DeleteCourse(Convert.ToInt32(txtCourseID.Text));
                    MessageBox.Show("Xóa khóa học thành công");
                    CourseDataChanged?.Invoke(); // Gọi sự kiện sau khi xóa thành công
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void QLBangKhoaHoc_Load(object sender, EventArgs e)
        {
            LoadDepartments(); // Nạp danh sách khoa khi form được mở
        }

        private void LoadDepartments()
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                DataTable departments = dal.GetAllDepartments(); // Lấy tất cả các khoa từ cơ sở dữ liệu

                // Gán dữ liệu vào ComboBox
                cmbDepartment.DataSource = departments;
                cmbDepartment.DisplayMember = "DepartmentName"; // Hiển thị tên khoa
                cmbDepartment.ValueMember = "DepartmentID"; // Lấy DepartmentID
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi tải danh sách khoa. Vui lòng thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
