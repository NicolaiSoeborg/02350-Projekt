namespace OLProgram.Interfaces
{
    // Recommendations for Abstract Classes vs. Interfaces
    // https://msdn.microsoft.com/en-us/library/scsyfw1d(v=vs.71).aspx
    // TODO: Læs og find ud af om interfacet ser korrekt ud
    // TODO: Hvordan specificeres at alle produkter skal have en constructor med bestemt signatur?

    public interface IProduct
    {
        int ProductId { get; }
        string Name { get; set; }
        string ImageFileName { get; set; }
        int Stock { get; set; }
        int Bought { get; set; }
    }
}
