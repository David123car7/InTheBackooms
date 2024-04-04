using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterItemConsumableModifierSO : CharacterModifierSO //Modificador para a sanidade
{
    public override void AffectCharacter(GameObject character, float val)
    {
        Sanaty sanaty = character.GetComponent<Sanaty>();
        if (sanaty != null)
            sanaty.GainSanatyItem((int)val);
    }
}
