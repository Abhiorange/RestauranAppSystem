using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Net.Http;
using ViewModels.Models;

namespace RestaurantApp.Areas.orders.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        Uri baseAddress = new Uri("http://localhost:7189/api");

        public OrdersController(ILogger<OrdersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial for a commercial license
        }
        [HttpGet]
        public IActionResult OrdersDetails(int orderid = 0)
        {
            List<OrdersVm> model = new List<OrdersVm>();
            List<ProductODVm> model1 = new List<ProductODVm>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetOrdersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<OrdersVm>>(userJson);
            }

            HttpResponseMessage response1 = _client.GetAsync(_client.BaseAddress + "/Orders/GetDataById/" + orderid).Result;
            if (response1.IsSuccessStatusCode)
            {
                var userJson1 = response1.Content.ReadAsStringAsync().Result;
                model1 = JsonConvert.DeserializeObject<List<ProductODVm>>(userJson1);
            }

            var tuple = new Tuple<List<OrdersVm>, List<ProductODVm>>(model, model1);
            return View(tuple);
        }



        [HttpGet]
        public IActionResult SalesReport(int orderid = 0)
        {
            List<OrdersVm> model = new List<OrdersVm>();
           

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetOrdersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<OrdersVm>>(userJson);
            }

           

            var orders = new List<OrdersVm>(model);
            return View(orders);
        }



        // ...

        [HttpGet]
        public IActionResult ExportToExcel(int orderid = 0)
        {
            List<OrdersVm> model = new List<OrdersVm>();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Orders/GetOrdersInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                var userJson = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<List<OrdersVm>>(userJson);
            }

            // Create a new Excel package
            ExcelPackage excelPackage = new ExcelPackage();

            // Add a new worksheet to the Excel package
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Orders");

            // Set headers
            worksheet.Cells[1, 1].Value = "Order ID";
            worksheet.Cells[1, 2].Value = "Total Price";
            worksheet.Cells[1, 3].Value = "Table No";
            worksheet.Cells[1, 4].Value = "Order Time";

            // Populate data
            for (int i = 0; i < model.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = model[i].orderid;
                worksheet.Cells[i + 2, 2].Value = model[i].Totalprice;
                worksheet.Cells[i + 2, 3].Value = model[i].tableno;
                worksheet.Cells[i + 2, 4].Value = model[i].ordertime.ToString("dd-MM-yyyy HH:mm:ss");
            }

            // Set the content type and file name
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Orders.xlsx";

            // Convert the Excel package to a byte array
            byte[] fileBytes = excelPackage.GetAsByteArray();

            // Return the Excel file
            return File(fileBytes, contentType, fileName);
        }



    }

}



