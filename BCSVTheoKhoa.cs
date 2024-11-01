using Microsoft.Reporting.WinForms;
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
    public partial class BCSVTheoKhoa : Form
    {
        public BCSVTheoKhoa()
        {
            InitializeComponent();
        }

        // Biến này để lưu tên khoa cần lọc
        public string DepartmentFilter { get; set; }

        // Hàm xử lý khi form load
        private void BCSVTheoKhoa_Load(object sender, EventArgs e)
        {
            // Gọi hàm LoadReport khi form được tải, truyền DepartmentFilter vào
            LoadReport(DepartmentFilter);
        }

        // Hàm tải dữ liệu và hiển thị báo cáo
        private void LoadReport(string departmentFilter)
        {
            // Cấu hình ReportViewer
            reportViewer1.ProcessingMode = ProcessingMode.Local;
            reportViewer1.LocalReport.ReportPath = "D:\\UngDungQLSV\\StudentDepartmentReport.rdlc"; // Đường dẫn tới file RDLC

            // Lấy dữ liệu sinh viên theo tên khoa
            if (!string.IsNullOrEmpty(departmentFilter))
            {
                DataTable studentData = GetStudentsByDepartment1(departmentFilter);
                ReportDataSource rdsStudent = new ReportDataSource("StudentByDepartmentDataSet", studentData); // Tên dataset phải khớp với tên trong file RDLC

                // Xóa dữ liệu cũ và thêm dữ liệu mới vào báo cáo
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(rdsStudent);
            }

            // Làm mới báo cáo để hiển thị dữ liệu mới
            reportViewer1.RefreshReport();
        }

        // Hàm lấy dữ liệu sinh viên theo khoa từ lớp DataAccessLayer
        private DataTable GetStudentsByDepartment1(string departmentName)
        {
            DataAccessLayer dal = new DataAccessLayer();
            return dal.SearchByDepartmentName1(departmentName); // Gọi phương thức tìm kiếm đã có
        }
    }
}