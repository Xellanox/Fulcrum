using Microsoft.EntityFrameworkCore;

namespace libFulcrum.Services.Media;

internal class MediaService : IMediaService
{
    private readonly FulcrumContext _context;

    public MediaService(FulcrumContext context)
    {
        _context = context;
    }

    public async Task<MediaServiceTypes.FetchAllMedia.FetchAllMediaResponse> FetchAllMedia()
    {
        var response = new MediaServiceTypes.FetchAllMedia.FetchAllMediaResponse();

        try
        {
            var media = await _context.Mediafiles.ToListAsync();

            response.Status = true;
            response.Message = "Successfully fetched all media.";
            response.Media = media;
        }
        catch (Exception)
        {
            response.Status = false;
            response.Message = "Failed to fetch all media.";
        }

        return response;
    }

    public async Task<MediaServiceTypes.FetchMediaPaged.FetchMediaPagedResponse> FetchMediaPaged(MediaServiceTypes.FetchMediaPaged.FetchAllMediaPagedRequest request)
    {
        var response = new MediaServiceTypes.FetchMediaPaged.FetchMediaPagedResponse();

        try
        {
            var media = await _context.Mediafiles
                .Skip(request.PageStart)
                .Take(request.PageSize)
                .ToListAsync();

            response.Status = true;
            response.Message = "Successfully fetched specified media.";
            response.Media = media;
        }
        catch (Exception)
        {
            response.Status = false;
            response.Message = "Failed to fetch Specified media.";
        }

        return response;
    }
}