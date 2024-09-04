using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadSystem : MonoSingleton<SceneLoadSystem>
{
    protected override void Awake()
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("TargetFPS", 60); ;

        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void LoadScene(int id)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(id);
        SoundSystem.Instance.PlayBGM("BGM_Map_1");
    }
}
