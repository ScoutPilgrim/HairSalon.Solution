using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client{
    private int _id;
    private string _name;
    private int _phone;
    private int _stylistId;

    public Client(int id, string name, int phone, int stylistId)
    {
      _id = id;
      _name = name;
      _phone = phone;
      _stylistId = stylistId;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public int GetPhone()
    {
      return _phone;
    }
    public GetStylist()
    {
      return _stylistId;
    }
  }
}
