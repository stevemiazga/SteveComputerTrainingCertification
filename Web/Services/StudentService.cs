using Application;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public StudentService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            string apiBaseURL = configuration.GetValue<string>("APIBaseURL");
            _remoteServiceBaseUrl = $"{apiBaseURL}/api/student";
            _httpClient.BaseAddress = new Uri(apiBaseURL);

        }

        public async Task<List<StudentRecordViewModel>> GetStudentRecords()
        {
            var uri = $"{ _remoteServiceBaseUrl}/students";
            var responseString = await _httpClient.GetStringAsync(uri);
            var studentRecordsDTO = JsonConvert.DeserializeObject<List<StudentRecordDTO>>(responseString);
            List<StudentRecordViewModel> studentRecordsViewModel = MapStudentDTOtoViewHelper.StudentRecordsDTOtoView(studentRecordsDTO);
            return studentRecordsViewModel;
        }

        public async Task<ObtainCertificationComponentViewModel> GetObtainCertificationNotices()
        {
            var uri = $"{ _remoteServiceBaseUrl}/notices";
            var responseString = await _httpClient.GetStringAsync(uri);
            var obtainCertifications = JsonConvert.DeserializeObject<ObtainCertificationsDTO>(responseString);
            ObtainCertificationComponentViewModel obtainCertificationViewModel = new ObtainCertificationComponentViewModel()
            {
                Count = obtainCertifications.Count,
                ObtainCertificationNotices = obtainCertifications.ObtainCertificationNotices.Select(a => new ObtainCertificationNoticeViewModel()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email,
                    CertificationCredential = a.CertificationCredential,
                    CertificationDescription = a.CertificationDescription
                    ,
                    ObtainCertificationDate = a.ObtainCertificationDate
                }).ToList()
            };

            return obtainCertificationViewModel;
        }

        public async Task<bool> CreateStudentRecord(StudentRecordViewModel studentRecordViewModel)
        {
            var uri = $"{ _remoteServiceBaseUrl}/student";
            var studentRecordContent = new StringContent(JsonConvert.SerializeObject(studentRecordViewModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, studentRecordContent);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(stream);
        }

        public async Task<StudentRecordViewModel> GetStudentRecordById(int studentId)
        {
            var uri = $"{ _remoteServiceBaseUrl}/student/{studentId}";
            var responseString = await _httpClient.GetStringAsync(uri);
            var studentRecordDTO = JsonConvert.DeserializeObject<StudentRecordDTO>(responseString);
            StudentRecordViewModel studentRecordViewModel = MapStudentDTOtoViewHelper.MapStudentRecordDTOtoView(studentRecordDTO);
            return studentRecordViewModel;
        }

        public async Task<bool> EditStudentRecord(StudentRecordViewModel studentRecordViewModel)
        {
            var uri = $"{ _remoteServiceBaseUrl}/student";
            var studentRecordContent = new StringContent(JsonConvert.SerializeObject(studentRecordViewModel), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri, studentRecordContent);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(stream);
        }

        public async Task<bool> DeleteStudentRecord(int studentId)
        {
            var uri = $"{ _remoteServiceBaseUrl}/student/{studentId}";
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(stream);
        }

    }
}
