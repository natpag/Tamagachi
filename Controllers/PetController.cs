using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagachi.Models;

namespace Tamagachi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();


    // Show all pets in Database
    [HttpGet("pets")]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(p => p.Name);
      return pets.ToList();
    }

    //Show pet with corresponding ID
    [HttpGet("{id}")]
    public Pet GetOneMenuItem(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      return item;
    }

    //Find pet by ID and Play (+5 to Happiness & +3 to Hunger)
    [HttpPut("{id}/play")]
    public Pet Play(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel += 5;
      item.HungerLevel += 3;
      db.SaveChanges();
      return item;
    }

    //Feed pet
    [HttpPut("{id}/feed")]
    public Pet Feed(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel += 3;
      item.HungerLevel -= 5;
      db.SaveChanges();
      return item;
    }

    //Scold pet
    [HttpPut("{id}/scold")]
    public Pet Scold(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel -= 5;
      db.SaveChanges();
      return item;
    }

    //Create new pet
    [HttpPost]
    public Pet CreateNewPet(Pet item)
    {
      db.Pets.Add(item);
      db.SaveChanges();
      return item;
    }

    //Create multiple pets
    [HttpPost("multiple")]
    public List<Pet> AddManyPets(List<Pet> items)
    {
      db.Pets.AddRange(items);
      db.SaveChanges();
      return items;
    }

    //Delete a pet
    [HttpDelete("{id}")]
    public ActionResult DeleteOne(int id)
    {
      var item = db.Pets.FirstOrDefault(f => f.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      db.Pets.Remove(item);
      db.SaveChanges();
      return Ok();
    }

  }
}