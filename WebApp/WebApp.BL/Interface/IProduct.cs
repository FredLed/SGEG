using System;

namespace WebApp.BL.Interface
{
    public interface IProduct
    {
        Guid Id { get; }

        string Name { get; }

        double MSRP { get; }

        string Description { get; }

        DateTime CreationDate { get; }

        string CUP { get; }

        ICategory Category { get; }
    }
}