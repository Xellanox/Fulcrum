using Fulcrum.Services.Media.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.Media;

internal partial class MediaService : IMedia
{
    public async Task<FetchMedia.FetchMediaResponse> FetchMedia(FetchMedia.FetchMediaRequest request)
    {
        var findMedia = await _context.Mediafiles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.MediafileId == request.Id);

        if (findMedia == null)
        {
            return new FetchMedia.FetchMediaResponse
            {
                Message = "Specified mediafile not found",
                Status = false
            };
        }

        return new FetchMedia.FetchMediaResponse
        {
            Message = "Successfully found media info",
            Status = true,
            Info = findMedia
        };
    }
}