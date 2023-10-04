using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterstialChangeSceneManager
{
    static int contChangeScenesForView = 0;

    const int scenesForView = 5;

    public static bool OnChangeScene()
    {
        contChangeScenesForView++;

        if (contChangeScenesForView >= scenesForView)
        {
            contChangeScenesForView = 0;
            return true;
        }

        return false;
    }
}

