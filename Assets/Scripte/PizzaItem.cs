using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PizzaItem : HideableObjectItem
{
    public PizzaProps Props = new PizzaProps(PizzaOrders.None, -1);

    public Sprite spriteHawei, spriteTuna, spriteMeeresfruechte, spriteGarnelen, spriteNone;

    protected override void StartExtended()
    {
        if (this.spriteHawei == null)
        {
            Debug.Log("spite hawei has init");
        }
        else
        {
            Debug.Log("spite hawei fail");
        }
    }

    /// <summary>
    /// Wird benötigt, um die Pizza Darstellung der Bestellung anzuzeigen
    /// </summary>
    private Animator anim;

    private Func<PizzaOrders, float>[] _mapPrderToValue = new Func<PizzaOrders, float>[] 
    {
        order => order == PizzaOrders.Hawei ? 4f : -1f,
        order => order == PizzaOrders.Tuna ? 1f : -1f,
        order => order == PizzaOrders.Garnelen ? 2f : -1f,
        order => order == PizzaOrders.Meeresfruechten ? 3f : -1f,
        order => order == PizzaOrders.None ? -1f : -2f
    };

    public PizzaOrders ActualPizza() => this.Props.PizzaOrder;
    
    public void SetPizzaOrder(PizzaProps pp)
    {
        foreach (var func in this._mapPrderToValue)
        {
            var val = func.Invoke(pp.PizzaOrder);
            Debug.Log($"Anim Value: {val}");
            if(val < 0f)
            {
                
                continue;
            }
            
            if(this.anim != null) this.anim.SetFloat("PizzaOrderValue", val);

            if (this.Icon != null)
            {
                Debug.Log($"Set for map sprite {pp.PizzaOrder}");
              
                switch (pp.PizzaOrder)
                {
                    case PizzaOrders.Hawei:  this.Icon.sprite = this.spriteHawei; Debug.Log("- H"); break;
                    case PizzaOrders.Tuna:  this.Icon.sprite = this.spriteTuna; Debug.Log("- T"); break;
                    case PizzaOrders.Meeresfruechten:  this.Icon.sprite = this.spriteMeeresfruechte; Debug.Log("- Mf"); break;
                    case PizzaOrders.Garnelen:  this.Icon.sprite = this.spriteGarnelen; Debug.Log("- G"); break;
                    default: this.Icon.sprite = this.spriteNone; break;
                }
            }

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
        this.Props = new PizzaProps(PizzaOrders.None, -1);
    }
}