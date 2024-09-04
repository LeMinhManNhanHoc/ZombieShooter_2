using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgmSlider;

    private int targetFramerate;

    private void OnEnable()
    {
        UpdateSliderValue();

        Time.timeScale = 0f;

        targetFramerate = PlayerPrefs.GetInt("TargetFPS", 60);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void OnUpdateSFXSlider()
    {
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);

        SoundSystem.Instance.UpdateVolume();

        SoundSystem.Instance.PlaySFX("ButtonClick");
    }

    public void OnUpdateBGMSlider()
    {
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);

        SoundSystem.Instance.UpdateVolume();

        SoundSystem.Instance.PlaySFX("ButtonClick");
    }

    public void OnClickDefault()
    {
        PlayerPrefs.SetFloat("SFX", 0.5f);
        PlayerPrefs.SetFloat("BGM", 0.5f);
        PlayerPrefs.Save();

        SoundSystem.Instance.UpdateVolume();

        UpdateSliderValue();
    }

    public void OnClickSave()
    {
        PlayerPrefs.SetInt("TargetFPS", targetFramerate);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
        PlayerPrefs.SetFloat("BGM", bgmSlider.value);
        PlayerPrefs.Save();

        SoundSystem.Instance.UpdateVolume();
        OnCloseClick();
    }

    public void OnCloseClick()
    {
        Application.targetFrameRate = PlayerPrefs.GetInt("TargetFPS", 60);
        gameObject.SetActive(false);
    }

    private void UpdateSliderValue()
    {
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.5f);
    }

    public void OnClick30FPS()
    {
        targetFramerate = 30;

        Application.targetFrameRate = targetFramerate;
    }

    public void OnClick60FPS()
    {
        targetFramerate = 60;

        Application.targetFrameRate = targetFramerate;
    }
}
