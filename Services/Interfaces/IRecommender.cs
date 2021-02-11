using liriksi.Model;
using liriksi.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liriksi.WebAPI.Services.Interfaces
{
    public interface IRecommender
    {
        List<SongGetRequest> GetRecommendedSongs(int songId);
       //void LoadSongs(int songId);
       //double GetSimilarity(List<UsersSongRate> commonRates1, List<UsersSongRate> commonRates2);
       // List<SongGetRequest> GetSimilarSongs(int songId);
    }
}
