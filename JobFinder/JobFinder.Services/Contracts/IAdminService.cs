using System;
using System.Collections.Generic;
using System.Text;

namespace JobFinder.Services.Contracts
{
    public interface IAdminService
    {
        void Update();
        void Delete(string jobAddId);

    }
}
