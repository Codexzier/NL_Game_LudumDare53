using System;
using UnityEngine;

public class PizzaItem : HideableObjectItem
{
    public PizzaProps Props = new PizzaProps(PizzaOrders.None, -1);
    
    /// <summary>
    /// Wird benötigt, um die Pizza Darstellung der Bestellung anzuzeigen
    /// </summary>
    private Animator anim;

    private Func<PizzaOrders, float>[] _mapPrderToValue = 
    {
        order => {
         if (order == PizzaOrders.Hawei)
         {
             return 0f;
         }

         return -1f;
        },
        order => order == PizzaOrders.Garnelen ? 1f : -1f,
        order => order == PizzaOrders.Meeresfruechten ? 1f : -1f,
        order => order == PizzaOrders.Tuna ? 1f : -1f
    };

    public PizzaOrders ActualPizza() => this.Props.PizzaOrder;

    public void SetPizzaOrder(PizzaProps pp)
    {
        foreach (var func in this._mapPrderToValue)
        {
            var val = func.Invoke(pp.PizzaOrder);
            if(val < 0f) continue;
            
            if(this.anim != null) this.anim.SetFloat("PizzaOrderValue", val);

            this.Props = pp;
            break;
        }
    }

    private void Awake()
    {
        this.anim = this.GetComponent<Animator>();

        if (this.anim == null) throw new NullReferenceException("Animtor nicht zu gewiesen!");
    }

    public void SetOrderNothing()
    {
        this.Props.PizzaOrder = PizzaOrders.None;
    }
}