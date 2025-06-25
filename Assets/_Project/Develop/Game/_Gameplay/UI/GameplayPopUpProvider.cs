using Zenject;

namespace UI
{
    public class GameplayPopUpProvider : PopUpsProvider
    {
        private LevelCompletionPopUp.Factory _levelCompletionPopUp;

        [Inject]
        private void Construct(LevelCompletionPopUp.Factory levelCompletionPopUp)
        {
            _levelCompletionPopUp = levelCompletionPopUp;
        }

        public void OpenLevelCompletionPopUp()
        {
            _levelCompletionPopUp.Create().Open();
        }
    }
}