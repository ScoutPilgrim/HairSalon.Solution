using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using MySql.Data.MySqlClient;

namespace HairSalon.TestTools
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.ClearAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ian_christner_test;";
    }

    [TestMethod]
    public void Equals_FunctionTest_True()
    {
      Stylist test_1 = new Stylist("test", 123456789, 1);
      Stylist test_2 = new Stylist("test", 123456789, 1);

      Assert.AreEqual(test_1, test_2);
    }
    [TestMethod]
    public void Equals_FunctionTest_False()
    {
      Stylist test_1 = new Stylist("test", 123456789, 1);
      Stylist test_2 = new Stylist("this", 987654321, 2);

      Assert.AreNotEqual(test_1, test_2);
    }
    [TestMethod]
    public void Save_GetAll_Test_One()
    {
      Stylist test_1 = new Stylist("test", 123456789, 1);
      test_1.Save();

      List<Stylist> getAllTest = Stylist.GetAll();
      Stylist test_2 = getAllTest[0];
      Assert.AreEqual(test_1, test_2);
    }
    [TestMethod]
    public void Save_GetAll_Test_Multiple()
    {
      List<Stylist> testList = new List<Stylist>();
      Stylist test_1 = new Stylist("test", 123456789, 1);
      Stylist test_2 = new Stylist("this", 987654321, 2);
      test_1.Save();
      test_2.Save();
      testList.Add(test_1);
      testList.Add(test_2);
      List<Stylist> getAllTest = Stylist.GetAll();
      CollectionAssert.AreEqual(testList, getAllTest);
    }
    [TestMethod]
    public void GetAllClients_Test_One()
    {
      Client testClient_1 = new Client("test", 123456789, 1, 1);
      testClient_1.Save();
      Stylist testStylist_1 = new Stylist("test", 123456789, 1);
      testStylist_1.Save();
      List<Client> testList = new List<Client>{testClient_1};
      List<Client> getAllList = testStylist_1.GetAllClients();
      CollectionAssert.AreEqual(testList, getAllList);
    }
    [TestMethod]
    public void GetAllClients_Test_Multiple()
    {
      Client.ClearAll();
      Client testClient_1 = new Client("test", 123456789, 1, 1);
      Client testClient_2 = new Client("test", 123456789, 1, 2);
      testClient_1.Save();
      testClient_2.Save();
      Stylist testStylist_1 = new Stylist("test", 123456789, 1);
      testStylist_1.Save();
      List<Client> testList = new List<Client>{testClient_1, testClient_2};
      List<Client> getAllList = testStylist_1.GetAllClients();
      CollectionAssert.AreEqual(testList, getAllList);
    }
  }
}
