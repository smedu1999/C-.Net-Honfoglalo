using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Honfoglalo.BLL.Interfaces;
using AutoMapper;
using Honfoglalo.BLL.DTOs;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using Honfoglalo.BLL.Service;
using Honfoglalo.DAL;

namespace Honfoglalo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HonfoglaloController : ControllerBase
    {
        public readonly IGameService service;                                   
        private readonly IMapper mapper;
        public HonfoglaloController(IGameService s, IMapper map)
        {
            service = s;
            mapper = map;
        }


        // GET: api/Honfoglalo/Init                                                        //Pelda!
        [HttpGet("Init", Name = "InitGame")]                                        //[Http<Uzenettipus>("<Eleresi utvonal>", Name="<Nev>")]
        public String Get()                                                     //public <Visszateres> <Uzenettipus>(<param>)
        {            
            return service.InitGame();                                            //      ...mapping...service.<fv>...                                                                                                  
        }                                                                           

        // GET: api/Honfoglalo/EndGame
        [HttpGet("EndGame", Name = "End")]
        public String Endgame()
        {
            return service.EndGame();
        }

        // GET: api/Honfoglalo/Question/5
        [HttpGet("Question/{id}", Name = "GetQuestion")]
        public ActionResult<Question> Get(int id)
        {
            return mapper.Map<Question>(service.GetQuestion(id));
        }

        // POST: api/Honfoglalo/Attack
        [HttpPost("Attack", Name = "AttackPost")]
        public ActionResult<Question> Post([FromBody] AttackInfo ai)
        {
            var result= mapper.Map<Question>(service.AttackField(ai.Attacker, ai.Field));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // POST: api/Honfoglalo/SendAnswer
        [HttpPost("SendAnswer", Name = "SendAnswerPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<String> Post([FromBody]AnswersInfo ai)
        {
            var result = service.GetAnswers(ai);
            return CreatedAtAction(nameof(Post), new { id = ai.qId }, result);
        }

        // POST: api/Honfoglalo/AddQuestion
        [HttpPost("AddQuestion", Name = "AddQuestionPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Question> Post([FromBody] Question q)               
        {
            Question result= mapper.Map<Question>(service.AddNewQuestion(mapper.Map <DAL.Model.Question>(q)));
            return CreatedAtAction(nameof(Get),new { id= result.Id},result);
        }

        [HttpDelete("DeleteQuestion", Name = "DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Question> Delete([FromBody] int id)
        {
            Question result = mapper.Map<Question>(service.DeleteQuestion(id));
            return CreatedAtAction(nameof(Delete), new { id = result.Id }, result);
        }

        // POST: api/Honfoglalo/UserReg
        [HttpPost("UserReg", Name = "UserRegPost")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserTable> Post([FromBody] UserTable user)              
        {
             var result = service.GetUsers(mapper.Map<DAL.Model.Users>(user));
            return CreatedAtAction(nameof(Get), new { id = result.Id }, mapper.Map<UserTable>(result));
        }
    }
}
