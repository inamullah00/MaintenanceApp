using Application.Dto_s.ClientDto_s.ClientServiceCategoryDto;
using Application.Dto_s.UserDto_s;
using Domain.Entity.UserEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s.ClientDto_s
{
    public class OfferedServiceResponseDto
    {

        public Guid Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? PreferredTime { get; set; }
        public string Building { get; set; }
        public string Apartment { get; set; }
        public string Floor { get; set; }
        public string Street { get; set; }
        public bool SetAsCurrentHomeAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public OfferedServiceCategoryResponseDto Category { get; set; }
        public ApplicationUsersResponseDto Client { get; set; }
    }
}
