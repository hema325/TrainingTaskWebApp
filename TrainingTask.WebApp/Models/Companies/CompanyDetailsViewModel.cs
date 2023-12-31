﻿using TrainingTask.WebApp.Common.Mapping;

namespace TrainingTask.WebApp.Models.Companies
{
    public class CompanyDetailsViewModel:IMapFrom<Entities.Company>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
    }
}
