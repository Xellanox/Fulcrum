using Fulcrum.Models;

namespace Fulcrum.Services.Media.Types;

public class FetchMedia
{
    public class FetchMediaRequest
    {
        public int Id {get; set;}
    }

    public class FetchMediaResponse : BaseResponse {
        public Mediafile Info {get; set;}
    }
}