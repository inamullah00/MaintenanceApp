﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s.ClientServiceCategoryDto
{
    public class OfferedServiceCategoryRequestDto
    {
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
    }
}
