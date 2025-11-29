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
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
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
        // 1. Calculate and perform initial draws
        int cardsToDraw = drawCardsGA.Amount;
        int initialDrawCount = Mathf.Min(cardsToDraw, drawPile.Count);

        for (int i = 0; i < initialDrawCount; i++)
        {
            yield return DrawCards();
        }

        // Update the number of cards still needed
        int remainingDraws = cardsToDraw - initialDrawCount;

        // 2. Handle Refill and remaining draws
        if (remainingDraws > 0)
        {
            RefillDeck();

            // CRITICAL FIX: Recalculate how many of the 'remainingDraws' are now available
            int postRefillDrawCount = Mathf.Min(remainingDraws, drawPile.Count);

            for (int i = 0; i < postRefillDrawCount; i++)
            {
                yield return DrawCards();
            }
        }
    }

    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach (var card in hand)
        {
            discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        Card cardToDiscard = playCardGA.Card;

        // 1. Remove the card from the hand
        hand.Remove(cardToDiscard);

        // 2. FIX: Add the card object to the discard pile list
        discardPile.Add(cardToDiscard);

        // 3. Handle the CardView visual removal/discard
        CardView cardView = handView.RemoveCard(cardToDiscard);
        yield return DiscardCard(cardView); // This handles the visual movement and destruction

        // 4. Perform Game Actions (Mana, Effects, etc.)
        SpendManaGA spendManaGA = new(cardToDiscard.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);

        foreach (var effect in cardToDiscard.Effects)
        {
            PerformEffectGA performEffectGA = new(effect);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
    }

    //Reactions
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }


    private IEnumerator DrawCards()
    {
        Card card = drawPile.Draw();
        if (card == null)
        {
            // This should not happen if logic is correct, but handles the 'default' return.
            Debug.LogWarning("Attempted to draw from an empty pile after refill logic failed. Skipping card draw.");
            yield break;
        }
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
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
    }
}
