namespace Ivao.It.IvaoApiSdk.Dto.Tracker;

#if NET8_0_OR_GREATER
public class TrackerConnectionType(string type)
{
#else
public class TrackerConnectionType
{
    private readonly string type;
    public TrackerConnectionType(string type)
    {
        this.type = type;
    }

#endif
    public static readonly TrackerConnectionType Pilot = new("PILOT");
    public static readonly TrackerConnectionType Atc = new("ATC");
    public static readonly TrackerConnectionType Observer = new("OBS");
    public static readonly TrackerConnectionType FollowMe = new("FOLME");

    public override string ToString() => type;
}