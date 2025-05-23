﻿using BusinessLogic.Abstractions;
using Components.Exceptions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Mapster;
using Microsoft.Graph;
using Repositories;

namespace BusinessLogic.Infrastructure;
public class PlayRecordService : IPlayRecordService
{
    private readonly IGenericRepository<DockerDbContext> _genericRepository;
    private readonly GraphServiceClient _graphServiceClient;


    public PlayRecordService(IGenericRepository<DockerDbContext> genericRepository, GraphServiceClient graphServiceClient)
    {
        _genericRepository = genericRepository;
        _graphServiceClient = graphServiceClient;
    }

    public async Task<IEnumerable<PlayRecord_GetSelf_Dto>> GetPlayRecords(int? gameId, string? userId)
    {
        if (userId is null)
            throw new DgcException("Can't create play record. User not found.", DgcExceptionType.Unauthorized);

        var selfPlayrecords = _genericRepository.GetMany<PlayRecords>(p => p.CreatedBy == userId && (gameId == null || p.GameId == gameId))
        .Select(p => p.Adapt<PlayRecord_GetSelf_Dto>()).ToArray();
        return selfPlayrecords;
    }

    public async Task<PlayRecord_GetById_Dto> GetPlayRecordById(int playRecordId)
    {
        var playRecord = _genericRepository.GetById<PlayRecords>(playRecordId).Adapt<PlayRecord_GetById_Dto>();
        return playRecord;
    }

    public void CreatePlayRecord(CreatePlayRecordDto playRecord, string? userId)
    {
        if (userId == null)
            throw new DgcException("Can't create play record. User not found.", DgcExceptionType.Unauthorized);

        var existingGame = _genericRepository.GetById<Games>(playRecord.GameId);
        if (existingGame == default)
            throw new DgcException("Can't create play record. Game not found.", DgcExceptionType.ResourceNotFound);

        if (playRecord.Rating < 0 || playRecord.Rating > 100)
            throw new DgcException("Can't create play record. Rating out of range.", DgcExceptionType.ArgumentOutOfRange);

        var newPlayRecordEntity = playRecord.Adapt<PlayRecords>();
        DockerDbContext.SetCreatedByUserId(userId);
        _genericRepository.InsertRecord(newPlayRecordEntity);
    }

    public void UpdatePlayRecord(int playRecordId, UpdatePlayRecordDto playRecord, string? userId)
    {

        var existingPlayRecord = _genericRepository.GetSingleNoTrack<PlayRecords>(p => p.Id == playRecordId);
        if (existingPlayRecord == default)
            throw new DgcException("Can't update Play Record. Play Record not found.", DgcExceptionType.ResourceNotFound);

        if (userId == null)
            throw new DgcException("Can't update Play Record. User not found.", DgcExceptionType.Unauthorized);

        if (existingPlayRecord.CreatedBy != userId)
            throw new DgcException("You cannot update another user's play record.", DgcExceptionType.Forbidden);

        var existingGame = _genericRepository.GetById<Games>(existingPlayRecord.GameId);
        if (existingGame == default)
            throw new DgcException("Can't update Play Record. Game not found.", DgcExceptionType.ResourceNotFound);

        if (playRecord.Rating < 0 || playRecord.Rating > 100)
            throw new DgcException("Can't update play record. Rating out of range.", DgcExceptionType.ArgumentOutOfRange);

        playRecord.Adapt(existingPlayRecord);

        DockerDbContext.SetCreatedByUserId(userId);
        _genericRepository.UpdateRecord(existingPlayRecord);
    }

    public void DeletePlayRecord(int playRecordId, string? userId)
    {
        if (userId is null)
            throw new DgcException("Cannot delete Play Record. No user logged in.", DgcExceptionType.Unauthorized);

        var existingPlayRecord = _genericRepository.GetById<PlayRecords>(playRecordId);
        if (existingPlayRecord is null)
            throw new DgcException("Cannot delete play record. No Record found.", DgcExceptionType.ResourceNotFound);

        if (existingPlayRecord.CreatedBy != userId)
            throw new DgcException("Cannot delete play record. Cannot update another user's play record.", DgcExceptionType.Forbidden);

        _genericRepository.DeleteRecordById<PlayRecords>(playRecordId);
    }
}
