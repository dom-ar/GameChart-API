﻿namespace Entities.Exceptions.NotFound
{
    public sealed class GameNotFoundException : NotFoundException
    {
        public GameNotFoundException(int gameId)
            : base($"Game with id: {gameId} doesn't exist in the database.") { }
    }
}