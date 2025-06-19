namespace GameRoot
{
    public class SceneEnterParams
    {
        public readonly string SceneName;

        public SceneEnterParams(string sceneName)
        {
            SceneName = sceneName;
        }

        public T As<T>() where T : SceneEnterParams
        {
            return (T)this;
        }
    }
}
