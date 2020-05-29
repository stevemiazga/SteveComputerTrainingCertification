using System;
using System.Collections.Generic;

namespace Web.ViewModels
{
    public class ObtainCertificationComponentViewModel
    {
        public int Count { get; set; }
        public List<ObtainCertificationNoticeViewModel> ObtainCertificationNotices { get; set; }
    }

    public class ObtainCertificationNoticeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string CertificationCredential { get; set; }
        public string CertificationDescription { get; set; }
        public DateTime ObtainCertificationDate { get; set; }
    }

}
