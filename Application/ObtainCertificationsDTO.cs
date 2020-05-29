using System;
using System.Collections.Generic;

namespace Application
{
    public class ObtainCertificationsDTO
    {
        public int Count { get; set; }
        public List<ObtainCertificationNoticeDTO> ObtainCertificationNotices { get; set; }
    }

    public class ObtainCertificationNoticeDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CertificationCredential { get; set; }
        public string CertificationDescription { get; set; }
        public DateTime ObtainCertificationDate { get; set; }
    }
}