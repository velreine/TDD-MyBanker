using System;
using System.Collections.Generic;
using System.Text;
using MyBanker_Library.Interfaces;

namespace MyBanker_Library.Concrete
{

    public class DummySQLDataLoader : IDummySQLDataLoader
    {

        private IDummySQLDataLoader _loader = null;

        public DummySQLDataLoader(IDummySQLDataLoader loader)
        {
            this._loader = loader;
        }

        public ICollection<T> LoadData<T>(string sql)
        {
            return _loader.LoadData<T>(sql);
        }

    }
}
