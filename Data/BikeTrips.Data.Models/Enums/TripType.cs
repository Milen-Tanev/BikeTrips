using System.ComponentModel;

public enum TripType
{
    [Description("Cross-country")]
    XC = 1,
    [Description("Road bikes")]
    Road = 2,
    [Description("Downhill/enduro")]
    DH = 3
}
