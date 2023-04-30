using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LandingStages : MonoBehaviour
{
    private readonly System.Random _random = new (DateTime.Now.Second);
    public LandingStage[] LandingStageItems;
    public PizzaDelivery PizzaDelivery;

    public int countMissings = 0;

    public GameOverBoard GameOver;
    
    public int Score { get; set; }
    
    void Start()
    {
        if (this.PizzaDelivery == null) throw new NullReferenceException("Pizza Lieferant nicht zugewiesen");

        // set random time to order
        foreach (var item in this.LandingStageItems)
        {
            item.countdownForNextOrder = (float) this._random.NextDouble();
        }
    }

    private PizzaOrders[] _orderMenu =
    {
        PizzaOrders.Hawei,
        PizzaOrders.Garnelen,
        PizzaOrders.Tuna,
        PizzaOrders.Meeresfruechten
    };
    
    void Update()
    {
        this.Score = this.LandingStageItems.Sum(item => item.pointsForPizzaDelivered);
        this.countMissings = this.LandingStageItems.Sum(item => item.countDeliverdOutofTime);

        if (this.countMissings >= 10)
        {
            this.SetGameOver();
            return;
        }
        
        //if(this._orders)
        
        // little more random 
        var r = this._random.Next(0, 100);
        if (r < 80) return;
        
        // check the customers an open order
        var index = this._random.Next(0, this.LandingStageItems.Length);
        if(this.LandingStageItems[index].Item.PizzaOrder != PizzaOrders.None) return;

        var orderIndex = this._random.Next(0, this._orderMenu.Length);

        var toOrderPizza = this._orderMenu[orderIndex];
        this.LandingStageItems[index].StartOrder(toOrderPizza);
        this.PizzaDelivery.AddOrder(toOrderPizza);
    }

    private void SetGameOver()
    {
        Debug.Log("Spielende");

        this.GameOver.Score = this.Score;

        this.GameOver.Show();
    }
}