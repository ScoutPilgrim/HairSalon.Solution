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
      DB.configuration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=ian_christner_test;";
    }

    [TestMethod]
    public void Equals_FunctionTest_True()
    {
      Stylist test_1 = new Stylist();
    }
  }
}
