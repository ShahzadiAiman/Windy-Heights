using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private AudioSource popSoundSource;

    public void ChangeEffectVolume()
    {
        popSoundSource.volume = soundEffectSlider.value;
    }
}
