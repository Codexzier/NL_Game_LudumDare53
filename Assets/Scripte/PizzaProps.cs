public class PizzaProps
{
    public PizzaOrders PizzaOrder;
    public int PizzaId;

    public PizzaProps(PizzaOrders toOrderPizza, int pizzaId)
    {
        this.PizzaOrder = toOrderPizza;
        this.PizzaId = pizzaId;
    }
}