﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;


namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            throw new NotImplementedException();
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        //Hunter's code to Add  Blog-----------------------------------------
        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, Url) 
                                        VALUES (@title, @url)";

                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Hunter's code to Add  Blog-----------------------------------------

        public void Update(Blog blog)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
