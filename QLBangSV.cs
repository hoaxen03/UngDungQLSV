using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace UngDungQLSV
{

    public partial class QLBangSV : Form
    {
        public event Action StudentDataChanged;

        public QLBangSV()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                string gender = rdbMale.Checked ? "M" : "F"; // Kiểm tra giới tính
                int departmentId = Convert.ToInt32(cmbDepartment.SelectedValue); // Lấy DepartmentID từ ComboBox

                dal.AddStudent(txtFirstName.Text, txtLastName.Text, dtpDateOfBirth.Value, gender, departmentId);
                MessageBox.Show("Thêm sinh viên thành công");
                StudentDataChanged?.Invoke();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin đầu vào.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm sinh viên. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtStudentID.Text))
                {
                    MessageBox.Show("Vui lòng chọn một sinh viên để cập nhật.");
                    return;
                }

                DataAccessLayer dal = new DataAccessLayer();
                string gender = rdbMale.Checked ? "M" : "F"; // Kiểm tra giới tính
                int departmentId = Convert.ToInt32(cmbDepartment.SelectedValue); // Lấy DepartmentID

                // Cập nhật thông tin sinh viên
                dal.UpdateStudent(
                    Convert.ToInt32(txtStudentID.Text),
                    txtFirstName.Text,
                    txtLastName.Text,
                    dtpDateOfBirth.Value,
                    gender,
                    departmentId
                );

                MessageBox.Show("Cập nhật thông tin sinh viên thành công");

                // Kích hoạt sự kiện sau khi dữ liệu thay đổi
                StudentDataChanged?.Invoke();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin đầu vào.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin sinh viên. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataAccessLayer dal = new DataAccessLayer();
                    dal.DeleteStudent(Convert.ToInt32(txtStudentID.Text));
                    MessageBox.Show("Xóa sinh viên thành công");

                    // Kích hoạt sự kiện sau khi dữ liệu thay đổi
                    StudentDataChanged?.Invoke();
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Dữ liệu không hợp lệ. Vui lòng kiểm tra lại thông tin đầu vào.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa sinh viên. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QLBangSV_Load(object sender, EventArgs e)
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
