using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCoreWebApi.Model;
using DotNetCoreWebApi.Models;

namespace DotNetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesMainsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SalesMainsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/SalesMains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesMain>>> GetSalesMains()
        {
            return await _context.SalesMains.ToListAsync();
        }

        // GET: api/SalesMains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesMain>> GetSalesMain(int id)
        {
          var saleMain=(from a in _context.SalesMains
                       where a.SalesMainID==id
                       select new
                       {
                           a.SalesMainID,
                           SalesDate=a.SalesDate.ToString("yyyy-MM-dd "),
                           DeletedOrderItemIDs=new List<int>(),
                           a.TotalAmount
                       }).FirstOrDefault();

            var SalesSubList = (from a in _context.SalesSubs
                                join b in _context.items on a.ItemID equals b.ItemID
                                where a.SalesMainID == id
                                select new
                                {
                                    a.SalesMainID,
                                    a.SalesSubID,
                                    a.ItemID,
                                    a.ItemQuantity,
                                    a.TotalPrice,
                                    ItemName = b.ItemName,
                                    Price = b.ItemRate,

                                }).ToList();
            return Ok(new { saleMain, SalesSubList });
        }

        // PUT: api/SalesMains/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalesMain(int id, SalesMain salesMain)
        {
            if (id != salesMain.SalesMainID)
            {
                return BadRequest();
            }

            _context.Entry(salesMain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesMainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SalesMains
        [HttpPost]
        public async Task<ActionResult<SalesMain>> PostSalesMain(SalesMain salesMain)
        {
            try
            {   //Sales Main Table
                if(salesMain.SalesMainID==0)
                _context.SalesMains.Add(salesMain);
                else
                {
                    _context.Entry(salesMain).State = EntityState.Modified;

                    //Sales Subs Table

                    foreach (var item in salesMain.SalesItems)
                    {
                        if (item.SalesSubID == 0)
                            _context.SalesSubs.Add(item);
                        else
                            _context.Entry(item).State = EntityState.Modified;

                    }

                }




                //Delete SalesSub TableItems
                if (salesMain.DeletedOrderItemIDs != null)
                    foreach (var id in salesMain.DeletedOrderItemIDs)
                    {
                        SalesSub sales = _context.SalesSubs.FirstOrDefault(x => x.SalesSubID == id);
                        _context.SalesSubs.Remove(sales);
                    }
            
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           

            //return CreatedAtAction("GetSalesMain", new { id = salesMain.SalesMainID }, salesMain);
        }

        // DELETE: api/SalesMains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalesMain>> DeleteSalesMain(int id)
        {
            var salesMain = _context.SalesMains.SingleOrDefault(x=>x.SalesMainID==id);

            _context.SalesMains.Remove(salesMain);

            var salesSUbList = _context.SalesSubs.Where(x=>x.SalesMainID==id);
            foreach(var item in salesSUbList)
            _context.SalesSubs.Remove(item);
            await _context.SaveChangesAsync();

            return salesMain;
        }

        private bool SalesMainExists(int id)
        {
            return _context.SalesMains.Any(e => e.SalesMainID == id);
        }
    }
}
