﻿using BusinessLogic.Abstractions;
using Components.Utilities;
using Components.Extensions;
using Components.Models;
using DataAccess.Contexts.DockerDb;
using DataAccess.Models.DockerDb;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Components.Exceptions;

namespace BusinessLogic.Infrastructure
{
    public class GameService : IGameService
    {
        private readonly GenericRepository<DockerDbContext> _genericRepository;
        public GameService(GenericRepository<DockerDbContext> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int CreateGame(GameDto game)
        {
            if (!game.ReleaseDate.ValidateDateOnly())
            {
                throw new DgcException($"Invalid game release date. Ensure release date is between 1/1/1955 and {DateTime.Now}", DgcExceptionType.ArgumentOutOfRange);
            }

            var gameEntity = new Games().Assign(game);
            gameEntity.CreatedBy = "System";
            gameEntity.CreatedDate = DateTime.Now;

            //TODO add validation checks for game?
            _genericRepository.InsertRecord(gameEntity);
            return gameEntity.Id;
        }
        public async Task<PagedResult<GameDto>?> GetAllGames(int pageIndex, int pageSize)
        {
            var query = _genericRepository.GetAll<Games>();

            var data = 
                    await query
                    .OrderBy(g => g.Title)
                    .Skip(pageIndex*pageIndex)
                    .Take(pageSize)
                    .Select(g => new GameDto().Assign(g))
                    .ToListAsync();

            return new PagedResult<GameDto>()
            {
                Data = data,
                TotalRowCount = query.Count(),
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }
        public GameDto GetGameById(int gameId)
        {
            var game = _genericRepository.GetById<Games>(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException();
            }

            var gameDto = new GameDto().Assign(game);
            return gameDto;
        }
        public void CreatePlayRecord(PlayRecordDto playRecord)
        {
            var existingUser = _genericRepository.GetById<Users>(playRecord.UserId);
            if (existingUser == default)
            {
                return;
            }

            var existingGame = _genericRepository.GetById<Games>(playRecord.GameId);
            if (existingGame == default)
            {
                return;
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                return;
            }

            var newPlayRecordEntity = new PlayRecords().Assign(playRecord);
            newPlayRecordEntity.CreatedBy = existingUser.Username;
            newPlayRecordEntity.CreatedDate = DateTime.Now;
            _genericRepository.InsertRecord(newPlayRecordEntity);  
        }

        public void UpdatePlayRecord(PlayRecordDto playRecord)
        {
            var existingUser = _genericRepository.GetById<Users>(playRecord.UserId);
            if (existingUser == default)
            {
                return;
            }

            var existingGame = _genericRepository.GetById<Games>(playRecord.GameId);
            if (existingGame == default)
            {
                return;
            }

            var existingPlayRecord = _genericRepository.GetSingleNoTrack<PlayRecords>(p => p.UserId == playRecord.UserId && p.GameId == playRecord.GameId);
            if (existingPlayRecord == default) //Existing Record
            {
                return;
            }

            if (playRecord.Rating < 0 || playRecord.Rating > 100)
            {
                return;
            }

            existingPlayRecord.CompletedFlag = playRecord.CompletedFlag;
            existingPlayRecord.HoursPlayed = playRecord.HoursPlayed;
            existingPlayRecord.PlayDescription = playRecord.PlayDescription;
            existingPlayRecord.Rating = playRecord.Rating;
            existingPlayRecord.ModifiedBy = existingUser.Username;
            existingPlayRecord.ModifiedDate = DateTime.Now;
            _genericRepository.UpdateRecord(existingPlayRecord);
        }
    }
}
