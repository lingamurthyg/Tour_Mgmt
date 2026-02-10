using System.ComponentModel.DataAnnotations;

namespace TourManagement.Web.ViewModels;

public class BookingViewModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TourId { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public string TourName { get; set; } = string.Empty;
    public DateTime BookingDate { get; set; }
    public int NumberOfPeople { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}

public class BookingCreateViewModel
{
    [Required(ErrorMessage = "Tour selection is required")]
    public int TourId { get; set; }

    [Required(ErrorMessage = "Booking date is required")]
    [DataType(DataType.Date)]
    public DateTime BookingDate { get; set; } = DateTime.Today.AddDays(1);

    [Required(ErrorMessage = "Number of people is required")]
    [Range(1, 50, ErrorMessage = "Number of people must be between 1 and 50")]
    public int NumberOfPeople { get; set; } = 1;

    public decimal TourPrice { get; set; }
    public decimal TotalPrice => TourPrice * NumberOfPeople;

    public string TourName { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public int Days { get; set; }
}
