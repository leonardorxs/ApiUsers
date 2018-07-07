using System.Collections.Generic;
using ApiUsers.Models;

namespace ApiUsers.Repository
{
    public interface IUserRepository
    {
        void Add(User user);

        IEnumerable<User> GetAll();

        User Find(long id);

        void Remove(long id);

        void Update(User user);
    }
}