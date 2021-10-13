using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeyVault.Models;
using KeyVault.Repository;

namespace KeyVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaultController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public VaultController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Vault
        [HttpGet]
        public ActionResult<IList<Vault>> GetVaultSet()
        {
            return _context.VaultSet.ToList();
        }

        // GET: api/Vault/5
        [HttpGet("{key}")]
        public ActionResult<Vault> GetVault(string key)
        {
            var vault = GetValueByKey(key);

            if (vault == null)
            {
                return NotFound();
            }

            return vault;
        }

        // PUT: api/Vault/5
        [HttpPut("{key}")]
        public IActionResult PutVault(string key, Vault vault)
        {
            var value = GetValueByKey(key);
            if (value == null)
            {
                return BadRequest();
            }

            value.Value = vault.Value;
            value.Type = vault.Type;
            value.Tags = vault.Tags;

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Vault
        [HttpPost]
        public ActionResult<Vault> PostVault(Vault vault)
        {
            if (VaultExists(vault.Key))
            {
                return Conflict(vault.Key + " already exists!");
            }
            else
            {
                _context.VaultSet.Add(vault);
                _context.SaveChanges();

                return Created("GetVault", vault);
            }
            
        }

        // DELETE: api/Vault/5
        [HttpDelete("{key}")]
        public ActionResult<Vault> DeleteVault(string key)
        {
            var vault = GetValueByKey(key);
            if (vault == null)
            {
                return NotFound();
            }

            _context.VaultSet.Remove(vault);
            _context.SaveChanges();

            return vault;
        }

        private bool VaultExists(string key)
        {
            return _context.VaultSet.Any(e => e.Key == key);
        }

        private Vault GetValueByKey(string key)
        {
            return _context.VaultSet.FirstOrDefault(x => x.Key == key);
        }
    }
}
