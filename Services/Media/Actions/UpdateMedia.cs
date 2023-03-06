using Fulcrum.Services.Media.Types;
using Microsoft.EntityFrameworkCore;

namespace Fulcrum.Services.Media;

internal partial class MediaService : IMedia
{
    public async Task<UpdateMedia.UpdateMediaResponse> UpdateMedia(UpdateMedia.UpdateMediaRequest request)
    {
        if(request.GetType().GetProperties().Where(x => x.PropertyType.Name == "String").Any(x => x.GetValue(request).ToString() == "")
            || request.GetType().GetProperties().Where(x => x.PropertyType.Name.Contains("Int")).Any(x => x.GetValue(request).ToString() == "0"))
        {
            return new UpdateMedia.UpdateMediaResponse
            {
                Message = "All fields must be provided",
                Status = false
            };
        }


        var target = await _context.Mediafiles
            .FirstOrDefaultAsync(x => x.MediafileId == request.Id);

        if (target == null)
        {
            return new UpdateMedia.UpdateMediaResponse
            {
                Message = "Cannot update a file that doesn't exist",
                Status = false
            };
        }

        target.Artist = request.TrackArtist;
        target.AlbumArtist = request.AlbumArtist;
        target.Album = request.Album;
        target.Title = request.Title;
        target.Year = request.Year;
        target.TrackNumber = request.TrackNumber;
        target.DiscNumber = request.DiscNumber;

        target.LastModified = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new UpdateMedia.UpdateMediaResponse
            {
                Message = "Error occured while updating mediafile",
                Status = false
            };
        }

        return new UpdateMedia.UpdateMediaResponse
        {
            Message = "Successfully updated mediafile",
            Status = true
        };
    }
}