using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechFix.Common.Interfaces;
using TechFix.EntityModels;

namespace TechFix.TransportModels.Dtos
{
    public class MemberDto : IMapFrom<User>
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string StaffCode { get; set; }
        public string Email { get; set; }
        public decimal BonusPercent { get; set; }
        public string Role { get; set; }
        public Guid StoreId { get; set; }
        public UserStatus Status { get; set; }
    }
}
