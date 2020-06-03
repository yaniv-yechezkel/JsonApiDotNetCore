using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Fluent;
using System.Threading.Tasks;
using UnitTests.Specifications;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.Fluent.HasManyBuilderTests
{
    public sealed class When_Calling_CanInclude : HasManyBuilderSpecificationBase
    {
        HasManyBuilder<UnAnnotatedProduct> _hasManyBuilder;

        protected override async Task Given()
        {
            await base.Given();

            _resourceContext.ResourceType = typeof(UnAnnotatedProduct);

            _property = typeof(UnAnnotatedProduct).GetProperty("Tags");

            _hasManyBuilder = new HasManyBuilder<UnAnnotatedProduct>(_resourceContext, _options, _property);

            _hasManyBuilder.Build();
        }

        protected override async Task When()
        {
            await base.When();

            _hasManyBuilder.CanInclude(false);
        }

        [Then]
        public void It_Should_Configure_CanInclude()
        {            
            Assert.False(((HasManyAttribute)_resourceContext.Relationships[0]).CanInclude);            
        }        
    }
}