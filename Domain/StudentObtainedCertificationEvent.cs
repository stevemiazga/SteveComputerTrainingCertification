using MediatR;
using System;

namespace Domain
{
    public class StudentObtainedCertificationEvent : INotification
    {

        public int StudentId { get; private set; }
        public int CertificationId { get; private set; }
        public DateTime ObtainCertificationDate { get; private set; }

        public StudentObtainedCertificationEvent(int studentId, int certificationId, DateTime obtainCertificationDate)
        {
            StudentId = studentId;
            CertificationId = certificationId;
            ObtainCertificationDate = obtainCertificationDate;
        }

    }

}