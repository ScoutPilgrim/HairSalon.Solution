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
    public int GetStylist()
    {
      return _stylistId;
    }
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
         Client newItem = (Client) otherClient;
         bool idEquality = this.GetId() == newItem.GetId();
         bool nameEquality = this.GetName() == newItem.GetName();
         bool phoneEquality = this.GetPhone() == newItem.GetPhone();
         bool clientIdEquality = this.GetStylist() == newItem.GetStylist();
         return (idEquality && nameEquality && phoneEquality && clientIdEquality);
       }
    }
  }
}
