using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookManagementApi.Models;

public partial class Book
{
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }

    public string? Isbn { get; set; }

    [DataType(DataType.Date)]
    public DateTime PublishedDate { get; set; }

    public string? Genre { get; set; }
}