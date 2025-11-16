/*
using UnityEngine;

public class ControleDeAudio : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


using UnityEngine;
using UnityEngine.UI;

public class ControleDeAudio : MonoBehaviour
{
    [Header("Referências")]
    public AudioSource audioSource;     // seu áudio
    public Slider sliderVolume;         // slider
    public Toggle toggleMute;           // toggle para desligar o áudio

    private float ultimoVolume = 1f;    // guarda o volume antes de mutar

    void Start()
    {
        // Define volume inicial no slider
        if (sliderVolume != null)
        {
            sliderVolume.value = audioSource.volume;
            sliderVolume.onValueChanged.AddListener(AlterarVolume);
        }

        // Conecta o toggle à função de mutar
        if (toggleMute != null)
            toggleMute.onValueChanged.AddListener(MutarDesmutar);
    }

    // Altera volume pelo slider
    public void AlterarVolume(float volume)
    {
        // Se estiver mutado, não faz nada
        if (toggleMute != null && toggleMute.isOn)
            return;

        audioSource.volume = volume;
        ultimoVolume = volume;
    }

    // Liga/Desliga o áudio
    public void MutarDesmutar(bool mutado)
    {
        if (mutado)
        {
            // Salva o volume antes de mutar
            ultimoVolume = audioSource.volume;

            // Volume zero
            audioSource.volume = 0f;

            // Bloqueia o slider quando mutado (opcional)
            sliderVolume.interactable = false;
        }
        else
        {
            // Volta ao último volume
            audioSource.volume = ultimoVolume;

            // Libera o slider novamente
            sliderVolume.interactable = true;
        }
    }
}



using UnityEngine;
using UnityEngine.UI;

public class ControleDeAudio : MonoBehaviour
{
    [Header("MÚSICA")]
    public AudioSource musicaSource;
    public Slider sliderMusica;
    public Toggle toggleMusica;
    private float ultimoVolumeMusica = 1f;

    [Header("EFEITOS SONOROS (SFX)")]
    public AudioSource sfxSource;
    public Slider sliderSFX;
    public Toggle toggleSFX;
    private float ultimoVolumeSFX = 1f;

    void Start()
    {
        // Música: slider + toggle
        if (sliderMusica != null)
        {
            sliderMusica.value = musicaSource.volume;
            sliderMusica.onValueChanged.AddListener(AlterarVolumeMusica);
        }
        if (toggleMusica != null)
            toggleMusica.onValueChanged.AddListener(ToggleMusica);

        // Efeitos: slider + toggle
        if (sliderSFX != null)
        {
            sliderSFX.value = sfxSource.volume;
            sliderSFX.onValueChanged.AddListener(AlterarVolumeSFX);
        }
        if (toggleSFX != null)
            toggleSFX.onValueChanged.AddListener(ToggleSFX);
    }

    // -----------------------
    //        MÚSICA
    // -----------------------
    public void AlterarVolumeMusica(float volume)
    {
        if (!toggleMusica.isOn)
        {
            musicaSource.volume = volume;
            ultimoVolumeMusica = volume;
        }
    }

    public void ToggleMusica(bool desligado)
    {
        if (desligado)
        {
            ultimoVolumeMusica = musicaSource.volume;
            musicaSource.volume = 0f;
            sliderMusica.interactable = false;
        }
        else
        {
            musicaSource.volume = ultimoVolumeMusica;
            sliderMusica.interactable = true;
        }
    }

    // -----------------------
    //         SFX
    // -----------------------
    public void AlterarVolumeSFX(float volume)
    {
        if (!toggleSFX.isOn)
        {
            sfxSource.volume = volume;
            ultimoVolumeSFX = volume;
        }
    }

    public void ToggleSFX(bool desligado)
    {
        if (desligado)
        {
            ultimoVolumeSFX = sfxSource.volume;
            sfxSource.volume = 0f;
            sliderSFX.interactable = false;
        }
        else
        {
            sfxSource.volume = ultimoVolumeSFX;
            sliderSFX.interactable = true;
        }
    }
}
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Volume Geral")]
    public Slider volumeSlider;
    public Toggle volumeToggle1;
    public Toggle volumeToggle2;
    public string volumeParameter = "MasterVolume";

    [Header("Efeitos Sonoros")]
    public Slider effectsSlider;
    public Toggle effectsToggle1;
    public Toggle effectsToggle2;
    public string effectsParameter = "EffectsVolume";

    private void Start()
    {
        // Inicializa Volume Geral
        InitSliderAndToggles(volumeSlider, volumeToggle1, volumeToggle2, volumeParameter);

        // Inicializa Efeitos Sonoros
        InitSliderAndToggles(effectsSlider, effectsToggle1, effectsToggle2, effectsParameter);
    }

    void InitSliderAndToggles(Slider slider, Toggle toggle1, Toggle toggle2, string param)
    {
        float db;
        audioMixer.GetFloat(param, out db);
        slider.value = Mathf.Pow(10, db / 20);

        bool isOn = slider.value > 0;
        toggle1.isOn = isOn;
        toggle2.isOn = isOn;

        // Adiciona listeners
        slider.onValueChanged.AddListener(value => UpdateVolume(param, value, toggle1, toggle2));
        toggle1.onValueChanged.AddListener(isToggleOn => ToggleVolume(param, isToggleOn, slider, toggle2));
        toggle2.onValueChanged.AddListener(isToggleOn => ToggleVolume(param, isToggleOn, slider, toggle1));
    }

    void UpdateVolume(string param, float value, Toggle toggleA, Toggle toggleB)
    {
        float db = value > 0 ? Mathf.Log10(value) * 20 : -80f;
        audioMixer.SetFloat(param, db);

        // Atualiza toggles sem causar loop
        toggleA.onValueChanged.RemoveAllListeners();
        toggleB.onValueChanged.RemoveAllListeners();
        bool isOn = value > 0;
        toggleA.isOn = isOn;
        toggleB.isOn = isOn;

        toggleA.onValueChanged.AddListener(isToggleOn => ToggleVolume(param, isToggleOn, null, toggleB));
        toggleB.onValueChanged.AddListener(isToggleOn => ToggleVolume(param, isToggleOn, null, toggleA));
    }

    void ToggleVolume(string param, bool isOn, Slider slider, Toggle otherToggle)
    {
        float value = isOn ? (slider != null ? slider.value : 1f) : 0f;
        audioMixer.SetFloat(param, value > 0 ? Mathf.Log10(value) * 20 : -80f);

        if (otherToggle != null)
        {
            otherToggle.onValueChanged.RemoveAllListeners();
            otherToggle.isOn = isOn;
            otherToggle.onValueChanged.AddListener(t => ToggleVolume(param, t, slider, null));
        }
    }
}