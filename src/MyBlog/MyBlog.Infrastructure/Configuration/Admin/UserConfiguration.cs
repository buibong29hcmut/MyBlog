using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using MyBlog.Domain.Entities.Identity;

namespace MyBlog.Infrastructure.Configuration.Admin
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //var hasher = new PasswordHasher<User>();
            //builder.HasData(
            //      new IdentityUser
            //{
            //  Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
            //  UserName = "buibong2912",
            //  NormalizedUserName = "BUIBONG2912",
            //  PasswordHash = hasher.HashPassword(null, "29122002Az@")
            // }
            //);

        }
    }
}
