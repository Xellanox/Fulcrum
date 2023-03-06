using Fulcrum.Services.Media.Types;

namespace Fulcrum.Services.Media;

public interface IMedia
{
    Task<FetchMedia.FetchMediaResponse> FetchMedia(FetchMedia.FetchMediaRequest request);
    Task<UpdateMedia.UpdateMediaResponse> UpdateMedia(UpdateMedia.UpdateMediaRequest request);
}