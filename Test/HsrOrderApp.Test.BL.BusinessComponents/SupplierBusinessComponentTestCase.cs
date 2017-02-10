#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.BL.BusinessComponents
{
    /// <summary>
    /// Summary description for SupplierServiceTestCase
    /// </summary>
    [TestClass]
    public class SupplierBusinessComponentTestCase
    {
        private ISupplierRepository context;
        private Mockery mockBuilder;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize()]
        public void Initialize()
        {
            mockBuilder = new Mockery();
            context = mockBuilder.NewMock<ISupplierRepository>();
        }

        [TestMethod]
        public void TestGetSupplierById()
        {
            SupplierBusinessComponent service = new SupplierBusinessComponent(this.context);
            Supplier supplier = new Supplier() {SupplierId = 123};

            Expect.Once.On(context).Method("GetById").Will(Return.Value(supplier));
            Supplier resultSupplier = service.GetSupplierById(123);
            Assert.AreEqual<decimal>(supplier.SupplierId, resultSupplier.SupplierId);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestGetSupplierByCriteria()
        {
            SupplierBusinessComponent service = new SupplierBusinessComponent(this.context);
            Supplier supplier = new Supplier() {SupplierId = 456, Name = "FakeSupplier"};
            IList<Supplier> suppliers = new List<Supplier>();
            suppliers.Add(supplier);

            foreach (SupplierSearchType type in Enum.GetValues(typeof (SupplierSearchType)))
            {
                Expect.Once.On(context).Method("GetAll").Will(Return.Value(suppliers.AsQueryable()));
                IQueryable<Supplier> resultSuppliers = service.GetSuppliersByCriteria(type, "FakeSupplier");
                Assert.AreEqual<decimal>(1, resultSuppliers.Count());
                Assert.AreEqual<decimal>(supplier.SupplierId, resultSuppliers.First().SupplierId);
            }

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestStoreSupplier()
        {
            int supplierId = 123;
            SupplierBusinessComponent service = new SupplierBusinessComponent(this.context);
            Supplier supplier = new Supplier() {SupplierId = 456, Name = "FakeSupplier"};

            Expect.Once.On(context).Method("SaveSupplier").Will(Return.Value(supplierId));
            int resultSupplierId = service.StoreSupplier(supplier);
            Assert.AreEqual<int>(supplierId, resultSupplierId);

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestDeleteSupplier()
        {
            SupplierBusinessComponent service = new SupplierBusinessComponent(this.context);

            Expect.Once.On(context).Method("DeleteSupplier").With(1);
            service.DeleteSupplier(1);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }
    }
}