using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{

    public float delay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(HideMe), delay);
    }

    private void HideMe(){
        gameObject.SetActive(false);
    }
}
