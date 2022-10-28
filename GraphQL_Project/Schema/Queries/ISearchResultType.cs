using HotChocolate.Types;
using System;

namespace GraphQL_Project.Schema.Queries
{
    [UnionType("SearchResult")]
    //[InterfaceType("SearchResult")]
    public interface ISearchResultType
    {
        //public Guid Id { get; set; }
    }
}