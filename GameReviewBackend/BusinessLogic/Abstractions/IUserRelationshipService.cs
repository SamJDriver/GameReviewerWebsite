using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Abstractions
{
    public interface IUserRelationshipService
    {
        public Task CreateUserRelationship(string parentUserId, string childUserId, int UserRelationshipTypeLookupId);
    }
}