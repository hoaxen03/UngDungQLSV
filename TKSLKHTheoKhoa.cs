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
using UngDungQLSV.CourseCountDataSetTableAdapters;

namespace UngDungQLSV
{
    public partial class TKSLKHTheoKhoa : Form
    {
        public TKSLKHTheoKhoa()
        {
            InitializeComponent();
        }

        private void TKSLKHTheoKhoa_Load(object sender, EventArgs e)
        {
            LoadCourseCountReport();
        }
        private void LoadCourseCountReport()
        {
            var dataset = new CourseCountDataSet();
            var adapter = new View_CourseCountByDepartmentTableAdapter();
            adapter.Fill(dataset.View_CourseCountByDepartment);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("CourseCountDataSet", dataset.Tables["View_CourseCountByDepartment"]));
            reportViewer1.RefreshReport();
        }
    }
}
