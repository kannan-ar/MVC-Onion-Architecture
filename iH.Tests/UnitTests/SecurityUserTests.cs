namespace iH.Tests.UnitTests
{
    using NUnit.Framework;

    using Application;
    using Domain.Security.Entities;

    [TestFixture]
    public class SecurityUserTests
    {
        [Test]
        public void InvalidLogin()
        {
            SecurityUserService service = new SecurityUserService(new FakeSecurityUserRepository(null));
            SecurityUser user;

            service.CreateUser(new SecurityUser() { UserName = "testusr", Password = "123" }, new int[] { });

            Assert.IsFalse(service.Authenticate("testusr", "321", out user));
            Assert.IsNull(user);
        }

        [Test]
        public void TestRegisterAndLogin()
        {
            SecurityUserService service = new SecurityUserService(new FakeSecurityUserRepository(null));
            SecurityUser user;

            service.CreateUser(new SecurityUser() { UserName = "testusr", Password = "123" }, new int[] { });

            Assert.IsTrue(service.Authenticate("testusr", "123", out user));
            Assert.IsNotNull(user);
            Assert.AreEqual("testusr", user.UserName);
            Assert.AreNotEqual("123", user.Password);
        }
    }
}
