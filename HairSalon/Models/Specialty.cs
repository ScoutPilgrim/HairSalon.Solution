using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Models
{
  public class Specialty{
    private int _id;
    private string _spec;

    public Specialty(string spec, int id = 0)
    {
      _id = id;
      _spec = spec;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetSpec()
    {
      return _spec;
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if (!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
         Specialty newItem = (Specialty) otherSpecialty;
         bool idEquality = this.GetId() == newItem.GetId();
         bool specEquality = this.GetSpec() == newItem.GetSpec();
         return (idEquality && specEquality);
       }
    }
    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties; DELETE FROM stylists_specialties";
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
      cmd.CommandText = @"INSERT INTO specialties (id, spec) VALUES (@id, @spec);";
      MySqlParameter id = new MySqlParameter("@id", this.GetId());
      MySqlParameter spec = new MySqlParameter("@spec", this.GetSpec());
      cmd.Parameters.Add(id);
      cmd.Parameters.Add(spec);
      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Specialty> GetAll()
    {
      List<Specialty> allSpecialties = new List<Specialty>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int readId = rdr.GetInt32(0);
        string readspec = rdr.GetString(1);
        Specialty readSpecialty = new Specialty(readspec, readId);
        allSpecialties.Add(readSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return allSpecialties;
    }
    public List<Stylist> GetAllStylists()
    {
      List<Stylist> foundStylists = new List<Stylist>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists_specialties WHERE specialty_id = @specialty_id;";
      MySqlParameter specialty_id = new MySqlParameter("@specialty_id", this._id);
      cmd.Parameters.Add(specialty_id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int readStylistId = rdr.GetInt32(1);
        Stylist readStylist = Stylist.Find(readStylistId);
        foundStylists.Add(readStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return foundStylists;
    }
    public static Specialty Find(int myId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = @id;";
      MySqlParameter id = new MySqlParameter("@id", myId);
      cmd.Parameters.Add(id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      int readSpecialtyId = 0;
      string readSpecialtyspec = "";

      while(rdr.Read())
      {
        readSpecialtyId = rdr.GetInt32(0);
        readSpecialtyspec = rdr.GetString(1);
      }
      Specialty readSpecialty = new Specialty(readSpecialtyspec, readSpecialtyId);
      return readSpecialty;
    }
    public void Edit(string newspec)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET spec = @spec WHERE id = @id;";
      MySqlParameter myspec = new MySqlParameter("@spec", newspec);
      MySqlParameter myId = new MySqlParameter("@id", this.GetId());
      cmd.Parameters.Add(myspec);
      cmd.Parameters.Add(myId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@Stylist, @Specialty);";
      MySqlParameter stylist = new MySqlParameter("@Stylist", newStylist.GetId());
      MySqlParameter specialty = new MySqlParameter("@Specialty", this._id);
      cmd.Parameters.Add(stylist);
      cmd.Parameters.Add(specialty);
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
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @id; DELETE FROM stylists_specialties WHERE stylist_id = @id;";
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
