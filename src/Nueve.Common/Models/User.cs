using Nueve.Common.Enums;
using System.Runtime.Serialization;

namespace Nueve.Common.Models
{
    public class User
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public UserRole Role { get; set; }
    }
}
