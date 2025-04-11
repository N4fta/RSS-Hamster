using Data.DTOs;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Tests
{
    [TestClass]
    public class ConvertionMethodTests
    {
        // Done in Loading Methods

        //// User to UserDTO
        //[TestMethod]
        //public void ConvertToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}
        //[TestMethod]
        //public void ConvertToUserDTO_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUserDTo_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}

        //// Feed to FeedDTO
        //[TestMethod]
        //public void ConvertToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}

        //[TestMethod]
        //public void ConvertToUserDTO_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUserDTo_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}

        //// Review to ReviewDTO
        //[TestMethod]
        //public void ConvertToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUser_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}

        //[TestMethod]
        //public void ConvertToUserDTO_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO = new UserDTO();
        //    userDTO.Id = 1;
        //    userDTO.Name = "John Doe";
        //    userDTO.Email = "john@doe.com";
        //    userDTO.Username = "jonnhyDoe";
        //    userDTO.HashedPassword = "password";
        //    userDTO.Notes = "";
        //    userDTO.Role = "Reader";
        //    userRepo.Add(1, userDTO);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetUser("john@doe.com");

        //    // Assert
        //    Assert.IsTrue(result is User);
        //    Assert.IsTrue(result.Id == userDTO.Id && result.Email == userDTO.Email);
        //}
        //[TestMethod]
        //public void ConvertListToUserDTo_Test()
        //{
        //    // Repo
        //    var userRepo = new Dictionary<int, UserDTO>();
        //    var userDTO1 = new UserDTO();
        //    userDTO1.Id = 1;
        //    userDTO1.Name = "John Doe";
        //    userDTO1.Email = "john@doe.com";
        //    userDTO1.Username = "jonnhyDoe";
        //    userDTO1.HashedPassword = "password";
        //    userDTO1.Notes = "";
        //    userDTO1.Role = "Reader";
        //    userRepo.Add(1, userDTO1);
        //    var userDTO2 = new UserDTO();
        //    userDTO2.Id = 2;
        //    userDTO2.Name = "Jane Doe";
        //    userDTO2.Email = "jane@doe.com";
        //    userDTO2.HashedPassword = "password";
        //    userDTO2.Notes = "";
        //    userDTO2.Username = "jannyDoe";
        //    userDTO2.Role = "Reader";
        //    userRepo.Add(2, userDTO2);
        //    var mockDbUsers = new MockDbUsers(userRepo, new(), new(), new(), new(), new(), new());

        //    // Others
        //    var userService = new UserService(mockDbUsers);

        //    // Act
        //    var result = userService.GetAll();

        //    // Assert
        //    Assert.IsTrue(result.Count == 2);
        //}
    }
}
