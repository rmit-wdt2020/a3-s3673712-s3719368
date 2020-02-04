using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Models;
using BankAPI.Models.DataManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountManager _repo;

        public AccountsController(AccountManager repo)
        {
            _repo = repo;
        }

        // GET: api/movies
        [HttpGet]
        public IEnumerable<Accounts> Get()
        {
            return _repo.GetAll();
        }

        // GET api/movies/1
        [HttpGet("{id}")]
        public Accounts Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/movies
        [HttpPost]
        public void Post([FromBody] Accounts account)
        {
            _repo.Add(account);
        }

        // PUT api/movies
        [HttpPut]
        public void Put([FromBody] Accounts account)
        {
            _repo.Update(account.CustomerId, account);
        }

        // DELETE api/movies/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}