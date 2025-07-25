using Configs;
using Zenject;

namespace UI
{
    public class TheoryPopUpProvider : PopUpsProvider
    {
        private TheoryTableOfContentsPopUp.Factory _tableOfContentsPopUp;

        [Inject]
        private void Construct(TheoryTableOfContentsPopUp.Factory tableOfContentsPopUp)
        {
            _tableOfContentsPopUp = tableOfContentsPopUp;
        }

        public void OpenTableOfContents(TheoryLevelConfigs configs)
        {
            _tableOfContentsPopUp.Create(configs).Open();
        }
    }
}