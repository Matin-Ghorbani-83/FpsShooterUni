using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnUse : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    public void PlayClip()
    {
        AudioSource.PlayClipAtPoint(audioClip,transform.position);
    }
}
