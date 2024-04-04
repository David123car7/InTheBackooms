using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterBatteryModifierSO : CharacterModifierSO 
{
    public override void AffectCharacter(GameObject character, float val)
    {
        FlashLightSystem flashlight = character.GetComponent<FlashLightSystem>();
        if (flashlight != null)
            flashlight.GainBattery((int)val);
    }
}