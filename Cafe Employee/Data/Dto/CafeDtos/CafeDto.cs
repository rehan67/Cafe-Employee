﻿namespace Cafe_Employee.Data.Dto.CafeDtos
{
    public class CafeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int Employees { get; set; }
        public string Logo { get; set; }
    }
}
