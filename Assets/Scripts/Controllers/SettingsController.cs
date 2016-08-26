using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class SettingsController : MonoBehaviour
{
    public GameObject _settingsDialogPrefab;

    private static int _volume;

    void Awake()
    {
        LoadSettings();
    }

    public void ShowSettingsDialog()
    {
        GameObject settingsDialog = Instantiate(_settingsDialogPrefab);
        Configure(settingsDialog);
    }

    private void Configure(GameObject settingsDialog)
    {
        settingsDialog.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        Text volumeValueText = settingsDialog.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "VolumeValueText");
        volumeValueText.text = _volume.ToString() + "%";

        Slider volumeSlider = settingsDialog.GetComponentsInChildren<Slider>().FirstOrDefault(s => s.name == "VolumeSlider");
        volumeSlider.onValueChanged.AddListener((value) => { volumeValueText.text = value.ToString() + "%"; });
        volumeSlider.value = _volume;

        ConfigureButtons(settingsDialog);
    }

    private void ConfigureButtons(GameObject settingsDialog)
    {
        Button exitButton = settingsDialog.GetComponentsInChildren<Button>().FirstOrDefault(b => b.name == "ExitButton");
        exitButton.onClick.AddListener(() => { Destroy(settingsDialog); });

        Button saveButton = settingsDialog.GetComponentsInChildren<Button>().FirstOrDefault(b => b.name == "SaveButton");
        saveButton.onClick.AddListener(() => { SaveSettings(settingsDialog); });

        Button cancelButton = settingsDialog.GetComponentsInChildren<Button>().FirstOrDefault(b => b.name == "CancelButton");
        cancelButton.onClick.AddListener(() =>
        {
            settingsDialog.GetComponentsInChildren<Slider>().FirstOrDefault(s => s.name == "VolumeSlider").value = _volume;
            settingsDialog.GetComponentsInChildren<Text>().FirstOrDefault(t => t.name == "VolumeValueText").text = _volume.ToString() + "%";
        });

    }

    private void SaveSettings(GameObject settingsDialog)
    {
        _volume = (int)settingsDialog.GetComponentsInChildren<Slider>().FirstOrDefault(s => s.name == "VolumeSlider").value;
        PlayerPrefs.SetInt("volume", _volume);
    }

    private void LoadSettings()
    {
        _volume = PlayerPrefs.GetInt("volume", 100);
    }
}
