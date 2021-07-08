using System.Threading.Tasks;
using AnimalFarmsMarket.Data.Services.Interfaces;
using AnimalFarmsMarket.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AnimalFarmsMarket.Data.DTOs;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Identity;
using AnimalFarmsMarket.Data.ViewModels;
using Microsoft.AspNetCore.Http;

namespace AnimalFarmsMarket.Data.Services.Implementations
{
    public class LiveStockService : ILiveStockService
    {
        private readonly AppDbContext _dbcontext;
        private readonly IRatingService _ratingService;
        private readonly IReviewService _reviewService;
        private readonly ILivestoclImage _livestockImage;

        public int TotalCount { get; set; }
        public LiveStockService(AppDbContext dbContext, IRatingService ratingService, 
            IReviewService reviewService, ILivestoclImage livestockImage)
        {
            _dbcontext = dbContext;
            _ratingService = ratingService;
            _reviewService = reviewService;
            _livestockImage = livestockImage;
        }

        private async Task<bool> SavedAsync()
        {
            var valueToReturned = false;
            if (await _dbcontext.SaveChangesAsync() > 0)
                valueToReturned = true;
            else
                valueToReturned = false;

            return valueToReturned;
        }

        private async Task<IEnumerable<ShappedListOfLivestock>> ShapeResult(IQueryable<Livestock> livestocks, int page, int perPage)
        {
            var result = await livestocks.Select(x => new ShappedListOfLivestock
             {
                 Id = x.Id,
                 Price = x.SellingPrice,
                 Photo = x.Images.FirstOrDefault(x => x.IsMain).ImageUrl
             }).ToListAsync(); 
            TotalCount = result.Count();

            return result.Skip((page - 1) * perPage).Take(perPage);
        }

  
        public IQueryable<Livestock> GetLiveStocks()
        {
            var livestock = _dbcontext.LiveStocks;
            TotalCount = livestock.Count();
            return livestock;
        }

        public  IEnumerable<LocationMarketDto> GetLiveStockLocation()
        {
            

            var result = _dbcontext.MarketAddresses.Include(x => x.Market).ToList();
            var response = new List<LocationMarketDto>();
            foreach (var marketItem in result)
            {
                var toReturn = new LocationMarketDto
                {
                    location = marketItem.State,
                    marketName = marketItem.Market.MarketName
                };
                response.Add(toReturn);
            }
            return response;
        }


        public async Task<IEnumerable<ShappedListOfLivestock>> GetLiveStocksAndMainImageAsync(int page, int perPage)
        {
            var result = GetLiveStocks().Include(x => x.Images);
            return await ShapeResult(result, page, perPage);
        }
        

        public async Task<Livestock> GetLivestockByIdAsync(string Id)
        {
            return await _dbcontext.LiveStocks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Livestock> GetLivestockFullDetailsByIdAsync(string Id)
        {

            var livestock = _dbcontext.LiveStocks.Include(a => a.Agent).ThenInclude(u => u.AppUser)
                                                 .Include(m => m.Market).ThenInclude(ma => ma.MarketAddress)
                                                 .FirstOrDefault(x => x.Id == Id);

            livestock.Reviews = await _reviewService.GetReviewsByLivestock(Id);
            livestock.Images = await _livestockImage.GetLivestockImagesByLivestock(Id);

            return livestock;
        }

        public async Task<bool> AddLivestockImageAsync(LivestockImages livestock)
        {
            await _dbcontext.LivestockImages.AddAsync(livestock);
            return await SavedAsync();
        }

        public async Task<LivestockImages> GetLivestockMainImage(string livestockId)
        {
            var query = await _dbcontext.LivestockImages.FirstOrDefaultAsync(x => x.LivestockId == livestockId && x.IsMain == true);
            return query;
        }
        
        public async Task<IEnumerable<LivestockImages>> GetLivestockImages(string livestockId)
        {
            return await _dbcontext.LivestockImages.Where(x => x.LivestockId == livestockId).ToListAsync();
        }


        public async Task<LivestockImages> GetLivestockImageAsync(string livestockId, string publicId)
        {
            var result = await _dbcontext.LivestockImages.FirstOrDefaultAsync(x => x.LivestockId == livestockId && x.PublicId == publicId);
            return result;
        }


        public async Task<bool> DeleteLivestockImageAsync(string livestockId, string publicId)
        {
            var image = _dbcontext.LivestockImages.FirstOrDefault(x => x.PublicId == publicId && x.LivestockId == livestockId);
            if(image != null)
            {
                _dbcontext.LivestockImages.Remove(image);
                return await SavedAsync();
            }
            return false;
        }

        public async Task<bool> DeleteAllLivestockImagesAsync(string livestockId)
        {
            var images = await GetLivestockImages(livestockId);
            if (images.Count() > 0)
            {
                _dbcontext.RemoveRange(images);
                return await SavedAsync();
            }
            return false;
        }


        public async Task<IEnumerable<ShappedListOfLivestock>> GetLivestocksByLocationAsync(int page, int perPage, string location)
        {
            var result = GetLiveStocks().Include(x => x.Images).Include(x => x.Market)
                .Where(x => x.Market.MarketAddress.State == location);

            return await ShapeResult(result, page, perPage);
        }


        public async Task<IEnumerable<ShappedListOfLivestock>> GetLivestocksByLocationAndMarketAsync(int page, int perPage, 
            string location, string market)
        {
            var result = GetLiveStocks().Include(x => x.Images).Include(x => x.Market)
                .Where(x => x.Market.MarketAddress.State.ToLower() == location.ToLower()
                                && x.Market.MarketName.ToLower() == market.ToLower());

            return await ShapeResult(result, page, perPage);
        }


        public async Task<IEnumerable<ShappedListOfLivestock>> GetLivestocksByQueriesAsync(SearchLivestockDto query, int perPage)
        {
            var result = GetLiveStocks().Include(x => x.Images).Include(x => x.Category).Where(x => x.Weight == query.Weight
            && x.Sex == query.Sex
            && x.Breed == query.Breed
            || x.Category.Name == query.Type);
            
            return await ShapeResult(result, query.PageNumber, perPage);
        }
      


        public async Task<bool> DeleteLivestockAsync(Livestock livestock)
        {
            _dbcontext.LiveStocks.Remove(livestock);
             return await SavedAsync();
        }


        public async Task<bool> AddLivestockAsync(Livestock livestock)
        {
            await _dbcontext.LiveStocks.AddAsync(livestock);
             return await SavedAsync();

        }


        public async Task<bool> UpdateLivestock(Livestock livestock)
        {
            _dbcontext.LiveStocks.Update(livestock);
            return await SavedAsync();
        }

        public IEnumerable<IEnumerable<string>> GetBreedsSexesAndWeightsForLivestocksCategory(string category)
        {

            var categoryBreeds = new List<string>();
            var categorySexes = new List<string>();
            var categoryWeights = new List<string>();

            if (category != null)
            {
                var livestocksByCategory = GetLiveStocks().Where(y => y.Category.Name.ToLower() == category.ToLower());
                categoryBreeds = GetDistintLivestocksBreeds(livestocksByCategory);
                categorySexes = GetDistintLivestocksSexes(livestocksByCategory);
                categoryWeights = GetDistintLivestocksWeights(livestocksByCategory);
            }


            return new List<List<string>>()
            {
                categoryBreeds, categorySexes, categoryWeights
            };
        }

        public List<String> GetDistintLivestocksBreeds(IQueryable<Livestock> livestocks)
        {
            var categoryBreeds = livestocks.Select(b => b.Breed).Distinct().OrderBy(br => br).ToList();

            return categoryBreeds;
        }

        public List<String> GetDistintLivestocksSexes(IQueryable<Livestock> livestocks)
        {
            var categorySexes = livestocks.Select(s => s.Sex.ToString()).Distinct().ToList().OrderBy(sx => sx).ToList();

            return categorySexes;
        }

        public List<String> GetDistintLivestocksWeights(IQueryable<Livestock> livestocks)
        {
            var categoryWeights = livestocks.Select(w => w.Weight.ToString()).Distinct().ToList().OrderBy(wt => wt).ToList();

            return categoryWeights;
        }

        public async Task<IEnumerable<ShappedListOfLivestock>> GetLivestockByUserIdAsync(string userId, int page, int perPage)
        {
            var result = _dbcontext.LiveStocks.Include(x => x.Images).Where(x => x.Id == userId);
            return await ShapeResult(result, page, perPage);
        }
    }
}