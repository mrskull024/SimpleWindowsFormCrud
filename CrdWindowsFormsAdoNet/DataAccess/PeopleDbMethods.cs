using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CrdWindowsFormsAdoNet.DataAccess
{
    public class PeopleDbMethods : DbFactory
    {
        public People Get(int peopleId)
        {
            var people = new People();

            using (var cn = Create())
            {
                cn.Open();
                using (var command = new SqlCommand("spGetPeopleById", cn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("id", peopleId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            people = new People()
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Age = (int)reader[2]
                            };
                        }
                    }
                }
                return people;
            }
        }

        public List<People> GetAll()
        {
            var peoples = new List<People>();

            using (var cn = Create())
            {
                cn.Open();
                using (var command = new SqlCommand("spGetPeople", cn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            People people = new People()
                            {
                                Id = (int)reader[0],
                                Name = (string)reader[1],
                                Age = (int)reader[2]
                            };

                            peoples.Add(people);
                        }
                    }
                }
                return peoples;
            }
        }

        public bool AddPeople(People people)
        {
            try
            {
                using (var cn = Create())
                {
                    cn.Open();
                    using (var command = new SqlCommand("spAddPeople", cn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("id", people.Id);
                        command.Parameters.AddWithValue("name", people.Name);
                        command.Parameters.AddWithValue("age", people.Age);
                        return command.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditPeople(People people)
        {
            try
            {
                using (var cn = Create())
                {
                    cn.Open();
                    using (var command = new SqlCommand("spEditPeople", cn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("id", people.Id);
                        command.Parameters.AddWithValue("name", people.Name);
                        command.Parameters.AddWithValue("age", people.Age);
                        return command.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePeople(People people)
        {
            try
            {
                using (var cn = Create())
                {
                    cn.Open();
                    using (var command = new SqlCommand("spDeletePeople", cn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("id", people.Id);
                        return command.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
