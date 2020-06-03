using System;
using System.Collections.Generic;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Models.Links;

namespace UnitTests.TestModels
{
    public sealed class TestResource : Identifiable
    {
        [Attr] public string StringField { get; set; }
        [Attr] public DateTime DateTimeField { get; set; }
        [Attr] public DateTime? NullableDateTimeField { get; set; }
        [Attr] public int IntField { get; set; }
        [Attr] public int? NullableIntField { get; set; }
        [Attr] public Guid GuidField { get; set; }
        [Attr] public ComplexType ComplexField { get; set; }
        [Attr(AttrCapabilities.All & ~AttrCapabilities.AllowMutate)] public string Immutable { get; set; }
    }

    public class TestResourceWithList : Identifiable
    {
        [Attr] public List<ComplexType> ComplexFields { get; set; }
    }

    public class ComplexType
    {
        public string CompoundName { get; set; }
    }

    public sealed class OneToOnePrincipal : IdentifiableWithAttribute
    {
        [HasOne] public OneToOneDependent Dependent { get; set; }
    }

    public sealed class OneToOneDependent : IdentifiableWithAttribute
    {
        [HasOne] public OneToOnePrincipal Principal { get; set; }
        public int? PrincipalId { get; set; }
    }

    public sealed class OneToOneRequiredDependent : IdentifiableWithAttribute
    {
        [HasOne] public OneToOnePrincipal Principal { get; set; }
        public int PrincipalId { get; set; }
    }

    public sealed class OneToManyDependent : IdentifiableWithAttribute
    {
        [HasOne] public OneToManyPrincipal Principal { get; set; }
        public int? PrincipalId { get; set; }
    }

    public class OneToManyRequiredDependent : IdentifiableWithAttribute
    {
        [HasOne] public OneToManyPrincipal Principal { get; set; }
        public int PrincipalId { get; set; }
    }

    public sealed class OneToManyPrincipal : IdentifiableWithAttribute
    {
        [HasMany] public ISet<OneToManyDependent> Dependents { get; set; }
    }

    public class IdentifiableWithAttribute : Identifiable
    {
        [Attr] public string AttributeMember { get; set; }
    }

    public sealed class MultipleRelationshipsPrincipalPart : IdentifiableWithAttribute
    {
        [HasOne] public OneToOneDependent PopulatedToOne { get; set; }
        [HasOne] public OneToOneDependent EmptyToOne { get; set; }
        [HasMany] public ISet<OneToManyDependent> PopulatedToManies { get; set; }
        [HasMany] public ISet<OneToManyDependent> EmptyToManies { get; set; }
        [HasOne] public MultipleRelationshipsPrincipalPart Multi { get; set; }
    }

    public class MultipleRelationshipsDependentPart : IdentifiableWithAttribute
    {
        [HasOne] public OneToOnePrincipal PopulatedToOne { get; set; }
        public int PopulatedToOneId { get; set; }
        [HasOne] public OneToOnePrincipal EmptyToOne { get; set; }
        public int? EmptyToOneId { get; set; }
        [HasOne] public OneToManyPrincipal PopulatedToMany { get; set; }
        public int PopulatedToManyId { get; set; }
        [HasOne] public OneToManyPrincipal EmptyToMany { get; set; }
        public int? EmptyToManyId { get; set; }
    }

    public class Article : Identifiable
    {
        [Attr] public string Title { get; set; }
        [HasOne] public Person Reviewer { get; set; }
        [HasOne] public Person Author { get; set; }

        [HasOne(canInclude: false)] public Person CannotInclude { get; set; }
    }

    public class Person : Identifiable
    {
        [Attr] public string Name { get; set; }
        [HasMany] public ISet<Blog> Blogs { get; set; }
        [HasOne] public Food FavoriteFood { get; set; }
        [HasOne] public Song FavoriteSong { get; set; }
    }

    public class Blog : Identifiable
    {
        [Attr] public string Title { get; set; }
        [HasOne] public Person Reviewer { get; set; }
        [HasOne] public Person Author { get; set; }
    }

    public class Food : Identifiable
    {
        [Attr] public string Dish { get; set; }
    }

    public class Song : Identifiable
    {
        [Attr] public string Title { get; set; }
    }

    [Resource("catalog-product")]
    [Links(Link.None, Link.None, Link.None)]
    public class AnnotatedProduct : Identifiable
    {
        [Attr("product-name", AttrCapabilities.None)]
        public string Name { get; set; }

        [EagerLoad]
        public Money UnitPrice { get; set; }

        [HasOne("associated-image")]
        public Image Image { get; set; }

        public int ImageIdentifier { get; set; }

        [HasMany("associated-tags", Link.All, true)]
        public List<Tag> Tags { get; set; }

        [HasManyThrough("associated-categories", nameof(ProductCategories))]
        public List<Category> Categories { get; set; }

        public ISet<AnnotatedProductCategories> ProductCategories { get; set; }
    }

    public class UnAnnotatedProduct: Identifiable
    {        
        public string Name { get; set; }

        public Money UnitPrice { get; set; }

        public Image Image { get; set; }

        public int ImageIdentifier { get; set; }

        public List<Tag> Tags { get; set; }

        public List<Category> Categories { get; set; }

        public ISet<UnAnnotatedProductCategories> ProductCategories { get; set; }
    }

    public class Money : Identifiable
    {
        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }

    public class Category : Identifiable
    {
        public string Name { get; set; }

        public string Description { get; set; }        
    }

    public class Tag : Identifiable
    {
        public string Title { get; set; }  
        
        public AnnotatedProduct Product { get; set; }
    }

    public class Image : Identifiable
    {
        public Uri Url { get; set; }        
    }

    public class UnAnnotatedProductCategories
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public UnAnnotatedProduct Product { get; set; }
        public Category Category { get; set; }
    }

    public class AnnotatedProductCategories
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public AnnotatedProduct Product { get; set; }
        public Category Category { get; set; }
    }
}