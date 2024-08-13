using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Components.Models;

namespace BusinessLogic.Abstractions
{
    public interface ILookupService
    {
        public IEnumerable<GenreLookupDto> GetGenreLookups();
    }
}