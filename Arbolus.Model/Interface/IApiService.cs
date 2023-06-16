using System;
using System.Threading.Tasks;

namespace Arbolus.Model.Interface
{
    public interface IApiService
    {
        string SvcUrl { get; set; }

        int RequestUri { get; set; }

        Task<String> Get();
    }
}
