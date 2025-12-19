<<<<<<< HEAD
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
using UnityEngine;

public class ApplyBurnGA : GameAction
{
    public int BurnDamage { get; private set; }
<<<<<<< HEAD
    public CombatantView Target { get; private set; }
=======
    public CombatantView Target {  get; private set; }
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0

    public ApplyBurnGA(int burnDamage, CombatantView target)
    {
        BurnDamage = burnDamage;
        Target = target;
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
