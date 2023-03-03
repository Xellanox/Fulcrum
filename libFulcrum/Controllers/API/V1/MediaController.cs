using Microsoft.AspNetCore.Mvc;
using libGatekeeper.Filters;

namespace libFulcrum.Services.Media;

public class MediaController : Controller
{
    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService)
    {
        _mediaService = mediaService;
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
}