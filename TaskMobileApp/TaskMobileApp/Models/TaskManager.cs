using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace TaskMobileApp.Models
{
    class TaskManager
    {
        private ObservableCollection<Task> taskList = new ObservableCollection<Task>();
        public ObservableCollection<Task> Tasks { get { return taskList; } }

        private string hectagonapikey = TaskMobileApp.Secrets.hectagonapikey;

        public async System.Threading.Tasks.Task CompleteTaskAsync(int TaskIDToComplete)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hectagonapi.azure-api.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", hectagonapikey);

                var method = new HttpMethod("PATCH");

                string apiOperationString = String.Concat("task/complete/", TaskIDToComplete.ToString());

                var request = new HttpRequestMessage(method, apiOperationString);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
        }

        public async System.Threading.Tasks.Task RefreshTasksAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hectagonapi.azure-api.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", hectagonapikey);

                HttpResponseMessage response = await client.GetAsync("task");
                ObservableCollection<Task> newTaskList = new ObservableCollection<Task>();
                if (response.IsSuccessStatusCode)
                {
                    newTaskList = await response.Content.ReadAsAsync<ObservableCollection<Task>>();
                    foreach(var t in newTaskList)
                    {
                        taskList.Add(t);
                    }
                }
            }
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Task TaskToAdd)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hectagonapi.azure-api.net");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Ocp-Apim-Trace", "true");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", hectagonapikey);

                HttpResponseMessage response = await client.PostAsJsonAsync("task", TaskToAdd);
                if (response.IsSuccessStatusCode)
                {
                    taskList.Add(TaskToAdd);
                    return;
                }
            }
        }
    }
}
