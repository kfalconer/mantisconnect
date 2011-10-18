using System;
using FalconerDevelopment.MantisConnect.Model.MantisConnectWebservice;

namespace FalconerDevelopment.MantisConnect.Model
{
    public enum AccessLevel
    {
        AnyBody = 0,
        Viewer = 10,
        Reporter = 25,
        Updater = 40,
        Developer = 55,
        Manager = 70,
        Administrator = 90,
        Nobody = 100
    }

    public class Account : IAccount
    {
        private readonly AccountData data;

        internal AccountData Data { get { return data; } }

        public string Email
        {
            get { return data.email; }
        }

        public long Id
        {
            get { return Convert.ToInt64(data.id); }
        }

        public string Name
        {
            get { return data.name; }
        }

        public string RealName
        {
            get { return data.real_name; }
        }

        internal Account(AccountData accountData)
        {
            data = accountData;
        }
    }
}