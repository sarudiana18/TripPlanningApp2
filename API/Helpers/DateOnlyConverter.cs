using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() 
        : base(dateOnly => 
                dateOnly.ToDateTime(TimeOnly.MinValue), 
            dateTime => DateOnly.FromDateTime(dateTime)) { }

}
 public class NullableDateOnlyConverter : ValueConverter<DateOnly?, DateTime?>
      {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public NullableDateOnlyConverter() : base(
            d => d == null 
                ? null 
                : new DateTime?(d.Value.ToDateTime(TimeOnly.MinValue)),
            d => d == null 
                ? null 
                : new DateOnly?(DateOnly.FromDateTime(d.Value)))
        { }
    }