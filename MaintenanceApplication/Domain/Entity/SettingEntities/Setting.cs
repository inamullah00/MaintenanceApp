using Domain.Common;
using Domain.Entity.UserEntities;

namespace Maintenance.Domain.Entity.SettingEntities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public ApplicationUser? ActionBy { get; set; }
    }
}
