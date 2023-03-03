namespace libFulcrum.Services.Media;

public interface IMediaService
{
    Task<MediaServiceTypes.FetchAllMedia.FetchAllMediaResponse> FetchAllMedia();
    Task<MediaServiceTypes.FetchMediaPaged.FetchMediaPagedResponse> FetchMediaPaged(MediaServiceTypes.FetchMediaPaged.FetchAllMediaPagedRequest request);
    Task<MediaServiceTypes.FetchMediaStream.FetchMediaStreamResponse> FetchMediaStream(MediaServiceTypes.FetchMediaStream.FetchMediaStreamRequest request);
}