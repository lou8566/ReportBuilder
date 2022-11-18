using System.Data;
using System.Text;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;
using ReportViewer.Server.Data;

namespace ReportViewer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private EmployeeService _employeeService = new EmployeeService();
        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpGet]
        [Route("GetReport")]
        public IActionResult GetReport(int reportType)
        {
            var dt = new DataTable();
            dt = _employeeService.GetEmployee(); // Call GraphQL Gateway to get data.

            string mimeType = "html\text";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\Report1.rdlc";

            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("param", "RDLC Report in Blazor Web Assembly");
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsEmployee", dt);
            
            switch (reportType)
            {
                case 1:
                    // For HTML
                    var html = localReport.Execute(RenderType.Html, extension, parameter,mimeType);
                    string s1 = System.Text.Encoding.UTF8.GetString(html.MainStream);
                    return Ok(s1);
                case 2:
                    // For Excel
                    var xl = localReport.Execute(RenderType.Excel, extension, parameter, mimeType);
                    return File(xl.MainStream, "application/msexcel", "myreport.xls");
                case 3:
                    var pdf = localReport.Execute(RenderType.Pdf, extension, parameter, "");
                    return File(pdf.MainStream, "application/pdf");
                default:
                    // For Word
                    var word = localReport.Execute(RenderType.Word, extension, parameter, mimeType);
                    return File(word.MainStream, "application/msword", "myreport.doc");
            }
        }
    }
}
