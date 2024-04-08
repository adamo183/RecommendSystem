using Microsoft.AspNetCore.Mvc;

namespace RecommendSystemApi.Controllers
{
    public class DataController : Controller
    {
        public IActionResult LoadDataFromCsv()
        {
            return Ok();
        }
    }
}
