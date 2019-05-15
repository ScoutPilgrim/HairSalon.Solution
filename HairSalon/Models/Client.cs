using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
  public class Client{
    private int _id;
    private string _name;
    private int _phone;
    private int _stylistId;

    public Client(string name, int phone, int stylistId, int id = 0)
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
    public static void ClearAllAtStylist(int stylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @stylistId;";
      MySqlParameter myId = new MySqlParameter("@stylistId", stylistId);
      cmd.Parameters.Add(myId);
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
        Client readClient = new Client(readName, readPhone, readStylistId, readId);
        allClients.Add(readClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    public static Client Find(int myId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @id;";
      MySqlParameter id = new MySqlParameter("@id", myId);
      cmd.Parameters.Add(id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int readClientId = 0;
      string readClientName = "";
      int readClientPhone = 0;
      int readClientStylistId = 0;
      while(rdr.Read())
      {
        readClientId = rdr.GetInt32(0);
        readClientName = rdr.GetString(1);
        readClientPhone = rdr.GetInt32(2);
        readClientStylistId = rdr.GetInt32(3);
      }
      Client readClient = new Client(readClientName, readClientPhone, readClientStylistId, readClientId);
      return readClient;
    }
    public void Edit(string newName, int newPhone)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @name WHERE id = @id; UPDATE clients SET phone = @phone WHERE id = @id";
      MySqlParameter myName = new MySqlParameter("@name", newName);
      MySqlParameter myPhone = new MySqlParameter("@phone", newPhone);
      MySqlParameter myId = new MySqlParameter("@id", this.GetId());
      cmd.Parameters.Add(myName);
      cmd.Parameters.Add(myPhone);
      cmd.Parameters.Add(myId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @id;";
      MySqlParameter myId = new MySqlParameter("@id", this.GetId());
      cmd.Parameters.Add(myId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}
