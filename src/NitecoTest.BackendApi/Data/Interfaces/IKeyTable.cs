using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NitecoTest.BackendApi.Data.Interfaces
{
    interface IKeyTable<T>
    {
        T Id { get; set; }
    }
}
