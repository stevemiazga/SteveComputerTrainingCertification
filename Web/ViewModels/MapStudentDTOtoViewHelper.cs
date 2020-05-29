using Application;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class MapStudentDTOtoViewHelper
    {

        public static List<StudentRecordViewModel> StudentRecordsDTOtoView(List<StudentRecordDTO> studentRecordsDTO)
        {
            var studentRecordViewModel = new List<StudentRecordViewModel>();
            studentRecordsDTO.ForEach(studentItem => studentRecordViewModel.Add(MapStudentRecordDTOtoView(studentItem)));
            return studentRecordViewModel;
        }

        public static StudentRecordViewModel MapStudentRecordDTOtoView(StudentRecordDTO studentItem)
        {
            var studentRecordView = new StudentRecordViewModel
            {
                StudentId = studentItem.StudentId,
                FirstName = studentItem.FirstName,
                LastName = studentItem.LastName,
                Email = studentItem.Email,
                Courses = MapStudentCoursesDTOtoView(studentItem.Courses),
                Exams = MapStudentExamsDTOtoView(studentItem.Exams),
                Certifications = MapStudentCertificationsDTOtoView(studentItem.Certifications)
            };

            return studentRecordView;
        }

        public static List<StudentCourseViewModel> MapStudentCoursesDTOtoView(List<StudentCourseDTO> studentCoursesDTO)
        {
            var studentCourseViewModel = new List<StudentCourseViewModel>();
            studentCoursesDTO.ForEach(courseItem => studentCourseViewModel.Add(MapStudentCourseDTOtoView(courseItem)));
            return studentCourseViewModel;
        }

        public static StudentCourseViewModel MapStudentCourseDTOtoView(StudentCourseDTO courseItem)
        {
            var studentCourseView = new StudentCourseViewModel
            {
                CourseId = courseItem.CourseId,
                CourseTitle = courseItem.CourseTitle
            };

            return studentCourseView;
        }

        public static List<StudentExamViewModel> MapStudentExamsDTOtoView(List<StudentExamDTO> studentExamsDTO)
        {
            var studentExamViewModel = new List<StudentExamViewModel>();
            studentExamsDTO.ForEach(examItem => studentExamViewModel.Add(MapStudentExamDTOtoView(examItem)));
            return studentExamViewModel;
        }

        public static StudentExamViewModel MapStudentExamDTOtoView(StudentExamDTO examItem)
        {
            var studentExamView = new StudentExamViewModel
            {
                ExamId = examItem.ExamId,
                ExamTitle = examItem.ExamTitle
            };

            return studentExamView;
        }

        public static List<StudentCertificationViewModel> MapStudentCertificationsDTOtoView(List<StudentCertificationDTO> studentCertificationsDTO)
        {
            var studentCertificationViewModel = new List<StudentCertificationViewModel>();
            studentCertificationsDTO.ForEach(certificationItem => studentCertificationViewModel.Add(MapStudentCertificationDTOtoView(certificationItem)));
            return studentCertificationViewModel;
        }

        public static StudentCertificationViewModel MapStudentCertificationDTOtoView(StudentCertificationDTO certificationItem)
        {
            var studentCertificationView = new StudentCertificationViewModel
            {
                CertificationId = certificationItem.CertificationId,
                CertificationTitle = certificationItem.CertificationTitle
            };

            return studentCertificationView;
        }
    }
}
