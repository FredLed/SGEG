using System;

namespace WebApp.BL.Interface
{
    public interface IItem
    {
        Guid Id { get; }

        IProduct Product { get; }

        double Cost { get; }

        string SerialNumber { get; }

        DateTime CreationDate { get; }

        DateTime ReceptionDate { get; }
    }
}