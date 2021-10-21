using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests
{
    [TestClass]
    public class CustomerManagerTests
    {
        Mock<ICustomerDal> _mockCustomerDal;
        List<Customer> _dbCustomers;

        [TestInitialize]
        public void Start()
        {
            _mockCustomerDal = new Mock<ICustomerDal>();
            _dbCustomers = new List<Customer>
            {
                new Customer{Id=1, FirstName="Ali"},
                new Customer{Id=2, FirstName="Ahmet"},
                new Customer{Id=3, FirstName="Ayşe"},
                new Customer{Id=4, FirstName="Aydın"},
                new Customer{Id=5, FirstName="Burhan"},
            };
            _mockCustomerDal.Setup(m => m.GetAll()).Returns(_dbCustomers);
        }

        [TestMethod]
        public void Tum_musteriler_listelenebilmelidir()
        {
            // Arrange : Test için gerekli ortamı oluşturmaktır.
            ICustomerService customerService = new CustomerManager(_mockCustomerDal.Object);

            // Act : Aksiyon almak demektir.
            List<Customer> customers = customerService.GetAll();

            // Assert
            Assert.AreEqual(5, customers.Count);
        }

        [TestMethod]
        public void A_ile_baslayan_dort_musteri_gelmelidir()
        {
            ICustomerService customerService = new CustomerManager(_mockCustomerDal.Object);
            List<Customer> customers = customerService.GetCustomerByInitial("A");
            Assert.AreEqual(4, customers.Count);
        }

        [TestMethod]
        public void Kucuk_a_ile_baslayan_dort_musteri_gelmelidir()
        {
            ICustomerService customerService = new CustomerManager(_mockCustomerDal.Object);
            List<Customer> customers = customerService.GetCustomerByInitial("a");
            Assert.AreEqual(4, customers.Count);
        }
    }
}
