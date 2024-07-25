using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEffects : MonoBehaviour
{
    [SerializeField] GameObject[] Effects;
    [SerializeField] Transform[] EffectLocations;
    [SerializeField] float[] EffectDestroyDelays;
    [SerializeField] GunScript gunscript;
    void Update()
    {
        if (gunscript.EffectSignal)
        {
            gunscript.EffectSignal = false;
            for (int i = 0; i < Effects.Length; i++)
            {
                Destroy(Instantiate(Effects[i], EffectLocations[i].position, Quaternion.identity), EffectDestroyDelays[i]);
            }
        }
    }
}
