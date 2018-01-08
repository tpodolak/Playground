using System.Collections.Generic;
using DotRezCore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotRezCore.Api.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Person>), 200)]
        public IActionResult Get()
        {
            var people = new[]
            {
                new Person
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@somewhere.com"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Bob",
                    LastName = "Smith",
                    Email = "bob.smith@somewhere.com"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Jane",
                    LastName = "Doe",
                    Email = "jane.doe@somewhere.com"
                }
            };

            return Ok(people);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public IActionResult Get(int id) =>
            Ok(new Person
                {
                    Id = id,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@somewhere.com"
                }
            );
    }
}