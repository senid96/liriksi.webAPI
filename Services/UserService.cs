using AutoMapper;
using liriksi.Model;
using liriksi.Model.Requests;
using liriksi.WebAPI.EF;
using liriksi.WebAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using liriksi.Model.Requests.user;

namespace liriksi.WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly LiriksiContext _context;
        private readonly IMapper _mapper;
        private User _currentUser;
        public UserService(LiriksiContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
       
        public List<UserGetRequest> Get(UserSearchRequest obj)
        {
            var query = _context.User.Include(x=>x.UserType).AsQueryable();

            if (!string.IsNullOrEmpty(obj.Name))
                query = query.Where(x => x.Name.Contains(obj.Name));
            if (!string.IsNullOrEmpty(obj.Surname))
                query = query.Where(x => x.Surname.Contains(obj.Surname));

            var result = query.ToList();
            return _mapper.Map<List<UserGetRequest>>(result);
        }
        [HttpGet("{id}")]

        public UserGetRequest Get(int id)
        {
            User usr = _context.User.Where(x => x.Id == id)
                                    .Include(b => b.City).Include(b => b.UserType).FirstOrDefault();
            return _mapper.Map<UserGetRequest>(usr);
        }

        public UserGetRequest Insert(UserInsertRequest obj)
        {
            if(!obj.Password.Equals(obj.PasswordConfirmation))
            {
                //todo
                //vjv custom exceptione ce trebat napravit
            }

            var entity = _mapper.Map<User>(obj);
            entity.Salt = GenerateSalt();
            entity.Hash = GenerateHash(entity.Salt, obj.Password);  

            _context.User.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<UserGetRequest>(_context.User.Last());
        }

        public UserGetRequest Update(int id, UserUpdateRequest obj)
        {
            var entity = _context.User.Find(id);
            if (obj != null)
            {
                entity.Username = obj.Username;
                entity.Name = obj.Name;
                entity.PhoneNumber = obj.PhoneNumber;
                entity.Surname = obj.Surname;
                entity.Email = obj.Email;
                entity.Image = obj.Image;
                entity.Status = obj.Status;
                _context.SaveChanges();
                return _mapper.Map<UserGetRequest>(entity);
            }
            return null;
        }

        public bool ChangeUserStatus(int id, bool status) //1 active, 0 nonactive
        {
            var usr = _context.User.Find(id);
            if (usr != null)
            {
                _context.User.Attach(usr);
                _context.Update(usr);
                usr.Status = status;

                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<UserType> GetUserTypes()
        {
            return _context.UserType.ToList();
        }

        public User Authenticate(string username, string pass)
        {
            var user = _context.User
                         .Include(x => x.UserType)
                         .Include(x=> x.City)
                         .FirstOrDefault(x => x.Username == username && x.Status.Equals(true));


            if (user != null)
            {
                var newHash = GenerateHash(user.Salt, pass);

                if (newHash == user.Hash)
                {
                    _context.SaveChanges();
                    return _mapper.Map<User>(user);
                }
            }
            return null;
        }

        public void SetCurrentUser(User currentUser)
        {
            _currentUser = currentUser;
        }

        //hash method helpers
        public static string GenerateSalt()
        {
            var buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        public UserGetRequest GetMyProfile()
        {
            if(_currentUser == null)
            {
                return null;
            }

            return _mapper.Map<UserGetRequest>(_context.User.Find(_currentUser.Id));
        }
    }
}
