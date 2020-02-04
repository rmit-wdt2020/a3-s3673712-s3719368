using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Models;
using BankAPI.Models.DataManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly LoginManager _repo;

        public LoginsController(LoginManager repo)
        {
            _repo = repo;
        }

        // GET: api/movies
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return _repo.GetAll();
        }

        // GET api/movies/1
        [HttpGet("{id}")]
        public Login Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] Login login)
        {
            _repo.Add(login);
        }

        // PUT api/movies
        [HttpPut]
        public void Put([FromBody] Login login)
        {
            _repo.Update(login.CustomerID, login);
        }

        // DELETE api/movies/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}