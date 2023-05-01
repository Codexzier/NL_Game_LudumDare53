using System;
using UnityEngine;

public class LandingStage : ReachableLandingStage
{
    public PizzaItem Item;
    
    private bool hasOrder;
    
    public float stepTime = 0.03f;
    public float countdownForNextOrderMin = 3f;
    public float countdownForNextOrder;
    public float orderTimeStatus;
    public float orderTimeStatusMax = 1f;

    public int pointsForPizzaDelivered;

    public int countDeliverdOutofTime;
    
    public DeliveryOutOfTimeIcon deliveryOutOfTimeIcon { get; set; }
    
    public bool HasAnOrder { get; set; }
    public bool HasOrdered { get; set; }

    private int _points;

    public void Start()
    {
        if (this.Item == null) throw new NullReferenceException("Pizza Order Icon muss zugewiesen werden!");
        
        this.Item.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.hasOrder) return;

        var step = Time.deltaTime * this.stepTime;
        this.countdownForNextOrder += step;
        
        if (this.Item.ActualPizza() == PizzaOrders.None) return;

        // TODO: Warum wird hier zurück gesetzt
        this.countdownForNextOrder = 0f;

        if (this.HasAnOrder) this.CheckHasOrdered();

        this.orderTimeStatus += step;
        if (this.orderTimeStatus < orderTimeStatusMax) return;

        this.deliveryOutOfTimeIcon.Show();
    }

    public void StartOrder(PizzaProps pp)
    {
        //if(this.countdownForNextOrder < this.countdownForNextOrderMin) return;
        
        Debug.Log($"Start Order set pizza Item {pp.PizzaOrder}");
        this.Item.SetPizzaOrder(pp);
        this.Item.Show();

        
        this.orderTimeStatus = 0;
    }

    private void CheckHasOrdered()
    {
        if(!this.HasOrdered) return;

        // ein Punkt Abzug wenn außerhalb der Zeit
        if (this.countdownForNextOrder > 1f)
        {
            this._points--;
            this.countDeliverdOutofTime++;
        }

        this.pointsForPizzaDelivered += this._points;

        this.HasOrdered = false;
        this.HasAnOrder = false;
        this.Item.SetOrderNothing();
        
        this.deliveryOutOfTimeIcon.Hide();

        
       this.Item.Hide();

        this.countdownForNextOrder = 0f;
        
        if(this.countdownForNextOrderMin < 0.5f) return;
        this.countdownForNextOrderMin -= 0.1f;
    }

    public void DeliverPizza(PizzaOrders order)
    {
        // no order exist
        if(!this.HasOrdered) return;
        
        this.OnDelivery(order);
    }
    
    public bool OnDelivery(PizzaOrders pizzaOrder)
    {
        if (pizzaOrder == PizzaOrders.None) return false;
        
        this._points = 2;
        if (pizzaOrder == this.Item.ActualPizza()) this._points = 3;
        
        return true;
    }
}