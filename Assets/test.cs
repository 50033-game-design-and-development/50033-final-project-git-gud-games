using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void OnBecameVisible() {
        Debug.Log("seen");
    }

    void OnBecameInvisible() {
        Debug.Log("cant see me");
    }
}
