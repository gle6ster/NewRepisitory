using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.DAL.Repositories
{
    /// <summary>
    /// Интерфейс, который отвечает за объявление CRUD методов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(); //получение всех объектов
        T Get(int id); //получение одного объекта по id
        void Create(T item); //создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
    }
}
