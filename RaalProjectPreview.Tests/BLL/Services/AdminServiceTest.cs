using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaalProjectPreview.BLL.Services;
using System.Collections.Generic;
using System.Linq;
using Effort;
using RaalProjectPreview.DAL.Models.DBModels;
using RaalProjectPreview.BLL.Models.Admin;
using RaalProjectPreview.BLL.Models.Enums;

namespace RaalProjectPreview.Tests.BLL.Services
{
    [TestClass]
    public class AdminServiceTest
    {
        [TestMethod]
        public void ShowUsers_Test()
        {
            var connection = DbConnectionFactory.CreateTransient();
            using (var context = new DatabaseTestContext(connection))
            {
                AdminService service = new AdminService(context);
                List<AllUserData> userDataList = new List<AllUserData>();
                
                for (var i = 1; i < 11; i++)
                {
                    userDataList.Add(new AllUserData() { 
                         Customer = new Customer() { 
                             Address = "addr" + i,
                             Code = "XXXXXXX", 
                             Discount = null, 
                             Id = i, 
                             Name = "Test" + i
                         },
                         AuthUserData = new AuthUserData() { 
                             CustomerId = i,
                             Id = i + 10, 
                             Login = "Login" + i, 
                             PasswordHash = "Pass" + i
                         },
                         UserRole = new UserRole() {
                             ClientRole = Security.Roles.ClientRole.Customer,
                             Id = i + 20, 
                             CustomerId = i  
                         }
                    });
                }
                context.Customers.AddRange(userDataList.Select(x => x.Customer));
                context.UserRoles.AddRange(userDataList.Select(x => x.UserRole));
                context.AuthUserDatas.AddRange(userDataList.Select(x => x.AuthUserData));
                context.SaveChanges();

                // Actual result
                List<AllUserData> expectedList = service.ShowUsers();
                int ActualUserCount = expectedList.Count;

                // Expected result
                int ExpectedUserCount = 10;

                Assert.AreEqual(ExpectedUserCount, ActualUserCount);
            }
        }
        [TestMethod]
        public void AddNewItem_BadCode_Test()
        {
            var connection = DbConnectionFactory.CreateTransient();
            using (var context = new DatabaseTestContext(connection))
            {
                AdminService service = new AdminService(context);
                service.AddNewItem(new Item() { 
                     Category = "TTTTT",
                     Code = "F#@WF@#f23FF#@F",
                     Id = 1, Name = "ewfwefwe",
                     Price = 101000
                });
                service.AddNewItem(new Item()
                {
                    Category = "TTTTT",
                    Code = "",
                    Id = 2,
                    Name = "vfrevrr",
                    Price = 343
                });
                service.AddNewItem(new Item()
                {
                    Category = "TTTTT",
                    Code = "",
                    Id = 2,
                    Name = "!!!!!!!!!!!!!!!!!!!!!!!!!!!!",
                    Price = 343
                });
                context.SaveChanges();

                List<Item> item = context.Items.ToList().Where(x => (x.Code[2] != '-' || 
                                                                     x.Code[7] != '-' ||
                                                                     x.Code.Contains("!") ||
                                                                     x.Code.Contains("#"))).ToList();
                // Actual result
                int ActualUserCount = item.Count;
                
                // Expected result
                int ExpectedUserCount = 0;

                Assert.AreEqual(ExpectedUserCount, ActualUserCount);
            }
        }
        [TestMethod]
        public void DeleteUser_NoUser_Test()
        {
            var connection = DbConnectionFactory.CreateTransient();
            using (var context = new DatabaseTestContext(connection))
            {
                AdminService service = new AdminService(context);

                context.Customers.Add(new Customer()
                {
                    Address = "324",
                    Code = "2313",
                    Id = 1,
                    Name = "2323"
                });
                context.SaveChanges();
                // Actual result
                ServiceResponseStatus ActualUserResponse = service.DeleteUser(31);

                // Expected result
                ServiceResponseStatus ExpectedResponse = ServiceResponseStatus.Failure;

                Assert.AreEqual(ExpectedResponse, ActualUserResponse);
            }
        }

    }
}
