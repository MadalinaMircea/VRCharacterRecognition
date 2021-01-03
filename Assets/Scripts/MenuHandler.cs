using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public void ToggleObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
}
