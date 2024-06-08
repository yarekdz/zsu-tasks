using Microsoft.Extensions.Caching.Distributed;

namespace Tasks.Api.Cache
{
    public static class CacheOptions
    {
        public static DistributedCacheEntryOptions DefaultExpiration =>
            new() { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20) };
    }

    //var task = await _cache.GetAsync($"tasks-{id}",
    //    async token =>
    //    {
    //        var task = await _dbContext.Tasks
    //            .AsNoTracking()
    //            .SingleOrDefaultAsync(t => t.Id == new TaskId(id), ct);

    //        return task;
    //    },
    //    CacheOptions.DefaultExpiration,
    //    ct);
}
