﻿
namespace Catalog.API.Products
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, 
        string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product() { Name = request.Name, ImageFile = request.ImageFile, Price = request.Price, Category = request.Category, Description = request.Description };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}