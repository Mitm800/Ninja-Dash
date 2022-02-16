using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip Jump, Victory, Death, Inverse;
    static AudioSource audioSrc;

    private void Start() {
        Jump = Resources.Load<AudioClip>("Jump");
        Victory = Resources.Load<AudioClip>("Victory");
        Death = Resources.Load<AudioClip>("Death");
        Inverse = Resources.Load<AudioClip>("Inverse");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip){
        switch(clip) {
            case "Jump":
                audioSrc.PlayOneShot(Jump);
                break;
            case "Victory":
                audioSrc.PlayOneShot(Victory);
                break;
            case "Death":
                audioSrc.PlayOneShot(Death);
                break;
            case "Inverse":
                audioSrc.PlayOneShot(Inverse);
                break;
        }
    }
}
