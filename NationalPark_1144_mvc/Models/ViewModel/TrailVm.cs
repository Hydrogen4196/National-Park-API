using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalPark_1144_mvc.Models.ViewModel
{
    public class TrailVm
    {
        public Trail Trail { get; set; }
        public IEnumerable<SelectListItem> NationalParkList { get; set; }
    }
}
