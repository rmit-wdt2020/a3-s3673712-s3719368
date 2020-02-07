using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using a2_s3673712_s3719368.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.DataManager;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayeesController : ControllerBase
    {
        private readonly PayeeManager _repo;

        public PayeesController(PayeeManager repo)
        {
            _repo = repo;
        }

        // GET: api/payees
        [HttpGet]
        public IEnumerable<Payee> Get()
        {
            return _repo.GetAll();
        }

        // GET api/payees/1
        [HttpGet("{id}")]
        public Payee Get(int id)
        {
            return _repo.Get(id);
        }

        // POST api/payees
        [HttpPost]
        public void Post([FromBody] Payee payee)
        {
            _repo.Add(payee);
        }

        // PUT api/payees
        [HttpPut]
        public void Put([FromBody] Payee payee)
        {
            _repo.Update(payee.PayeeID, payee);
        }

        // DELETE api/payees/1
        [HttpDelete("{id}")]
        public long Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
}