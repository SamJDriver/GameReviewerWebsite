using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstractions;
using Components.Exceptions;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.Graph;
using Repositories;

namespace BusinessLogic.Infrastructure
{
    public class UserRelationshipService : IUserRelationshipService
    {

        private readonly GraphServiceClient _graphServiceClient;
        private readonly GenericRepository<DockerDbContext> _genericRepository;


        public UserRelationshipService(GraphServiceClient graphServiceClient, GenericRepository<DockerDbContext> genericRepository)
        {
            _graphServiceClient = graphServiceClient;
            _genericRepository = genericRepository;
        }

        public async Task AddUserRelationship(string parentUserId, string childUserId, int UserRelationshipTypeLookupId)
        {

            if (parentUserId == childUserId)
                throw new DgcException("Cannot add yourself.", DgcExceptionType.InvalidArgument);
            
            var parentGraphResult = await _graphServiceClient.Users[parentUserId].GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = Components.Constants.MicrosoftGraph.GraphUserQueryParams;
            });

            if (parentGraphResult is null)
                throw new DgcException("Could not authenticate requesting user.", DgcExceptionType.Unauthorized);

            var childGraphResult = await _graphServiceClient.Users[childUserId].GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Select = Components.Constants.MicrosoftGraph.GraphUserQueryParams;
            });

            if (childGraphResult is null)
                throw new DgcException("Could not authenticate user to add.", DgcExceptionType.Unauthorized);

            var incomingUserRelationshipTypeLookup = _genericRepository.GetById<UserRelationshipTypeLookup>(UserRelationshipTypeLookupId);
            if (incomingUserRelationshipTypeLookup is null)
                throw new DgcException("Invalid relationship type defined.", DgcExceptionType.InvalidArgument);

            var existingRelationshipRecord = _genericRepository.GetSingleTracked<UserRelationship>(u => u.CreatedBy == parentUserId && u.ChildUserId == childUserId);
            if (existingRelationshipRecord != null)
            {
                //Same record
                if (existingRelationshipRecord.UserRelationshipTypeLookupId == incomingUserRelationshipTypeLookup.Id)
                {
                    throw new DgcException($"User is already on your {existingRelationshipRecord.UserRelationshipTypeLookup.Name} list.", DgcExceptionType.NotSupported);
                }

                // If the child user is already on requesting user's friends list and they get added to requesting user's ignore list. Remove them from friends list
                // If the child user is already on requesting user's ignore list and they get added to requesting user's friend list. Remove them from ignore list
                var existingUserRelationShipTypeLookup = existingRelationshipRecord.UserRelationshipTypeLookup;
                if ((existingUserRelationShipTypeLookup.Code == Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode && incomingUserRelationshipTypeLookup.Code == Components.Constants.LookupCodes.UserRelationshipTypeLookup.IgnoreCode) 
                || (existingUserRelationShipTypeLookup.Code == Components.Constants.LookupCodes.UserRelationshipTypeLookup.IgnoreCode && incomingUserRelationshipTypeLookup.Code == Components.Constants.LookupCodes.UserRelationshipTypeLookup.FriendCode))
                {
                    existingRelationshipRecord.UserRelationshipTypeLookupId = incomingUserRelationshipTypeLookup.Id;
                    _genericRepository.UpdateRecord(existingRelationshipRecord);
                }
            }
            else
            {
                UserRelationship newUserRelationship = new UserRelationship()
                {
                    ChildUserId = childUserId,
                    UserRelationshipTypeLookupId = incomingUserRelationshipTypeLookup.Id,
                    CreatedBy = parentUserId,
                    CreatedDate = DateTime.UtcNow
                };

                DockerDbContext.SetCreatedByUserId(parentUserId);
                _genericRepository.InsertRecord(newUserRelationship);
            }

        }
    }
}