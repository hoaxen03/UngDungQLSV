using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UngDungQLSV
{

    public partial class QLDiemSo : Form
    {
        public event Action GradeDataChanged;

        public QLDiemSo()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                int studentId = Convert.ToInt32(txtStudentID.Text);
                int courseId = Convert.ToInt32(cmbCourse.SelectedValue); // Lấy CourseID từ ComboBox
                int grade = Convert.ToInt32(txtGrade.Text);

                dal.AddGrade(studentId, courseId, grade);
                MessageBox.Show("Thêm điểm thành công");
                GradeDataChanged?.Invoke();
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
                int enrollmentId = Convert.ToInt32(txtEnrollmentID.Text);
                int grade = Convert.ToInt32(txtGrade.Text);

                dal.UpdateGrade(enrollmentId, grade);
                MessageBox.Show("Cập nhật điểm thành công");
                GradeDataChanged?.Invoke();
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
                DataAccessLayer dal = new DataAccessLayer();
                int enrollmentId = Convert.ToInt32(txtEnrollmentID.Text);

                dal.DeleteGrade(enrollmentId);
                MessageBox.Show("Xóa điểm thành công");
                GradeDataChanged?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCourses()
        {
            DataAccessLayer dal = new DataAccessLayer();
            DataTable dtCourses = dal.GetCourses();

            // Thiết lập nguồn dữ liệu cho ComboBox
            cmbCourse.DataSource = dtCourses;
            cmbCourse.DisplayMember = "CourseName";  // Cột hiển thị
            cmbCourse.ValueMember = "CourseID";      // Cột giá trị
        }

        private void QLDiemSo_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }
    }
}
