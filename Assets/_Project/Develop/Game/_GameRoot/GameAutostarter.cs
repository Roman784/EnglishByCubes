using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRoot
{
    public class GameAutostarter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if (SceneManager.GetActiveScene().name != Scenes.BOOT)
                SceneManager.LoadScene(Scenes.BOOT);
        }
    }
}