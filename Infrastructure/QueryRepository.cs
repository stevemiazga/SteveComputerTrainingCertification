using Application;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class QueryRepository : IQueryRepository
    {
        private readonly QueriesConnectionString _connectionString;

        public QueryRepository(QueriesConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public StudentRecordDTO GetStudentById(int Id)
        {
            var studentRecords = GetStudentRecords(Id);
            return studentRecords.FirstOrDefault();
        }

        public List<StudentRecordDTO> GetStudentRecords(int Id = 0)
        {
            string sql = string.Empty;
            if (Id != 0)
            {
                sql = @"select * 
                            from (select StudentId,FirstName,LastName,Email from Certify_Student) a 
                            left outer join (select ct.StudentId,c.CourseId,title as CourseTitle 
                                             from Certify_CourseTaken ct inner join Certify_Course c on ct.CourseId = c.CourseId) b
                            on a.StudentId = b.StudentId
                            left outer join (select ep.StudentId,e.ExamId, examnumber +' - '+ ExamDescription as ExamTitle 
                                             from Certify_ExamPassed ep inner join Certify_Exam e on ep.ExamId = e.ExamId) c
                            on a.StudentId = c.StudentId
                            left outer join(select co.StudentId,c.CertificationId, c.CertificationCredential +' - ' + c.CertificationDescription as CertificationTitle 
                                            from Certify_CertificationObtained co inner join Certify_Certification c 
			                                on co.CertificationId = c.CertificationId) d
                            on a.StudentId = d.StudentId
                            where a.StudentId = " + Id + "";
            }
            else
            {
                sql = @"select * 
                            from (select StudentId,FirstName,LastName,Email from Certify_Student) a 
                            left outer join (select ct.StudentId,c.CourseId,title as CourseTitle 
                                             from Certify_CourseTaken ct inner join Certify_Course c on ct.CourseId = c.CourseId) b
                            on a.StudentId = b.StudentId
                            left outer join (select ep.StudentId,e.ExamId, examnumber +' - '+ ExamDescription as ExamTitle 
                                             from Certify_ExamPassed ep inner join Certify_Exam e on ep.ExamId = e.ExamId) c
                            on a.StudentId = c.StudentId
                            left outer join(select co.StudentId,c.CertificationId, c.CertificationCredential +' - ' + c.CertificationDescription as CertificationTitle 
                                            from Certify_CertificationObtained co inner join Certify_Certification c 
			                                on co.CertificationId = c.CertificationId) d
                            on a.StudentId = d.StudentId
                            order by a.FirstName + ' ' + a.LastName
                            ";
            }
            using (SqlConnection connection = new SqlConnection(_connectionString.Value))
            {
                var studentDictionary = new Dictionary<int, StudentRecordDTO>();
                var studentRecordList = connection.Query<StudentRecordDTO, StudentCourseDTO, StudentExamDTO, StudentCertificationDTO, StudentRecordDTO>(
                    sql, (student, course, exam, certification) =>
                    {
                        StudentRecordDTO studentRecord;

                        if (!studentDictionary.TryGetValue(student.StudentId, out studentRecord))
                        {
                            studentRecord = student;
                            studentRecord.Courses = new List<StudentCourseDTO>();
                            studentRecord.Exams = new List<StudentExamDTO>();
                            studentRecord.Certifications = new List<StudentCertificationDTO>();
                            studentDictionary.Add(studentRecord.StudentId, studentRecord);
                        }
                        if (course != null && !studentRecord.Courses.Any(x => x.CourseId == course.CourseId))
                            studentRecord.Courses.Add(course);
                        if (exam != null && !studentRecord.Exams.Any(x => x.ExamId == exam.ExamId))
                            studentRecord.Exams.Add(exam);
                        if (certification != null && !studentRecord.Certifications.Any(x => x.CertificationId == certification.CertificationId))
                            studentRecord.Certifications.Add(certification);
                        return studentRecord;
                    },
                    splitOn: "StudentId")
                    .Distinct()
                    .ToList();

                return studentRecordList;
            }
        }

        public ObtainCertificationsDTO GetObtainCertifications(DateTime obtainCertificationDate)
        {
            string sql = @"select FirstName,LastName,Email,CertificationCredential,CertificationDescription,ObtainCertificationDate 
                            from StudentObtainedCertificationEvent a 
                            inner join Certify_Student b on b.StudentId = a.StudentId
                            inner join Certify_Certification c on c.CertificationId = a.CertificationId
                            where convert(date,a.ObtainCertificationDate) = convert(date,@ObtainCertificationDate)";

            using (SqlConnection connection = new SqlConnection(_connectionString.Value))
            {

                var obtainCertificationNoticesDTO = connection.Query<ObtainCertificationNoticeDTO>(sql, new { ObtainCertificationDate = obtainCertificationDate }).ToList();
                var count = obtainCertificationNoticesDTO.Count();
                ObtainCertificationsDTO obtainCertificationsDTO = new ObtainCertificationsDTO()
                {
                    Count = count,
                    ObtainCertificationNotices = obtainCertificationNoticesDTO
                };
                return obtainCertificationsDTO;
            }

        }
    }

}
