using Fulcrum.Filters;
using Fulcrum.Services.CurrentConfig;
using Fulcrum.Services.Media;
using Fulcrum.Services.Media.Types;
using Microsoft.AspNetCore.Mvc;

namespace Fulcrum.Controllers.API;

public class MediaController : Controller
{
    private readonly IMedia _media;
    private readonly ICurrentConfig _config;

    public MediaController(IMedia media, ICurrentConfig config)
    {
        _media = media;
        _config = config;
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/media/stream")]
    public async Task<ActionResult> StreamMedia([FromBody] FetchMedia.FetchMediaRequest req)
    {
        var res = await _media.FetchMedia(req);

        if (res.Status == false)
        {
            return BadRequest(res);
        }

        var CurrentConfig = await _config.FetchConfig();

        var fileStream = new FileStream(Path.Join(CurrentConfig.CurrentConfig.LibraryBasePath, res.Info.FilePath), FileMode.Open, FileAccess.Read);

        return File(fileStream, res.Info.MimeType, true);
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/media/art")]
    public async Task<ActionResult> FetchArt([FromBody] FetchMedia.FetchMediaRequest req)
    {
        var res = await _media.FetchMedia(req);

        if (res.Status == false)
        {
            return BadRequest(res);
        }

        var CurrentConfig = await _config.FetchConfig();

        var fileStream = new FileStream(Path.Join(CurrentConfig.CurrentConfig.ArtBasePath, res.Info.ArtFilename), FileMode.Open, FileAccess.Read);

        return File(fileStream, "image/jpeg", false);
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/media")]
    public async Task<ActionResult<FetchMedia.FetchMediaResponse>> FetchMedia([FromBody] FetchMedia.FetchMediaRequest req)
        => await _media.FetchMedia(req);
}