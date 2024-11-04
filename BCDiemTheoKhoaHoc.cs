using Microsoft.Reporting.WinForms;
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
    public partial class BCDiemTheoKhoaHoc : Form
    {
        public BCDiemTheoKhoaHoc()
        {
            InitializeComponent();
        }
        // Hàm xử lý khi form load
        private void BCDiemTheoKhoaHoc_Load(object sender, EventArgs e)
        {
            // Gọi hàm LoadReport khi form được tải, truyền DepartmentFilter vào
            LoadReport();
        }

        // Hàm tải dữ liệu và hiển thị báo cáo
        private void LoadReport()
        {
            var dataset = new DataSet1();
            var adapter = new DataSet1TableAdapters.View_AverageGradeByCourseTableAdapter();
            adapter.Fill(dataset.View_AverageGradeByCourse);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("StudentAverageGradeDataSet", dataset.Tables["View_AverageGradeByCourse"]));
            reportViewer1.RefreshReport();
        }
    }
}
