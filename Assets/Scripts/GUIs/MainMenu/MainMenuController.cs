using ProjectTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject selectLevelPopup;

    private void Start()
    {
        SoundSystem.Instance.PlayBGM("BGM_MainMenu");
    }

    public void OnClickStartGame()
    {
        SoundSystem.Instance.PlaySFX("ButtonClick");
        selectLevelPopup.SetActive(true);
    }

    public void OnCloseLevelPopupClicked()
    {
        SoundSystem.Instance.PlaySFX("ButtonClick");
        selectLevelPopup.SetActive(false);
        SoundSystem.Instance.PlaySFX("ChangeScene");
    }

    public void OnLevelSelected(int id)
    {
        SoundSystem.Instance.PlaySFX("ButtonClick");
        SceneLoadSystem.Instance.LoadScene(id);
        SoundSystem.Instance.PlaySFX("ChangeScene");
    }

    public void OnQuitGameClicked()
    {
        SoundSystem.Instance.PlaySFX("ButtonClick");
        Application.Quit();
    }
}
