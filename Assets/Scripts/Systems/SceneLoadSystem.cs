using UnityEngine.SceneManagement;

public class SceneLoadSystem : MonoSingleton<SceneLoadSystem>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
        SoundSystem.Instance.PlayBGM("BGM_Map_1");
    }
}
