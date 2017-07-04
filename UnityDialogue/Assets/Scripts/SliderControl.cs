using UnityEngine;
using System.Collections;

public class SliderControl : MonoBehaviour
{

    public void BGMSliderChanged(float value)
    {
        AudioManager.Instance.BackGroundMusicVolumeControl(value);
    }

    public void SFXSliderChanged(float value)
    {
        AudioManager.Instance.SFXVolumeControl(value);
    }
}
