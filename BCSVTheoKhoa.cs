using Microsoft.Reporting.WinForms;
using System;

using System.Windows.Forms;
using UngDungQLSV.StudentDepartmentDataSetTableAdapters;


namespace UngDungQLSV
{
    public partial class BCSVTheoKhoa : Form
    {
        public BCSVTheoKhoa()
        {
            InitializeComponent();
        }


        // Hàm xử lý khi form load
        private void BCSVTheoKhoa_Load(object sender, EventArgs e)
        {
            // Gọi hàm LoadReport khi form được tải, truyền DepartmentFilter vào
            LoadReport();
        }

        // Hàm tải dữ liệu và hiển thị báo cáo
        private void LoadReport()
        {
            // Tạo instance của dataset
            var dataset = new StudentDepartmentDataSet();

            // Sử dụng TableAdapter để điền dữ liệu vào DataTable trong DataSet
            var adapter = new View_StudentByDepartmentTableAdapter();
            adapter.Fill(dataset.View_StudentByDepartment);

            // Gán DataSource cho ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dataset.Tables["View_StudentByDepartment"]));
            reportViewer1.RefreshReport();
        }
    }
}