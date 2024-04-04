using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notes : MonoBehaviour
{
    public Sprite sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

}
