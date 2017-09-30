using System.ComponentModel.DataAnnotations;

public enum TripType
{
    [Display(Name = "Cross-country")]
    XC = 1,
    Road = 2,
    [Display(Name = "Downhill / enduro")]
    DH = 3
}
