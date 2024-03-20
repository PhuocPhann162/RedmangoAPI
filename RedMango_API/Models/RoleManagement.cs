using Microsoft.AspNetCore.Mvc.Rendering;

namespace RedMango_API.Models
{
    public class RoleManagement
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
