using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Models;
using BankAPI.Models.DataManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DataManager;

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

        // GET: api/logins
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return _repo.GetAll();
        }

        // GET api/logins/1
        [HttpGet("{id}")]
        public Login Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/logins
        [HttpPost]
        public void Post([FromBody] Login login)
        {
            _repo.Add(login);
        }

        // PUT api/logins
        [HttpPut]
        public void Put([FromBody] Login login)
        {
            _repo.Update(Convert.ToInt32(login.LoginID), login);
        }

        // DELETE api/logins/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}