using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication2.Models
{
        public class Cascade
        {

            public IEnumerable<SelectListItem> OgretmenList { get; set; }
            public IEnumerable<SelectListItem> DerslerList { get; set; }

        }
    }


