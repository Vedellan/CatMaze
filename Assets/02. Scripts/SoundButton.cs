using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioSource bgm;
    public Sprite[] buttonSprite = new Sprite[2];
    public Image soundButton;

    private void Awake()
    {
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
        soundButton = GetComponent<Image>();
    }

    public void ToggleSoundOnOff()
    {
        if(bgm.volume == 0)
        {
            SoundOn();
        }

        else
        {
            SoundOff();
        }
    }

    private void SoundOn()
    {
        bgm.volume = 1;
        soundButton.sprite = buttonSprite[1];
    }

    private void SoundOff()
    {
        bgm.volume = 0;
        soundButton.sprite = buttonSprite[0];
    }
}
