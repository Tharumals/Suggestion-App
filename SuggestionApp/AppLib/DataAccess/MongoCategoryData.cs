using Microsoft.Extensions.Caching.Memory;

namespace AppLib.DataAccess
{
    public class MongoCategoryData : ICategoryData
    {
        private readonly IMongoCollection<CategoryModel> _categories;
        private readonly IMemoryCache _cache;
        private const string casheName = "CategoryData";

        public MongoCategoryData(IDbConnection db, IMemoryCache cache)
        {
            _cache = cache;
            _categories = db.CategoryCollection;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var output = _cache.Get<List<CategoryModel>>(casheName);
            if (output is null)
            {
                var result = await _categories.FindAsync(_ => true);
                output = result.ToList();

                _cache.Set(casheName, output, TimeSpan.FromDays(1));
            }
            return output;
        }

        public Task CreateCategory(CategoryModel category)
        {
            return _categories.InsertOneAsync(category);
        }
    }
}
