using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static bool playExplosion;
    public static bool playCombo;
    public static int comboType;

    public AudioClip[] A_Explosions;
    public AudioClip[] A_Combo;

    public AudioSource ExplosionSource;
    public AudioSource ComboSource;

    private void Update()
    {
        if (playExplosion)
        {
            playExplosion = false;
            if (!ExplosionSource.isPlaying)
            {
                ExplosionSource.PlayOneShot(A_Explosions[Random.Range(0, A_Explosions.Length)]);
            }
        }

        if (playCombo)
        {
            playCombo = false;
            ComboSource.PlayOneShot(A_Combo[comboType]);
        }
        
    }
}
