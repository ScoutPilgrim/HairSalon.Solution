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
         bool stylistIdEquality = this.GetStylist() == newItem.GetStylist();
         return (idEquality && nameEquality && phoneEquality && stylistIdEquality);
       }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (id, name, phone, stylist_id) VALUES (@id, @name, @phone, @stylistId);";
      MySqlParameter id = new MySqlParameter("@id", this._id);
      MySqlParameter name = new MySqlParameter("@name", this._name);
      MySqlParameter phone = new MySqlParameter("@phone", this._phone);
      MySqlParameter stylist_id = new MySqlParameter("@stylistId", this._stylistId);
      cmd.Parameters.Add(id);
      cmd.Parameters.Add(name);
      cmd.Parameters.Add(phone);
      cmd.Parameters.Add(stylist_id);
      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        string readName = rdr.GetString(1);
        int readPhone = rdr.GetInt32(2);
        int readStylistId = rdr.GetInt32(3);
        Client readClient = new Client(readId, readName, readPhone, readStylistId);
        allClients.Add(readClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
  }
}
