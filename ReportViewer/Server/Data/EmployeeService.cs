using System.Data;

namespace ReportViewer.Server.Data
{
    public class EmployeeService
    {
        public DataTable GetEmployee()
        {
            var dt = new DataTable();
            dt.Columns.Add("EmpId");
            dt.Columns.Add("EmpName");
            dt.Columns.Add("Department");
            dt.Columns.Add("Designation");
            dt.Columns.Add("JoinDate");

            DataRow row1;
            for (int i = 1; i < 50; i++)
            {
                row1 = dt.NewRow();
                row1["EmpId"] = i;
                row1["Empname"] = $"Lou {i}";
                row1["Department"] = "Information Technology";
                row1["Designation"] = "Software Engineer";
                row1["JoinDate"] = DateTime.Now.AddYears(-30).AddDays(i);
                dt.Rows.Add(row1);
            }

            return dt;
        }
    }
}
 