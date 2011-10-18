using System;

namespace FalconerDevelopment.MantisConnect.Model
{
    public interface IAccount
    {
        String Email { get; }

        long Id { get; }

        String Name { get; }

        String RealName { get; }
    }
}