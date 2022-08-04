using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODPlayer : MonoBehaviour
{
    void PlayFootstepsEvent(string path)
    {
        FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.start();
        Footsteps.release();
    }
}
