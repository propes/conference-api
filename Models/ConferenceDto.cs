public class ConferenceDto
{
    public required string Name { get; init; }
    /// <summary>
    /// The start date and time of the conference in local time.
    /// </summary>
    public required DateTime StartLocal { get; init; }
    /// <summary>
    /// The IANA time zone id of the conference.
    /// </summary>
    public required string TimeZoneId { get; init; }
    /// <summary>
    /// The start date and time of the conference in UTC.
    /// </summary>
    /// <remarks>
    /// We can convert the local time to UTC using its timezone on-the-fly as we need it.
    /// </remarks>
    public required DateTime StartUtc { get; init; }
}
