namespace Ivao.It.IvaoApiSdk.Dto.Tracker;
public class TrackerConnectionType(string type)
{
    public static TrackerConnectionType Pilot = new("PILOT");
    public static TrackerConnectionType Atc = new("ATC");
    public static TrackerConnectionType Observer = new("OBS");
    public static TrackerConnectionType FollowMe = new("FOLME");

    public override string ToString() => type;
}