using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour {

    // OnBecameInvisible is called when the renderer is no longer visible by any camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
