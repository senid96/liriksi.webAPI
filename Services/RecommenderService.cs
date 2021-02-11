using AutoMapper;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using liriksi.Model.Requests.rates;

namespace liriksi.WebAPI.Services
{
    public class RecommenderService : IRecommender
    {
        //context dependency injection
        private readonly LiriksiContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRatingService _ratingService;
        private readonly int positiveRating = 3;

        public RecommenderService(LiriksiContext context, IMapper mapper, IUserService userService, IRatingService ratingService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _ratingService = ratingService;
        }

       public List<SongGetRequest> GetRecommendedSongs(int songId)
       {
            int userId = _userService.GetMyProfile().Id;
            List<SongGetRequest> recommenededSongs = new List<SongGetRequest>();

            if (userId != 0)
            {
                SongGetRequest selectedSong =  _mapper.Map<SongGetRequest>(_context.Song.Include(x=>x.Album).Include(x=>x.Album.Genre).Where(x => x.Id == songId).FirstOrDefault());
                int selectedSongId = selectedSong.Id; 
                int selectedGenreId = selectedSong.Album.GenreId;

                //get average rates of all songs
                List<AverageRate> averageRates = _context.UsersSongRates.Where(x=>x.UserId != userId)
                .Join(
                _context.Song,
                rate => rate.SongId,
                song => song.Id,
                (rate, song) => new
                {
                    song.Title,
                    rate.Rate,
                    rate.SongId,
                })
                .GroupBy(p => p.SongId)
                .Select(g => new AverageRate { Id = g.First().SongId, Title = g.First().Title, AvgRate = Math.Round(Convert.ToDouble(g.Sum(x => x.Rate)) / g.Count(), 1) }).OrderByDescending(x => x.AvgRate).ToList();

                List<AverageRate> avgRatesHigherThanPosRating = new List<AverageRate>();
                foreach (AverageRate item in averageRates)
                {
                    if (item.AvgRate > positiveRating)
                        avgRatesHigherThanPosRating.Add(item);
                }

                List<int> recommendedSongIds = new List<int>();
                recommendedSongIds = avgRatesHigherThanPosRating.Where(x=>x.Id != songId).Select(x => x.Id).Distinct().ToList();

                if (recommendedSongIds.Count() > 0)
                {
                    foreach (int id in recommendedSongIds)
                    {
                        //list of songs where genre is the same as genre of selected song
                        SongGetRequest s = _mapper.Map<SongGetRequest>(_context.Song.Include(a => a.Album).Where(x => x.Id == id && x.Album.GenreId == selectedGenreId).FirstOrDefault());
                        if(s!=null)
                            recommenededSongs.Add(s);
                    }
                }
            }
            return recommenededSongs;
       }


        // ovo je testno dolje, zanemariti
       
        //Dictionary<int, List<UsersSongRate>> songs = new Dictionary<int, List<UsersSongRate>>();

        //public RecommenderService(LiriksiContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}

        //public List<SongGetRequest> GetSimilarSongs(int songId)
        //{
        //    LoadSongs(songId);
        //    List<UsersSongRate> ratesOfObservedSong = _context.UsersSongRates.Where(x => x.SongId == songId).OrderBy(x => x.UserId).ToList();

        //    List<UsersSongRate> commonRates1 = new List<UsersSongRate>();
        //    List<UsersSongRate> commonRates2 = new List<UsersSongRate>();
        //    List<SongGetRequest> recommendedSongs = new List<SongGetRequest>();

        //    foreach (var item in songs)
        //    {
        //        foreach (UsersSongRate r in ratesOfObservedSong)
        //        {
        //            if (item.Value.Where(x => x.UserId == r.UserId).Count() > 0)
        //            {
        //                commonRates1.Add(r);
        //                commonRates2.Add(item.Value.Where(x => x.UserId == r.UserId).First());
        //            }
        //        }
        //        double similarity = GetSimilarity(commonRates1, commonRates2);

        //        if (similarity > 0.8)
        //            recommendedSongs.Add(_mapper.Map<SongGetRequest>(_context.Song.Where(x => x.Id == item.Key).FirstOrDefault()));

        //        commonRates1.Clear();
        //        commonRates2.Clear();
        //    }

        //    return recommendedSongs;
        //}

        //public double GetSimilarity(List<UsersSongRate> commonRates1, List<UsersSongRate> commonRates2)
        //{
        //    if (commonRates1.Count != commonRates2.Count)
        //        return 0;

        //    double brojnik = 0, nazivnik1 = 0, nazivnik2 = 0;

        //    for (int i = 0; i < commonRates1.Count; i++)
        //    {
        //        brojnik = commonRates1[i].Rate * commonRates2[i].Rate;
        //        nazivnik1 = commonRates1[i].Rate * commonRates1[i].Rate;
        //        nazivnik2 = commonRates2[i].Rate * commonRates2[i].Rate;
        //    }
        //    nazivnik1 = Math.Sqrt(nazivnik1);
        //    nazivnik2 = Math.Sqrt(nazivnik2);

        //    double nazivnik = nazivnik1 * nazivnik2;
        //    if (nazivnik == 0)
        //        return 0;

        //    return brojnik / nazivnik;
        //}

        //public void LoadSongs(int songId)
        //{
        //    List<Song> allSongs = _context.Song.Where(x => x.Id != songId).ToList(); //sve osim posmatrane pjesme, tj te referentne
        //    List<UsersSongRate> rates;
        //    foreach (Song item in allSongs)
        //    {
        //        rates = _context.UsersSongRates.Where(x => x.SongId == item.Id).OrderBy(x => x.UserId).ToList();
        //        if (rates.Count > 0)
        //        {
        //            songs.Add(item.Id, rates);
        //        }
        //    }
        //} 
    }
}