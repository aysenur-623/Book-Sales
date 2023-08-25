﻿using Infrastructure.Model;

namespace BS.Model.Dtos.Author
{
    public class AuthorPutDto : IDto
    {   public int AuthorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
