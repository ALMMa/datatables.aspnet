using Bogus;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using DataTables.AspNet.Samples.Net60.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DataTables.AspNet.Samples.Net60.Controllers
{
    public class SampleData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }
    }

    [Route("")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly List<SampleData> _sampleData;

        static HomeController()
        {
            Randomizer.Seed = new Random(26354);
            int counter = 0;

            var sampleGenerator = new Faker<SampleData>()
                .StrictMode(true)
                .RuleFor(sample => sample.Id, faker => counter++)
                .RuleFor(sample => sample.FirstName, faker => faker.Name.FirstName())
                .RuleFor(sample => sample.LastName, faker => faker.Name.LastName())
                .RuleFor(sample => sample.Position, faker => faker.Name.JobTitle())
                .RuleFor(sample => sample.Office, faker => faker.Address.Country())
                .RuleFor(sample => sample.StartDate, faker => faker.Person.DateOfBirth.Date)
                .RuleFor(sample => sample.Salary, faker => faker.Finance.Amount(7000, 25000));

            _sampleData = sampleGenerator.Generate(1000);
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("page-data")]
        public IActionResult PageData(IDataTablesRequest dataTablesRequest)
        {
            var data = _sampleData.Skip(dataTablesRequest.Start).Take(dataTablesRequest.Length);
            var response = DataTablesResponse<SampleData>.Create(dataTablesRequest, 100, 10, data);
            return Json(response);
        }
    }
}