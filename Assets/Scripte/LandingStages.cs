using System;
using System.Linq;
using UnityEngine;

public class LandingStages : MonoBehaviour
{
    private readonly System.Random _random = new (DateTime.Now.Second);
    public LandingStage[] LandingStageItems;
    public PizzaDelivery PizzaDelivery;
    public HeadUpDisplay HUD;

    public int countMissings = 0;

    public GameOverBoard GameOver;
    
    public int Score { get; set; }

    private int pizzaId = 0;
    
    void Start()
    {
        if (this.PizzaDelivery == null) throw new NullReferenceException("Pizza Lieferant nicht zugewiesen");

        // set random time to order
        foreach (var item in this.LandingStageItems)
        {
            if (item == null) throw new NullReferenceException("LandingStage nicht zugewiesen!");
                
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
        if (this.LandingStageItems == null) throw new NullReferenceException("LandingStage nicht zugewiesen!");
        
        this.Score = this.LandingStageItems.Sum(item => item.pointsForPizzaDelivered);
        this.countMissings = this.LandingStageItems.Sum(item => item.countDeliverdOutofTime);
        this.HUD.SetScore(this.Score);
        
        if (this.countMissings >= 10)
        {
            this.SetGameOver();
            return;
        }
        
        //if(this._orders)
        
        // little more random 
        var r = this._random.Next(0, 100);
        if (r < 90) return;

        
        //if(this.LandingStageItems.All(a => a.Item.ActualPizza() != PizzaOrders.None)) return;
        //Debug.Log($"New order come in! {this.LandingStageItems.Length}");
        if(this.PizzaDelivery.OrderListIsFull()) return;
        
        // check the customers an open order
        var index = this._random.Next(0, this.LandingStageItems.Length);
        if(this.LandingStageItems[index].Item.ActualPizza() != PizzaOrders.None) return;
        
        //if(this.LandingStageItems[index].countdownForNextOrder < this.LandingStageItems[index].countdownForNextOrderMin) return;
        
        Debug.Log($"Customer has actual! {this.LandingStageItems[index].Item.ActualPizza() }");

        var orderIndex = this._random.Next(0, this._orderMenu.Length);
        this.pizzaId++;
        PizzaProps pp = new PizzaProps(this._orderMenu[orderIndex], this.pizzaId);
        
        this.LandingStageItems[index].StartOrder(pp);
        Debug.Log($"Check Customer has actual! {this.LandingStageItems[index].Item.ActualPizza() }");
        
        this.PizzaDelivery.AddOrder(pp);
        Debug.Log($"An customer order: {pp.PizzaOrder}, ID {pp.PizzaId}");
    }

    private void SetGameOver()
    {
        Debug.Log("Spielende");

        this.GameOver.Score = this.Score;

        this.GameOver.Show();
    }
}