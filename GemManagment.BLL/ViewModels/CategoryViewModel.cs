using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemManagment.BLL.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category Name")]
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(20, ErrorMessage = "Category name cannot exceed 20 characters")]
        public string Name { get; set; }

        // Additional properties for display
        public int SessionCount { get; set; }
    }
}
