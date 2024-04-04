using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory.Model
{
    [CreateAssetMenu]
    public class CollectibleItemSO : ItemSO
    {
        public string ActionName => "Use";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

    }
}