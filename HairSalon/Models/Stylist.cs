using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
  public class Stylist{
    private int _id;
    private string _name;
    private int _phone;

    public Stylist(string name, int phone, int id = 0)
    {
      _id = id;
      _name = name;
      _phone = phone;
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

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
         Stylist newItem = (Stylist) otherStylist;
         bool idEquality = this.GetId() == newItem.GetId();
         bool nameEquality = this.GetName() == newItem.GetName();
         bool phoneEquality = this.GetPhone() == newItem.GetPhone();
         return (idEquality && nameEquality && phoneEquality);
       }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists; DELETE FROM clients";
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
      cmd.CommandText = @"INSERT INTO stylists (id, name, phone) VALUES (@id, @name, @phone);";
      MySqlParameter id = new MySqlParameter("@id", this._id);
      MySqlParameter name = new MySqlParameter("@name", this._name);
      MySqlParameter phone = new MySqlParameter("@phone", this._phone);
      cmd.Parameters.Add(id);
      cmd.Parameters.Add(name);
      cmd.Parameters.Add(phone);
      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        string readName = rdr.GetString(1);
        int readPhone = rdr.GetInt32(2);
        Stylist readStylist = new Stylist(readName, readPhone, readId);
        allStylists.Add(readStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    public List<Client> GetAllClients()
    {
      List<Client> foundClients = new List<Client>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";
      MySqlParameter stylist_id = new MySqlParameter("@stylist_id", this._id);
      cmd.Parameters.Add(stylist_id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int readClientId = rdr.GetInt32(0);
        string readClientName = rdr.GetString(1);
        int readClientPhone = rdr.GetInt32(2);
        Client readClient = new Client(readClientName, readClientPhone, this._id, readClientId);
        foundClients.Add(readClient);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundClients;
    }
    public List<Specialty> GetAllSpecialties()
    {
      List<Specialty> foundSpecialties = new List<Specialty>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists_specialties WHERE stylist_id = @stylist_id;";
      MySqlParameter stylist_id = new MySqlParameter("@stylist_id", this._id);
      cmd.Parameters.Add(stylist_id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int readSpecialtyId = rdr.GetInt32(2);
        Specialty readSpecialty = Specialty.Find(readSpecialtyId);
        foundSpecialties.Add(readSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundSpecialties;
    }
    public static Stylist Find(int myId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @id;";
      MySqlParameter id = new MySqlParameter("@id", myId);
      cmd.Parameters.Add(id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int readStylistId = 0;
      string readStylistName = "";
      int readStylistPhone = 0;
      while(rdr.Read())
      {
        readStylistId = rdr.GetInt32(0);
        readStylistName = rdr.GetString(1);
        readStylistPhone = rdr.GetInt32(2);
      }
      Stylist readStylist = new Stylist(readStylistName, readStylistPhone, readStylistId);
      return readStylist;
    }
    public void Edit(string newName, int newPhone)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @name WHERE id = @id; UPDATE stylists SET phone = @phone WHERE id = @id";
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
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @id; DELETE FROM clients WHERE stylist_id = @id; DELETE FROM stylists_specialties WHERE stylist_id = @id";
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
