using System;
using System.Collections.Generic;

public class PizzaDelivery : ReachableLandingStage
{
    public PizzaItem[] orders;

    private void Start()
    {
        if (this.orders == null) throw new NullReferenceException("Pizza Items nicht zu gewiesen!");
        
        foreach (var pizzaItem in this.orders)
        {
            pizzaItem.Hide();
        }
        
    }

    public void AddOrder(PizzaOrders toOrderPizza)
    {
        foreach (var pizzaItem in this.orders)
        {
            if (pizzaItem.PizzaOrder != PizzaOrders.None) continue;
            
            pizzaItem.PizzaOrder = toOrderPizza;
            pizzaItem.Show();
            break;
        }
    }

    public PizzaItem[] GetOrders()
    {
        var prepare = new List<PizzaItem>();

        foreach (var pizzaItem in this.orders)
        {
            if(pizzaItem.PizzaOrder == PizzaOrders.None) break;
            
            prepare.Add(new PizzaItem { PizzaOrder = pizzaItem.PizzaOrder});
        }

        return prepare.ToArray();
    }
}