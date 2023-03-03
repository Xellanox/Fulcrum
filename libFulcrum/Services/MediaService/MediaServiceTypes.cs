using libFulcrum.Models;

namespace libFulcrum.Services.Media;

public class MediaServiceTypes
{
    public class FetchAllMedia
    {
        public class FetchAllMediaResponse : BaseResponse
        {
            public List<Mediafile> Media { get; set; }
        }
    }

    public class FetchMediaPaged
    {
        public class FetchAllMediaPagedRequest
        {
            public int PageStart { get; set; }
            public int PageSize {get; set;}
        }

        public class FetchMediaPagedResponse : BaseResponse
        {
            public List<Mediafile> Media { get; set; }
        }
    }
}