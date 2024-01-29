namespace OpenClosedPrinciple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductType1 product=new ProductType1();
            product.Name = "Iphone 13Pro";
            product.Price = 50;
            product.ProductType = 1;

            Console.WriteLine($"Discount for {product.Name} is: {product.GetDiscount()}");
            Console.ReadKey();
        }
    }
}