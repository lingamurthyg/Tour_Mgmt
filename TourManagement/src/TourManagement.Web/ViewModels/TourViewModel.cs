using System.ComponentModel.DataAnnotations;

namespace TourManagement.Web.ViewModels;

public class TourViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tour name is required")]
    [StringLength(200, ErrorMessage = "Tour name cannot exceed 200 characters")]
    public string TourName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Place is required")]
    [StringLength(200, ErrorMessage = "Place cannot exceed 200 characters")]
    public string Place { get; set; } = string.Empty;

    [Required(ErrorMessage = "Days is required")]
    [Range(1, 365, ErrorMessage = "Days must be between 1 and 365")]
    public int Days { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Locations cannot exceed 500 characters")]
    public string Locations { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "Tour info cannot exceed 2000 characters")]
    public string TourInfo { get; set; } = string.Empty;

    public string? PicturePath { get; set; }
    public IFormFile? PictureFile { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class TourCreateViewModel
{
    [Required(ErrorMessage = "Tour name is required")]
    [StringLength(200, ErrorMessage = "Tour name cannot exceed 200 characters")]
    public string TourName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Place is required")]
    [StringLength(200, ErrorMessage = "Place cannot exceed 200 characters")]
    public string Place { get; set; } = string.Empty;

    [Required(ErrorMessage = "Days is required")]
    [Range(1, 365, ErrorMessage = "Days must be between 1 and 365")]
    public int Days { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Locations cannot exceed 500 characters")]
    public string Locations { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "Tour info cannot exceed 2000 characters")]
    public string TourInfo { get; set; } = string.Empty;

    public IFormFile? PictureFile { get; set; }
}

public class TourEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tour name is required")]
    [StringLength(200, ErrorMessage = "Tour name cannot exceed 200 characters")]
    public string TourName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Place is required")]
    [StringLength(200, ErrorMessage = "Place cannot exceed 200 characters")]
    public string Place { get; set; } = string.Empty;

    [Required(ErrorMessage = "Days is required")]
    [Range(1, 365, ErrorMessage = "Days must be between 1 and 365")]
    public int Days { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [StringLength(500, ErrorMessage = "Locations cannot exceed 500 characters")]
    public string Locations { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "Tour info cannot exceed 2000 characters")]
    public string TourInfo { get; set; } = string.Empty;

    public string? ExistingPicturePath { get; set; }
    public IFormFile? PictureFile { get; set; }
}
