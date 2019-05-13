using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using MySql.Data.MySqlClient;

namespace HairSalon.TestTools
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.ClearAll();
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ian_christner_test;";
    }

    [TestMethod]
    public void Equals_FunctionTest_True()
    {
      Client test_1 = new Client(1, "test", 123456789, 1);
      Client test_2 = new Client(1, "test", 123456789, 1);

      Assert.AreEqual(test_1, test_2);
    }
    [TestMethod]
    public void Equals_FunctionTest_False()
    {
      Client test_1 = new Client(1, "test", 123456789, 1);
      Client test_2 = new Client(2, "this", 987654321, 2);

      Assert.AreNotEqual(test_1, test_2);
    }
    [TestMethod]
    public void Save_GetAll_Test_One()
    {
      Client test_1 = new Client(1, "test", 123456789, 2);
      test_1.Save();

      List<Client> getAllTest = Client.GetAll();
      Client test_2 = getAllTest[0];
      Assert.AreEqual(test_1, test_2);
    }
    [TestMethod]
    public void Save_GetAll_Test_Multiple()
    {
      List<Client> testList = new List<Client>();
      Client test_1 = new Client(1, "test", 123456789, 1);
      Client test_2 = new Client(2, "this", 987654321, 2);
      test_1.Save();
      test_2.Save();
      testList.Add(test_1);
      testList.Add(test_2);
      List<Client> getAllTest = Client.GetAll();
      CollectionAssert.AreEqual(testList, getAllTest);
    }
  }
}
