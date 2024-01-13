using Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

 

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public async Task<IActionResult> MainView()
        {
            List<TaskModel> tasks = new List<TaskModel>();

            var client = new HttpClient();
            await Task.Run(() =>
            {
                var response = client.GetAsync("https://localhost:7244/api/Tasks").Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObject = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<List<TaskModel>>(dataObject);

                    foreach(var item in result)
                    {
                        if(item != null)
                        {
                            tasks.Add(item);
                        }
                    }
                }
            });

            return View(tasks);
        }

        public async Task<IActionResult> CreateTask(TaskModel task)
        {
            task.TaskId = Guid.NewGuid().ToString();
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(task);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8,"application/json");
            var response = await client.PostAsync("https://localhost:7244/api/Tasks", httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("data posted");
            }
            return RedirectToAction("MainView");
        }

        public async Task<IActionResult> UpdateTask(string taskId, TaskModel updatedTask)
        {
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(updatedTask);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"https://localhost:7244/api/Tasks/{taskId}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data updated");
                return RedirectToAction("MainView");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Task not found");
                return NotFound();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode);
            }
        }

        public async Task<IActionResult> DeleteTask(string taskId)
        {
            var client = new HttpClient();
            var response = await client.DeleteAsync($"https://localhost:7244/api/Tasks/{taskId}");

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data deleted");
                return RedirectToAction("MainView");
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                Console.WriteLine("Task not found");
                return NotFound();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return StatusCode((int)response.StatusCode);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
