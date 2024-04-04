using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterModifierSOStamina : CharacterModifierSO 
{
    public override void AffectCharacter(GameObject character, float val)
    {
        FirstPersonController fpc = character.GetComponent<FirstPersonController>();
        if (fpc != null)
            fpc.StaminaChange((float)val);
    }
}
