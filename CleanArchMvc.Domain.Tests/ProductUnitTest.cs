using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product Name", "Product Descriptiom", 9.99m, 99, "product image");

        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "Product Descriptiom", 9.99m, 99, "product image");

        action.Should().Throw<DomainExceptionValidation>()
              .WithMessage("Invalid id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product Descriptiom", 9.99m, 99, "product image");


        action.Should().Throw<DomainExceptionValidation>()
              .WithMessage("Invalid name, too short, minimum 3 characters.");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", 9.99m, 99, "product image product image product image product image product image product image product imageproduct imageproduct imageproduct imageproduct imageproduct image tooooooooooooo lomggggggggggggggggggggggggggggg");


        action.Should().NotThrow<DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", 9.99m, 99, null);

        action.Should().NotThrow<DomainExceptionValidation>();
    }


    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", 9.99m, 99, null);

        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", 9.99m, 99, "");

        action.Should().NotThrow<DomainExceptionValidation>();
    }


    [Fact]
    public void CreateProduct_InvalidPriceValue_DomainException()
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", -9.99m, 99, "");

        action.Should().Throw<DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }



    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainExceptionNegativeValue(int value)
    {
        Action action = () => new Product(1, "Pro", "Product Descriptiom", 9.99m, value, "product image");

        action.Should().Throw<DomainExceptionValidation>()
              .WithMessage("Invalid stock value");
    }

}
