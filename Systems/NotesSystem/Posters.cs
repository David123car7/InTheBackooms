using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posters : MonoBehaviour
{
    public Sprite sprite { get; set; }

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
    }
}
