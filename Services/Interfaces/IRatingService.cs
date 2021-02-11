using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.Model.Requests.rates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface IRatingService
    {
        bool RateSong(UsersSongRate obj);
        bool RateAlbum(UsersAlbumRate obj);

        List<UserAlbumRateGetRequest> GetRatesByAlbum(int albumId);
        List<UserSongRateGetRequest> GetRatesBySong(int songId);
        List<RateDetails> GetSongRates();
        List<RateDetails> GetAlbumRates();
        List<UserSongRateGetRequest> GetSongRatesByUser(int userId);
        List<UserAlbumRateGetRequest> GetAlbumRatesByUser(int userId);
        UsersSongRate GetRateBySongByUser(HasUserRatedRequest obj);
        UsersAlbumRate GetRateByAlbumByUser(HasUserRatedRequest obj);
    }
}
