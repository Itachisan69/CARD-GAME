using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCardEffect : Effect
{
    [SerializeField] private int drawAmount;
    public override GameAction GetGameAction()
    {
        DrawCardsGA drawCardsGA = new(drawAmount);
        return drawCardsGA;
    }
}
