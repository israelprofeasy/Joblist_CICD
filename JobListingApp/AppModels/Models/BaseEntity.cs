﻿using System;

namespace JobListingApp.AppModels.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
        public string UpdatedAt { get; set; } = DateTime.Now.ToString();
    }
}
