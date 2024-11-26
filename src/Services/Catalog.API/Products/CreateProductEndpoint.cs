namespace Catalog.API.Products
{
    public record CreateProductRequest(string Name, List<string> Category, string Description,
    string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("products", async (CreateProductRequest request, ISender sender) =>
            {
                var cmd = request.Adapt<CreateProductCommand>();
                var r = await sender.Send(cmd);
                var response = r.Adapt<CreateProductResponse>();
                return Results.Created($"products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}