﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DotRezCore.Api.Models.V1
{
    public class Order
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset EffectiveDate { get; set; } = DateTimeOffset.Now;

        [Required]
        public string Customer { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
    }
}