namespace DrillBitSizer;

public class DrillBitSize
{
    public decimal DecimalInches { get; set; }
    public string? Millimeters { get; set; }
    public string? NumberLetterSize { get; set; }
    public string? FractionalInches { get; set; }
    public string? ReducedFractionalInches { get; set; }

    private bool HasReducedFractionalInches()
    {
        return ReducedFractionalInches != null && ReducedFractionalInches != "---";
    }

    private bool HasFractionalInches()
    {
        return FractionalInches != null && FractionalInches != "---";
    }

    private bool HasNumberLetterSize()
    {
        return NumberLetterSize != null && NumberLetterSize != "---";
    }

    private bool HasMillimeters()
    {
        return Millimeters != null && Millimeters != "---";
    }

    public bool IsImperial()
    {
        return HasReducedFractionalInches() || HasFractionalInches() || HasNumberLetterSize();
    }

    public bool IsMetric()
    {
        return HasMillimeters() && !HasReducedFractionalInches() && !HasFractionalInches() && !HasNumberLetterSize();
    }

    public string GetSize()
    {
        if (HasReducedFractionalInches())
        {
            return $"{ReducedFractionalInches}\"";
        }
        
        if (HasFractionalInches())
        {
            return $"{FractionalInches}\"";
        }
        
        if (HasNumberLetterSize())
        {
            if (int.TryParse(NumberLetterSize, out _))
            {
                return $"#{NumberLetterSize}";
            }

            return NumberLetterSize;
        }

        if (HasMillimeters())
        {
            if (decimal.TryParse(Millimeters, out decimal parsed))
            {
                return $"{parsed:F2} mm";
            }
        }

        throw new ApplicationException("Invalid entry");
    }
}