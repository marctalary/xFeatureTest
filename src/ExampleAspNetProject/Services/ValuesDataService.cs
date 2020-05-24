using System.Collections.Generic;
using System.Linq;
using ExampleAspNetProject.Models;
using Microsoft.Extensions.Caching.Memory;

namespace ExampleAspNetProject.Services
{
    public interface IValuesDataService
    {
        IEnumerable<ValueModel> Read();
        ValueModel Read(int id);
        void Update(int id, string value);
        void Delete(int id);
        ValueModel Create(string value);
    }

    public class ValuesDataService : IValuesDataService
    {
        private const string CacheKey = "data";
        private readonly IMemoryCache _cache;

        public ValuesDataService(IMemoryCache cache)
        {
            _cache = cache;
            var dataSource = DataSource;
            dataSource.Add(1, "value1");
            dataSource.Add(2, "value2");
            dataSource.Add(3, "value3");

            _cache.CreateEntry(CacheKey);
        }

        private Dictionary<int, string> DataSource => _cache.GetOrCreate(CacheKey, c => new Dictionary<int, string>());

        public IEnumerable<ValueModel> Read()
        {
            return DataSource.Select(o => new ValueModel { Id = o.Key, Value = o.Value }).ToArray();
        }

        public ValueModel Read(int id)
        {
            return new ValueModel { Id = id, Value = DataSource[id] };
        }

        public void Update(int id, string value)
        {
            DataSource[id] = value;
        }

        public void Delete(int id)
        {
            DataSource.Remove(id);
        }

        public ValueModel Create(string value)
        {
            var newId = DataSource.Keys.Max() + 1;
            DataSource.Add(newId, value);
            return new ValueModel {Id = newId, Value = value};
        }
    }
}