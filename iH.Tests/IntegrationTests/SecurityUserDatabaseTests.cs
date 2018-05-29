namespace iH.Tests.IntegrationTests
{
    using NUnit.Framework;
    using System.Configuration;
    using Npgsql;

    using Application;
    using Infrastructure;
    using Domain.Security.Entities;
    using Infrastructure.Core;

    [TestFixture]
    public class SecurityUserDatabaseTests
    {
        private void ClearData()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["SecurityConnection"].ConnectionString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("TRUNCATE TABLE \"SecurityUsers\"", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        [SetUp]
        public void Init()
        {
            ClearData();
        }

        [Test]
        public void TestRegisterAndLogin()
        {
            SecurityUserService service = new SecurityUserService(
                new SecurityUserRepository(
                    new Database(ConfigurationManager.ConnectionStrings["SecurityConnection"].ConnectionString)));
            SecurityUser user;

            service.CreateUser(new SecurityUser() { UserName = "testusr", Password = "123"},new int[] { });

            Assert.IsTrue(service.Authenticate("testusr", "123", out user));
            Assert.IsNotNull(user);
            Assert.Greater(user.UserId, 0);
            Assert.AreNotEqual("123", user.Password);
        }
    }
}
