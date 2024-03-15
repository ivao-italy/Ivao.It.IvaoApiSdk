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
    public static TrackerConnectionType Pilot = new("PILOT");
    public static TrackerConnectionType Atc = new("ATC");
    public static TrackerConnectionType Observer = new("OBS");
    public static TrackerConnectionType FollowMe = new("FOLME");

    public override string ToString() => type;
}