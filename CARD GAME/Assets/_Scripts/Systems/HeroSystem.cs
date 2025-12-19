using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSystem : Singelton<HeroSystem>
{
    [field: SerializeField] public HeroView HeroView { get; private set; }
<<<<<<< HEAD
    void OnEnable()
    {
        
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        
=======

    void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

<<<<<<< HEAD
    //Reactions
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
=======
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
        int burnStacks = HeroView.GetStatusEffectStacks(StatusEffectType.BURN);
        if (burnStacks > 0)
        {
            ApplyBurnGA applyBurnGA = new(burnStacks, HeroView);
            ActionSystem.Instance.AddReaction(applyBurnGA);
        }
<<<<<<< HEAD
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
=======
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }
    public void Setup(HeroData heroData)
    {
        HeroView.Setup(heroData);
    }
}
