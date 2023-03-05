using Fulcrum.Filters;
using Fulcrum.Services.Media;
using Fulcrum.Services.Media.Types;
using Microsoft.AspNetCore.Mvc;

namespace Fulcrum.Controllers.API;

public class MediaController : Controller
{
    private readonly IMedia _media;

    public MediaController(IMedia media)
    {
        _media = media;
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

        var fileStream = new FileStream(Path.Join(Environment.GetEnvironmentVariable("LIBRARY_ROOT"), res.Info.FilePath), FileMode.Open, FileAccess.Read);

        return File(fileStream, res.Info.MimeType, true);
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("/api/media")]
    public async Task<ActionResult<FetchMedia.FetchMediaResponse>> FetchMedia([FromBody] FetchMedia.FetchMediaRequest req)
        => await _media.FetchMedia(req);
}