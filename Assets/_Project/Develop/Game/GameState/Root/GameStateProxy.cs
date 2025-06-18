using Theme;

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

        public void SetLastCompletedLevelNumber(int number)
        {
            _gameState.LastCompletedLevelNumber = number;
            _gameStateProvider.SaveGameState();
        }

        public void SetCurrentThemeMode(ThemeModes mode)
        {
            _gameState.CurrentThemeMode = (int)mode;
            _gameStateProvider.SaveGameState();
        }
    }
}
