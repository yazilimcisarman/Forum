using Forum.Application.Dtos.PostDtos;
using Forum.Application.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Forum.MVC.Controllers
{
    //[Authorize]
    public class ProfilesController : BaseController
    {
        private readonly HttpClient _httpClient;
        public ProfilesController(IHttpClientFactory httpClient, IHttpContextAccessor httpContextAccessor) :base(httpContextAccessor)
        {
            _httpClient = httpClient.CreateClient("api");
        }
        public async Task<IActionResult> Index()
        {
            // Cookie'den token'ı al
            var token = tkn;

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts");
            }

            // HttpRequestMessage ile Authorization header'ı ekle
            var request = new HttpRequestMessage(HttpMethod.Get, "profile?test=12");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<string>>(json);

            if (posts != null && posts.Status)
            {
                return View();
            }

            return RedirectToAction("Login", "Accounts");
        }
        public async Task<IActionResult> Topics()
        { 
            // Cookie'den token'ı al
            var token = tkn;

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts");
            }

            // HttpRequestMessage ile Authorization header'ı ekle
            var request = new HttpRequestMessage(HttpMethod.Get, "profile/posts");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultPostDto>>> (json);

            if (posts != null && posts.Status)
            {
                return View(posts.Data);
            }

            return RedirectToAction("Login", "Accounts");
        }
        public async Task<IActionResult> Comments()
        {
            // Cookie'den token'ı al
            var token = tkn;

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Accounts");
            }

            // HttpRequestMessage ile Authorization header'ı ekle
            var request = new HttpRequestMessage(HttpMethod.Get, "profile/comments");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var posts = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<List<ResultPostDto>>>(json);

            if (posts != null && posts.Status)
            {
                return View(posts.Data);
            }

            return RedirectToAction("Login", "Accounts");
        }
    }
}
