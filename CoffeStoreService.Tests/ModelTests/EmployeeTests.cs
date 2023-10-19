namespace CoffeStoreService.Tests.ModelTests
{
    internal class EmployeeTests
    {
        [Test]
        public void Cannot_Add_Employee_AsNonAdmin()
        {
            var notAdmin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Salesman);

            string expectedMessage = "Its necessary to be an admin to conclude the operation";

            string? actualExceptionMessage = Assert.Throws<NotAdminException>(() =>
            {
                notAdmin.AddEmployee(new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Salesman));
            }).Message;

            Assert.AreEqual(expectedMessage, actualExceptionMessage);
        }

        [Test]
        public void Cannot_Add_ShopAddress_AsNonAdmin()
        {
            var notAdmin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Salesman);

            string expectedMessage = "Its necessary to be an admin to conclude the operation";

            string? actualExceptionMessage = Assert.Throws<NotAdminException>(() =>
            {
                notAdmin.AddNewShopAddress(new ShopAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP"));
            }).Message;

            Assert.AreEqual(expectedMessage, actualExceptionMessage);
        }

        [Test]
        public void Adding_Employee_AsAdmin_HaveToBeSucceeded()
        {
            var admin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Admin);
            var salesman = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Salesman);

            admin.AddEmployee(salesman);

            Assert.IsTrue(admin.Subordinates.Contains(salesman));
        }

        [Test]
        public void Adding_ShopAddress_AsAdmin_HaveToBeSucceeded()
        {
            var admin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Admin);
            var shopAddress = new ShopAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");

            admin.AddNewShopAddress(shopAddress);

            Assert.IsTrue(admin.ShopAddresses.Contains(shopAddress));
        }

        [Test]
        public void Disable_Employee_AsAdmin_HaveToBeSucceeded()
        {
            var admin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Admin);
            var salesman = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Salesman);

            admin.AddEmployee(salesman);
            var disabledSubordinate = admin.DisableEmployee(salesman);

            Assert.IsFalse(disabledSubordinate?.IsEnabled);
            Assert.IsFalse(admin.Subordinates.Contains(salesman));
        }

        [Test]
        public void Remove_ShopAddress_AsAdmin_HaveToBeSucceeded()
        {
            var admin = new Employee("joão", "admin@admin.com", "admin", "admin", ProfileEnum.Admin);
            var shopAddress = new ShopAddress("123", "Rua Test", 123, "bloco 8", "Vila Test", "Test City", "SP");

            admin.AddNewShopAddress(shopAddress);
            admin.RemoveShopAddress(shopAddress);

            Assert.IsFalse(admin.ShopAddresses.Contains(shopAddress));
        }       
    }
}
