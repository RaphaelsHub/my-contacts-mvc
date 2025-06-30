using Xunit;
using MyContact.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyContact.Mvc.Test;

public class ContactControllerTests
{
    private ContactController CreateController()
    {
        var logger = new LoggerFactory().CreateLogger<ContactController>();
        return new ContactController(logger);
    }

    [Fact]
    public void Index_Returns_View_With_Contacts()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.Index() as ViewResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<MyContact.Mvc.Models.Contact>>(result.Model);
    }

    [Fact]
    public void Add_Adds_Contact_And_Redirects()
    {
        // Arrange
        var controller = CreateController();
        var name = "Test";
        var phone = "123";
        var email = "test@mail.com";

        // Act
        var result = controller.Add(name, phone, email);

        // Assert
        var index = controller.Index() as ViewResult;
        var contacts = index.Model as List<MyContact.Mvc.Models.Contact>;

        Assert.Contains(contacts, c => c.Name == name && c.Phone == phone && c.Email == email);
        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public void Delete_Removes_Contact()
    {
        // Arrange
        var controller = CreateController();
        var resultAdd = controller.Add("Delete Me", "111", "x@y.com");

        var contactsBefore = (controller.Index() as ViewResult).Model as List<MyContact.Mvc.Models.Contact>;
        var toDelete = contactsBefore.Last();

        // Act
        controller.Delete(toDelete.Id);

        var contactsAfter = (controller.Index() as ViewResult).Model as List<MyContact.Mvc.Models.Contact>;

        // Assert
        Assert.DoesNotContain(contactsAfter, c => c.Id == toDelete.Id);
    }
}