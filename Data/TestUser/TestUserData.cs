using AdaptiveWebInterfaces_WebAPI.Models.User;
using System;
using System.Collections.Generic;

namespace AdaptiveWebInterfaces_WebAPI.Data.User
{
    public class TestUserData
    {
        public static List<UserModel> Users { get; } = new List<UserModel>
        {
            new UserModel
            {
                UserId = 1,
                LastName = "Doe",
                FirstName = "John",
                Email = "john@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Password = "password1",
                Address = "123 Main St",
                PhoneNumber = "555-1234",
                LastLoginDate = DateTime.Now.AddDays(-2),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 2,
                LastName = "Smith",
                FirstName = "Jane",
                Email = "jane@example.com",
                DateOfBirth = new DateTime(1995, 5, 10),
                Password = "password2",
                Address = "456 Oak St",
                PhoneNumber = "555-5678",
                LastLoginDate = DateTime.Now.AddDays(-1),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 3,
                LastName = "Brown",
                FirstName = "Michael",
                Email = "michael@example.com",
                DateOfBirth = new DateTime(1988, 3, 15),
                Password = "password3",
                Address = "789 Elm St",
                PhoneNumber = "555-9876",
                LastLoginDate = DateTime.Now.AddDays(-3),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 4,
                LastName = "Johnson",
                FirstName = "Emily",
                Email = "emily@example.com",
                DateOfBirth = new DateTime(1992, 7, 20),
                Password = "password4",
                Address = "101 Pine St",
                PhoneNumber = "555-4321",
                LastLoginDate = DateTime.Now.AddDays(-4),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 5,
                LastName = "Davis",
                FirstName = "Chris",
                Email = "chris@example.com",
                DateOfBirth = new DateTime(1985, 12, 5),
                Password = "password5",
                Address = "246 Maple St",
                PhoneNumber = "555-8765",
                LastLoginDate = DateTime.Now.AddDays(-5),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 6,
                LastName = "Wilson",
                FirstName = "Sophia",
                Email = "sophia@example.com",
                DateOfBirth = new DateTime(1998, 9, 18),
                Password = "password6",
                Address = "369 Oak St",
                PhoneNumber = "555-2345",
                LastLoginDate = DateTime.Now.AddDays(-6),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 7,
                LastName = "Martinez",
                FirstName = "David",
                Email = "david@example.com",
                DateOfBirth = new DateTime(1983, 6, 30),
                Password = "password7",
                Address = "582 Elm St",
                PhoneNumber = "555-6789",
                LastLoginDate = DateTime.Now.AddDays(-7),
                FailedLoginAttempts = 0
            },
            new UserModel
            {
                UserId = 8,
                LastName = "Taylor",
                FirstName = "Emma",
                Email = "emma@example.com",
                DateOfBirth = new DateTime(1993, 4, 25),
                Password = "password8",
                Address = "753 Pine St",
                PhoneNumber = "555-3210",
                LastLoginDate = DateTime.Now.AddDays(-8),
                FailedLoginAttempts = 0
            },
        };
    }
}
