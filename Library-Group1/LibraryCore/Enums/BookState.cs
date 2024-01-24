using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore.Enums
{
    public enum BookState
    {
        [Display(Name = "Disponible")]
        AVAILABLE,
        [Display(Name = "Emprunté")]
        LOANED
    }
}
