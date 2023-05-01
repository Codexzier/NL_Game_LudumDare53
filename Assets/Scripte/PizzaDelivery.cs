using System;
using System.Collections.Generic;
using System.Linq;

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

    public void AddOrder(PizzaProps pp)
    {
        foreach (var pizzaItem in this.orders)
        {
            if (pizzaItem.ActualPizza() != PizzaOrders.None) continue;
            
            //Debug.Log($"Add order to item: {pp.PizzaOrder}");
            pizzaItem.SetPizzaOrder(new PizzaProps(pp.PizzaOrder, pp.PizzaId));
            pizzaItem.Show();
            break;
        }
    }

    public List<PizzaItem> GetOrders()
    {
        var prepare = new List<PizzaItem>();

        foreach (var pizzaItem in this.orders)
        {
            if(pizzaItem.ActualPizza() == PizzaOrders.None) break;

            var newItem = new PizzaItem();
            newItem.SetPizzaOrder(new PizzaProps(pizzaItem.Props.PizzaOrder, pizzaItem.Props.PizzaId));
            prepare.Add(newItem);
            
            pizzaItem.SetOrderNothing();
            pizzaItem.Hide();
        }
        
        return prepare;
    }

    public bool OrderListIsFull() => this.orders.All(a => a.ActualPizza() != PizzaOrders.None);
}