using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static bool playExplosion;
    public static bool playCombo;
    public static int comboType;
    public static bool playSatellite;


    public AudioClip[] A_Explosions;
    public AudioClip[] A_Combo;
    public AudioClip A_Satellite;

    public AudioSource ExplosionSource;
    public AudioSource ComboSource;
    public AudioSource SatelliteSource;

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

        if (playSatellite)
        {
            playSatellite = false;
            SatelliteSource.PlayOneShot(A_Satellite);
        }

    }
}
