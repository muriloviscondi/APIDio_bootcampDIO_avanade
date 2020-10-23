using System;
using APIDio.Data.Collections;
using APIDio.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace APIDio.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class InfectedController : ControllerBase
  {
    Data.MongoDB _mongoDB;
    IMongoCollection<Infected> _infectedsCollection;

    public InfectedController(Data.MongoDB mongoDB)
    {
      _mongoDB = mongoDB;
      _infectedsCollection = _mongoDB.DB.GetCollection<Infected>(typeof(Infected).Name.ToLower());
    }

    [HttpPost]
    public ActionResult CreateInfected([FromBody] InfectedDto dto)
    {
      var infected = new Infected(dto.Birth, dto.Gender, dto.Latitude, dto.Longitude);

      _infectedsCollection.InsertOne(infected);

      return StatusCode(201, "Infectado adicionado com sucesso.");
    }

    [HttpGet]
    public ActionResult GetInfected()
    {
      var infected = _infectedsCollection.Find(Builders<Infected>.Filter.Empty).ToList();

      return Ok(infected);
    }

    [HttpPut]
    public ActionResult EditInfected([FromBody] InfectedDto dto)
    {
      _infectedsCollection.UpdateOne(Builders<Infected>.Filter.Where(_ => _.Birth == dto.Birth), Builders<Infected>.Update.Set("gender", dto.Gender));

      return Ok("Atualizado com sucesso.");
    }

    [HttpDelete("{birth}")]
    public ActionResult DeleteInfected(DateTime birth)
    {
      _infectedsCollection.DeleteOne(Builders<Infected>.Filter.Where(_ => _.Birth == birth));

      return Ok("Deletado com sucesso.");
    }
  }
}