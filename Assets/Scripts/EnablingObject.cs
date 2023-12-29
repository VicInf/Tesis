using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingObject : MonoBehaviour
{
    public void SetActive(bool isActive)
    {
         gameObject.SetActive(isActive);
    }
}
