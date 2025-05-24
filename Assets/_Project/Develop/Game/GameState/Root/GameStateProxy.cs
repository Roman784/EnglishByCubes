using R3;
using UnityEngine;
using Zenject;

namespace GameState
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        private readonly IGameStateProvider _gameStateProvider;

        public GameState State => _gameState;

        public GameStateProxy(GameState gameState, IGameStateProvider gameStateProvider)
        {
            _gameState = gameState;
            _gameStateProvider = gameStateProvider;
        }
    }
}
