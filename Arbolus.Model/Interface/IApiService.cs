using System;
using System.Threading.Tasks;

namespace Arbolus.Model.Interface
{
    public interface IApiService
    {
        string SvcUrl { get; set; }

        string RequestUri { get; set; }

        Task<String> Get();
    }
}
