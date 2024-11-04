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
using UngDungQLSV.StudentCountDataSetTableAdapters;

namespace UngDungQLSV
{
    public partial class BCSLSVTheoKhoa : Form
    {
        public BCSLSVTheoKhoa()
        {
            InitializeComponent();
        }

        private void BCSLSVTheoKhoa_Load(object sender, EventArgs e)
        {

            LoadStudentCountReport();
        }
        private void LoadStudentCountReport()
        {
            var dataset = new StudentCountDataSet();
            var adapter = new View_StudentCountByDepartmentTableAdapter();
            adapter.Fill(dataset.View_StudentCountByDepartment);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("StudentCountDataSet", dataset.Tables["View_StudentCountByDepartment"]));
            reportViewer1.RefreshReport();
        }

    }
}
