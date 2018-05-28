using Projects.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Projects.DAL.Repositories
{
    public class ProjectRepository : IRepository<Project>
    {
        SqlConnection sqlConnection;
        public ProjectRepository()
        {
            string connectionstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=
            E:\KPI\6 semestr\Project\Projects-Changes\Projects-Changes\Projects.DAL\Projects.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionstring); //открытое подключение к базе данных SQL Server.
        }
        /// <summary>
        /// Этот метод создает объект
        /// </summary>
        /// <param name="item"></param>
        public void Create(Project item)
        {
            sqlConnection.Open(); //Открывает подключение к базе данных со значениями свойств, определяемыми объектом ConnectionString
            string sqlString = "Insert into Project (Id,Name,ManagerId,Description,ProjectStart,ProjectEnd) "
            + "values(@Id,@Name,@ManagerId,@Description,@ProjectStart,@ProjectEnd)";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection); //экземпляр класса SqlCommand текстом запроса и подключением SqlConnection.
            command.Parameters.AddWithValue("@Id", item.Id);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@ManagerId", item.ManagerId);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@ProjectStart", item.ProjectStart);
            command.Parameters.AddWithValue("@ProjectEnd", item.ProjectEnd);
            command.ExecuteNonQuery();
            sqlConnection.Close(); //Закрывает соединение с базой данных.

        }
        /// <summary>
        /// Этот метод отвечает за удаление объекта по id
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            sqlConnection.Open();
            string sqlString = "Delete FROM Project WHERE id=@id";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
        /// <summary>
        /// Метод отвечает за получение одного объекта по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Метод возвращает Project по соответствующему id, который содержит id, имя, id менеджера, дату начала и окончания сроков реализации задачи и её описание</returns>
        public Project Get(int id)
        {
            sqlConnection.Open();
            string sqlString = "Select * from Project WHERE id=@id";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = command.ExecuteReader()) //Предоставляет возможность чтения потока строк только в прямом направлении из базы данных SQL Server
            {
                if (reader.Read()) //Перемещает SqlDataReader к следующей записи
                {
                    var projects = new Project();
                    projects.Id = reader.GetInt32(0);
                    projects.Name = reader.GetString(1);
                    projects.ManagerId = reader.GetInt32(2);
                    projects.ProjectStart = reader.GetDateTime(3);
                    projects.ProjectEnd = reader.GetDateTime(4);
                    projects.Description = reader.GetString(5);
                    sqlConnection.Close();
                    return projects;
                }
                else
                {
                    throw new System.InvalidOperationException("Cannot get project with id=" +id.ToString());
                }
            }
            
        }
        /// <summary>
        /// Метод отвечающий за получение всех объектов 
        /// </summary>
        /// <returns>Возвращает все объекты Project</returns>
        public IEnumerable<Project> GetAll()
        {
            sqlConnection.Open();
            string sqlString = "Select * from Project";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var projects = new Project();
                    projects.Id = reader.GetInt32(0);
                    projects.Name = reader.GetString(1);
                    projects.ManagerId = reader.GetInt32(2);
                    projects.ProjectStart = reader.GetDateTime(3);
                    projects.ProjectEnd = reader.GetDateTime(4);
                    projects.Description = reader.GetString(5);
                    yield return projects;
                }
            }
            sqlConnection.Close();
        }
        /// <summary>
        /// Метод, отвечающий за обновление объекта с соответсвующим id
        /// </summary>
        /// <param name="item"></param>
        public void Update(Project item)
        {
            sqlConnection.Open();
            string sqlString = "UPDATE Project SET Name=@Name,ManagerId=@ManagerId,Description=@Description,ProjectStart=@ProjectStart," +
                "ProjectEnd=@ProjectEnd WHERE id=@id";
            SqlCommand command = new SqlCommand(sqlString, sqlConnection);
            command.Parameters.AddWithValue("@id", item.Id);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@ManagerId",item.ManagerId);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@ProjectStart", item.ProjectStart);
            command.Parameters.AddWithValue("@ProjectEnd", item.ProjectEnd);
            command.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
