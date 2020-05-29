using Application;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
    public class CertificationService : ICertificationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public CertificationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            string apiBaseURL = configuration.GetValue<string>("APIBaseURL");
            _remoteServiceBaseUrl = $"{apiBaseURL}/api/certification";
            _httpClient.BaseAddress = new Uri(apiBaseURL);
        }

        public async Task<AvailableCertificatonsViewModel> GetAvailableCertifications()
        {
            var uri = $"{ _remoteServiceBaseUrl}/available";
            var responseString = await _httpClient.GetStringAsync(uri);
            var availableCertifications = JsonConvert.DeserializeObject<AvailableCertificationsDTO>(responseString);
            AvailableCertificatonsViewModel availableCertificatonsView = new AvailableCertificatonsViewModel()
            {
                Courses = MapStudentDTOtoViewHelper.MapStudentCoursesDTOtoView(availableCertifications.Courses),
                Exams = MapStudentDTOtoViewHelper.MapStudentExamsDTOtoView(availableCertifications.Exams),
                Certifications = MapStudentDTOtoViewHelper.MapStudentCertificationsDTOtoView(availableCertifications.Certifications)
            };

            return availableCertificatonsView;
        }
    }
}
