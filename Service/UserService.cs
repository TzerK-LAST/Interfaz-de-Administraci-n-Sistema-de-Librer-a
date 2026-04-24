using InterfazdeAdministración_SistemadeLibrería.Data;
using InterfazdeAdministración_SistemadeLibrería.Models;
using InterfazdeAdministración_SistemadeLibrería.Response;
using Microsoft.EntityFrameworkCore;

namespace InterfazdeAdministración_SistemadeLibrería.Service;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public ServiceResponse<IEnumerable<User>> GetAllUser()
    {
        var users = _context.Users.ToList();
        return new ServiceResponse<IEnumerable<User>>()
        {
            Success = true,
            Data = users
        };
    }

    public ServiceResponse<User> CreateUser(User user)
        {
            var users = new ServiceResponse<User>();
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                users.Success = true;
                users.Data = user;
                users.Message = "User created";
            }
            catch (Exception e)
            {
                users.Success = false;
                users.Message = "Error: " + e.Message;
                
            } 
            return users;
        }
    }
