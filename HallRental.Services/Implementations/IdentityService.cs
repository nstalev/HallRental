﻿
namespace HallRental.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Models;

    public class IdentityService : IIdentityService
    {

        private readonly HallRentalDbContext db;

        public IdentityService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserModel> AllUsers()
        {
            return this.db.Users
                .Select(u => new UserModel
                {
                    Email = u.Email,
                    UserName = u.UserName
                }).ToList();
                
        }
    }
}