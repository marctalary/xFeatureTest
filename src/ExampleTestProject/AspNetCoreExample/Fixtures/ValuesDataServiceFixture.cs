using System.Collections.Generic;
using System.Linq;
using ExampleAspNetProject.Models;
using ExampleAspNetProject.Services;

namespace ExampleTestProject.AspNetCoreExample.Fixtures
{
    public class ValuesDataServiceFixture : IValuesDataService
    {
        private readonly List<ValueModel> _valueModels = new List<ValueModel>();

        public IEnumerable<ValueModel> Read()
        {
            return _valueModels;
        }

        public ValueModel Read(int id)
        {
            return _valueModels.SingleOrDefault(v => v.Id == id);
        }

        public void Update(int id, string value)
        {
            _valueModels.Single(v => v.Id == id).Value = value;
        }

        public void Delete(int id)
        {
            _valueModels.Remove(_valueModels.Single(v => v.Id == id));
        }

        public ValueModel Create(string value)
        {
            var model = new ValueModel {Id = _valueModels.Max(o => o.Id as int?) ?? 0 + 1, Value = value};
            _valueModels.Add(model);
            return model;
        }

        public void Delete()
        {
            _valueModels.Clear();
        }
    }
}