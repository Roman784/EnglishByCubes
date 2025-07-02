using Theme;
using UnityEngine;

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

        public void CompleteLevel(int number)
        {
            if (State.LastCompletedLevelNumber < number)
            {
                State.LastCompletedLevelNumber = number;
                _gameStateProvider.SaveGameState();
            }
        }

        public void SetLastCompletedLevelNumber(int number)
        {
            State.LastCompletedLevelNumber = number;
            _gameStateProvider.SaveGameState();
        }

        public void SetCurrentThemeMode(ThemeModes mode)
        {
            State.CurrentThemeMode = (int)mode;
            _gameStateProvider.SaveGameState();
        }

        public void AddCollectionItem(int id)
        {
            if (State.CollectedCollectionItems.Contains(id))
            {
                Debug.LogError($"An item in the collection with id {id} already exists!");
                return;
            }

            State.CollectedCollectionItems.Add(id);
            _gameStateProvider.SaveGameState();
        }

        public void SetCurrentCollectionItemFill(float fill)
        {
            fill = Mathf.Clamp01(fill);
            State.CurrentCollectionItemFill = fill;
            _gameStateProvider.SaveGameState();
        }

        public void SetMusicVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            State.MusicVolume = volume;
            _gameStateProvider.SaveGameState();
        }

        public void SetSoundVolume(float volume)
        {
            volume = Mathf.Clamp01(volume);
            State.SoundVolume = volume;
            _gameStateProvider.SaveGameState();
        }
    }
}
