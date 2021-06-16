using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        //   ------------------------------------ Added by CD  ---------------------------------------------------------------------                 
        /// <summary>
        /// Method to return all Rows from the Blog table
        /// </summary>
        /// <returns></returns>
        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id,
                                               Title,
                                               URL
                                          FROM Blog";
                    List<Blog> retValue = new List<Blog>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("URL")),
                        };
                        retValue.Add(blog);
                    }

                    reader.Close();
                    return retValue;

                }
            }

        }
        //   ------------------------------------ Added by CD  ---------------------------------------------------------------------     
        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Blog blog)
        {
            throw new NotImplementedException();
        }

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
