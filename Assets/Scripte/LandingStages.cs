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
        
        this.GameOver.NewGame();
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
        this.HUD.SetScore(this.Score, this.countMissings);
        
        if (this.countMissings >= 10)
        {
            this.SetGameOver();
            return;
        }
        
        // little more random 
        var r = this._random.Next(0, 100);
        if (r < 90) return;

        if(this.PizzaDelivery.OrderListIsFull()) return;
        
        // check the customers an open order
        var index = this._random.Next(0, this.LandingStageItems.Length);
        if(this.LandingStageItems[index].Item.ActualPizza() != PizzaOrders.None) return;

        var orderIndex = this._random.Next(0, this._orderMenu.Length);
        this.pizzaId++;
        PizzaProps pp = new PizzaProps(this._orderMenu[orderIndex], this.pizzaId);
        
        this.LandingStageItems[index].StartOrder(pp);
        
        this.PizzaDelivery.AddOrder(pp);
    }

    private void SetGameOver()
    {
        Debug.Log("Spielende");
        Time.timeScale = 0f;
        
        this.GameOver.SetScore(this.Score);
        this.GameOver.Show();
    }
}