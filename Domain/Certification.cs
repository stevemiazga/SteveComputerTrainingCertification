using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Certification : Entity
    {
        public string CertificationCredential { get; }
        public string CertificationDescription { get; }

        private readonly List<CertificationRequirement> _certificationRequirements = new List<CertificationRequirement>();
        public virtual IReadOnlyList<CertificationRequirement> CertificationRequirements => _certificationRequirements.ToList();

        protected Certification()
        {
        }

        public Certification(int id, string certificationCredential, string certificationDescription)
            : base(id)
        {
            CertificationCredential = certificationCredential;
            CertificationDescription = certificationDescription;
        }
    }
}
