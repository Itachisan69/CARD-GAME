using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnVFX;
<<<<<<< HEAD

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
=======
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);

>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
    }

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        Instantiate(burnVFX, target.transform.position, Quaternion.identity);
        target.Damage(applyBurnGA.BurnDamage);
        target.RemoveStatusEffect(StatusEffectType.BURN, 1);
<<<<<<< HEAD
        yield return new WaitForSeconds(1f);
    }
}
=======
        yield return new WaitForSeconds(1);
    }
}
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
