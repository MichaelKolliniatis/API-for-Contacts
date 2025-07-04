﻿using System.ComponentModel.DataAnnotations;

public class CreateProfessionDto
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Name of the profession is missing.")]
    [MaxLength(100, ErrorMessage = "Name cannot have more than 100 characters.")]
    [RegularExpression(@"^[^\d]+$", ErrorMessage = "Name cannot contain numbers.")]
    public string? Name { get; set; } = string.Empty;
}