using System;
using System.Collections.Generic;

namespace Application
{
    public interface IQueryRepository
    {
        List<StudentRecordDTO> GetStudentRecords(int Id = 0);
        StudentRecordDTO GetStudentById(int Id);
        ObtainCertificationsDTO GetObtainCertifications(DateTime obtainCertificationDate);
    }
}