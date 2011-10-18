using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FalconerDevelopment.MantisConnect;
using FalconerDevelopment.MantisConnect.Model;
using NUnit.Framework;

namespace MantisConnect.Test.Integration
{
    [TestFixture]
    public class SessionTest
    {
        private string url;
        private string userName;
        private string password;

        [TestFixtureSetUp]
        public void Initialize()
        {
            url = "http://mantis.local/mantis-1.2.4/api/soap/mantisconnect.php";
            userName = "administrator";
            password = "root";
        }

        [Test]
        public void Is_Nullable_Fields()
        {
            var session = new Session(url, userName, password);
            IList<IFilter> projectVersions = session.GetFilters(1);

            Assert.IsNotNull(projectVersions);
            Assert.IsNotEmpty(projectVersions.ToList());
        }
    }
}
