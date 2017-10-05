using System.ComponentModel.DataAnnotations;

public enum TripType
{
    [Display(Name = "Cross-country")]
    XC = 1,
    [Display(Name = "Road")]
    Road = 2,
    [Display(Name = "Downhill / enduro")]
    DH = 3
}
