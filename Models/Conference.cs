/// <summary>
/// The domain model for the conference.
/// </summary>
public class Conference
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    /// <summary>
    /// The start date and time of the conference in local time.
    /// </summary>
    /// <remarks>
    /// The safest way to store the conference start date is as a local time with its timezone.
    /// This ensures we're not subject to potential future timezone changes that could invalidate
    /// our original conversion from a local time to UTC. If we need to convert this time to UTC
    /// we can do this on-the-fly as we need it.
    /// </remarks>
    public required DateTime StartLocal { get; init; }

    /// <summary>
    /// The IANA time zone id of the conference.
    /// </summary>
    public required string TimeZoneId { get; init; }

    /// <summary>
    /// The timestamp the conference was last modified.
    /// </summary>
    /// <remarks>
    /// We can use UTC time here since this date will be in the past and it's reasonably safe
    /// to assume that it was converted to UTC from a local date in a timezone that won't be subject
    /// to a historical correction.
    /// </remarks>
    public required DateTime DateModifiedUtc { get; init; }
}
