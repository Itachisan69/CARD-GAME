using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardSystem : Singelton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawPilePoint;
    [SerializeField] private Transform discardPilePoint;

    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
<<<<<<< HEAD
   
=======
        
>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        
    }
    //Publics
    public void Setup(List<CardData> deckData)
    {
        foreach (var cardDatat in deckData)
        {
            Card card = new(cardDatat);
            drawPile.Add(card);
        }
    }

    //Performers
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)

    {

        int actualAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);

        int notDrawnAmount = drawCardsGA.Amount - actualAmount;

        for (int i = 0; i < actualAmount; i++)

        {

            yield return DrawCards();

        }

        if (notDrawnAmount > 0)

        {

            RefillDeck();

            for (int i = 0; i < notDrawnAmount; i++)

            {

                yield return DrawCards();

            }

        }

    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand)
        {
            
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)

    {

        hand.Remove(playCardGA.Card);

        CardView cardView = handView.RemoveCard(playCardGA.Card);

        yield return DiscardCard(cardView);



        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);

        ActionSystem.Instance.AddReaction(spendManaGA);

        if (playCardGA.Card.ManualTargetEffect != null)
        {
            PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
            ActionSystem.Instance.AddReaction(performEffectGA);
        }

        // Perform Effects

        foreach (var effectWrapper in playCardGA.Card.OtherEffects)

        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);

            ActionSystem.Instance.AddReaction(performEffectGA);

        }

    }


<<<<<<< HEAD
=======

    
    


>>>>>>> 776a8393a4012d4982778dc8e78dee93ef52c7d0
    private IEnumerator DrawCards()

    {

        Card card = drawPile.Draw();

        hand.Add(card);

        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);

        yield return handView.AddCard(cardView);

    }
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }

    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
}
