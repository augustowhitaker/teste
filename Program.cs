using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Downy loca");
// app.MapPost("/", () => new{Name = "Augusto Whitaker", Age = 37});
// app.MapGet("/AddHeader", (HttpResponse response) => {
//     response.Headers.Add("Teste", "Augusto Whitaker");
//     return new {Name = "Augusto Whitaker", Age = 37};
//     });
app.MapPost("/saveproduct", (Product product) => {
    ProductRepository.Add(product);
});

app.MapGet("/getproduct/{code}", ([FromRoute] string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});
// app.MapGet("/getproduct", ([FromQuery] string dateStart,[FromQuery] string dateEnd) => {
//     return dateStart + " - " + dateEnd;
// });


app.MapGet("/getproductbyheader", (HttpRequest request) 
=> {
    return request.Headers["product-code"].ToString();

});
app.MapPut("/editproduct", (Product product) => {
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("/deleteproduct/{code}", ([FromRoute] string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});

app.Run();

public class ProductRepository{
    public static List<Product> Products { get; set; }

    public static void Add(Product product){
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code){
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product){
        Products.Remove(product);
    }
}

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}