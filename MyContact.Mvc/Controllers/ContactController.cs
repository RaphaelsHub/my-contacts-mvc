using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyContact.Mvc.Models;

namespace MyContact.Mvc.Controllers;


public class ContactController : Controller
{
    private readonly ILogger<ContactController> _logger;
    private static List<Contact> contacts = new();

    public ContactController(ILogger<ContactController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index() => View(contacts);


    [HttpPost]
    public IActionResult Add(string name, string phone, string email)
    {
        contacts.Add(new Contact
        {
            Name = name,
            Phone = phone,
            Email = email
        });

        return RedirectToAction("Index");
    }

    [HttpGet("{id}")]
    public IActionResult Delete(int id)
    {
        contacts.RemoveAll(c => c.Id == id);
        return RedirectToAction("Index");
    }
}
