using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Filters;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace libFulcrum.Services.Media;

public class MediaController : Controller
{
    private readonly IMediaService _mediaService;
    private readonly IConfiguration _configuration;

    public MediaController(IMediaService mediaService, IConfiguration configuration)
    {
        _mediaService = mediaService;
        _configuration = configuration;
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("api/v1/media")]
    public async Task<ActionResult<MediaServiceTypes.FetchAllMedia.FetchAllMediaResponse>> FetchAllMedia()
    {
        var response = await _mediaService.FetchAllMedia();

        if (response.Status)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("api/v1/media/paged")]
    public async Task<ActionResult<MediaServiceTypes.FetchMediaPaged.FetchMediaPagedResponse>> FetchMediaPaged([FromBody] MediaServiceTypes.FetchMediaPaged.FetchAllMediaPagedRequest request)
    {
        var response = await _mediaService.FetchMediaPaged(request);

        if (response.Status)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [HttpGet]
    [TypeFilter(typeof(Authenticate))]
    [Route("api/v1/media/stream")]
    public async Task<ActionResult> FetchMediaStream([FromQuery] int id)
    {
        var request = new MediaServiceTypes.FetchMediaStream.FetchMediaStreamRequest
        {
            MediafileId = id
        };

        var response = await _mediaService.FetchMediaStream(request);

        if (!response.Status)
        {
            return BadRequest(response);
        }

        var fileStream = new FileStream(Path.Join(_configuration.GetSection("FulcrumConfig").GetValue<string>("MediaRoot"), response.MediafilePath), FileMode.Open, FileAccess.Read);

        return File(fileStream, "audio/ogg", enableRangeProcessing: true);
    }
}