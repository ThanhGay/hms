using HMS.Auth.Domain;
using HMS.Shared.Constant.Permission;
using Microsoft.EntityFrameworkCore;

namespace HMS.Auth.Infrastructures
{
    public static class SeedData
    {
        /// <summary>
        /// Seed data for Role
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedAuthRole(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AuthRole>()
                .HasData(
                    new AuthRole { RoleId = 0, RoleName = "Admin" },
                    new AuthRole { RoleId = 1, RoleName = "Manager" },
                    new AuthRole { RoleId = 2, RoleName = "Receptionist" },
                    new AuthRole { RoleId = 3, RoleName = "Customer" }
                );
        }

        /// <summary>
        /// Seed data for role - permission
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedAuthRolePermission(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AuthRolePermission>()
                .HasData(
                    new AuthRolePermission { RoleId = 2, PermissonKey = PermissionKeys.AddCustomer }
                );
        }

        /// <summary>
        /// Seed data for account login
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedAccount(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<AuthUser>()
                .HasData(
                    new AuthUser { UserId = 0, Email = "admin@gmail.com", Password = "123456", RoleId = 0 },
                    new AuthUser { UserId = 1, Email = "manager@gmail.com", Password = "123456", RoleId = 1 },
                    new AuthUser { UserId = 2, Email = "receptionist@gmail.com", Password = "123456", RoleId = 2 },
                    new AuthUser { UserId = 3, Email = "customer@gmail.com", Password = "123456", RoleId = 3 }
            );

            modelBuilder
                .Entity<AuthReceptionist>()
                .HasData(
                    new AuthReceptionist
                    {
                        ReceptionistId = 2,
                        FirstName = "Phạm",
                        LastName = "Đăng Khoa",
                        PhoneNumber = "0987654321",
                        CitizenIdentity = "001122334455",
                        DateOfBirth = new DateTime(2003, 03, 20),
                    }
                );

            modelBuilder
                .Entity<AuthCustomer>()
                .HasData(
                    new AuthCustomer
                    {
                        CustomerId = 3,
                        FirstName = "Đỗ",
                        LastName = "Duy Khánh",
                        PhoneNumber = "0987123654",
                        CitizenIdentity = "002233445566",
                        DateOfBirth = new DateTime(2003, 04, 25),
                    }
                );
        }
    }
}
