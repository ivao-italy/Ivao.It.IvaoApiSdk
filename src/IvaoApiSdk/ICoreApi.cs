using Ivao.It.IvaoApiSdk.Dto;

namespace Ivao.It.IvaoApiSdk;
public interface ICoreApi
{
    /// <summary>
    /// Gets all the FRA of facilities with ICAO starting with given strings
    /// </summary>
    /// <param name="icaoStart"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    Task<List<FraDto>> GetAllFras(string icaoStart, CancellationToken ct);
}
