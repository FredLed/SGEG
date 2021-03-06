﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.BL.Interface;

namespace WebApp.Pages.Product
{
    public class CreateProductModel : PageModel
    {
        public CreateProductModel()
        {
        }

        public CreateProductModel(IProduct product)
        {
            _id = product.Id;
            Name = product.Name;
            Description = product.Description;
        }
        
        private Guid _id = Guid.Empty;

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}