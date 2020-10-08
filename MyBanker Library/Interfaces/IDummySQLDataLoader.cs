using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker_Library.Interfaces
{
    public interface IDummySQLDataLoader
    {  
        ICollection<T> LoadData<T>(string sql);
    }
}
