namespace Fulcrum.Services.Media.Types;

public class UpdateMedia
{
    public class UpdateMediaRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string TrackArtist { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public int Year { get; set; }
        public int DiscNumber { get; set; }
        public int TrackNumber { get; set; }
    }

    public class UpdateMediaResponse : BaseResponse { }
}