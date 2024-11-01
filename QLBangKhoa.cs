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
    public partial class QLBangKhoa : Form
    {
        public QLBangKhoa()
        {
            InitializeComponent();
        }
        public event Action DepartmentDataChanged;
        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khoa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataAccessLayer dal = new DataAccessLayer();
                dal.AddDepartment(txtDepartmentName.Text);
                MessageBox.Show("Thêm khoa thành công.");
                DepartmentDataChanged?.Invoke();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm khoa. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdateDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text))
                {
                    MessageBox.Show("Vui lòng chọn một khoa để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDepartmentName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khoa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataAccessLayer dal = new DataAccessLayer();
                dal.UpdateDepartment(Convert.ToInt32(txtDepartmentID.Text), txtDepartmentName.Text);
                MessageBox.Show("Cập nhật thông tin khoa thành công.");
                DepartmentDataChanged?.Invoke();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi cập nhật thông tin khoa. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDepartmentID.Text))
                {
                    MessageBox.Show("Vui lòng chọn một khoa để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc muốn xóa khoa này?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataAccessLayer dal = new DataAccessLayer();
                    dal.DeleteDepartment(Convert.ToInt32(txtDepartmentID.Text));
                    MessageBox.Show("Xóa khoa thành công.");
                    DepartmentDataChanged?.Invoke();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi xóa khoa. Vui lòng kiểm tra lại dữ liệu và thử lại.\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi không xác định: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
