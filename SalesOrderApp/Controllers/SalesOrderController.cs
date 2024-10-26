using Microsoft.AspNetCore.Mvc;
using SalesOrderDALApp.Data;
using SalesOrderDALApp.Models;

[Route("[controller]")]
public class SalesOrderController : Controller
{
    private readonly SalesOrderDAL _salesOrderDal;

    public SalesOrderController(IConfiguration configuration)
    {
        _salesOrderDal = new SalesOrderDAL(configuration.GetConnectionString("DefaultConnection"));
    }

    // =============================
    // API Methods
    // =============================

    // API: GET: /SalesOrder/api - Get all Sales Orders as JSON
    [HttpGet("api")]
    public IActionResult GetAllSalesOrders()
    {
        var salesOrders = _salesOrderDal.GetSalesOrders();
        return Ok(salesOrders);
    }

    // API: GET: /SalesOrder/api/{id} - Get a Sales Order by ID as JSON
    [HttpGet("api/{id}")]
    public IActionResult GetSalesOrderById(string id)
    {
        var salesOrder = _salesOrderDal.GetSalesOrders().FirstOrDefault(o => o.SalesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }
        return Ok(salesOrder);
    }

    // API: POST: /SalesOrder/api - Create a new Sales Order
    [HttpPost("api")]
    public IActionResult CreateSalesOrder([FromBody] SalesOrder salesOrder)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        salesOrder.Timestamp = DateTime.Now;
        _salesOrderDal.AddSalesOrder(salesOrder);
        return CreatedAtAction(nameof(GetSalesOrderById), new { id = salesOrder.SalesOrderId }, salesOrder);
    }

    // API: PUT: /SalesOrder/api/{id} - Update an existing Sales Order
    [HttpPut("api/{id}")]
    public IActionResult UpdateSalesOrder(string id, [FromBody] SalesOrder salesOrder)
    {
        if (id != salesOrder.SalesOrderId)
        {
            return BadRequest("SalesOrder ID mismatch.");
        }

        if (!ModelState.IsValid) return BadRequest(ModelState);

        salesOrder.Timestamp = DateTime.Now;
        _salesOrderDal.UpdateSalesOrder(salesOrder);
        return NoContent();
    }

    // API: DELETE: /SalesOrder/api/{id} - Delete a Sales Order by ID
    [HttpDelete("api/{id}")]
    public IActionResult DeleteSalesOrder(string id)
    {
        // Tìm đơn hàng để xóa
        var salesOrder = _salesOrderDal.GetSalesOrders().FirstOrDefault(o => o.SalesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound(); // Nếu không tìm thấy đơn hàng
        }

        _salesOrderDal.DeleteSalesOrder(id); // Gọi hàm xóa đơn hàng
        return NoContent(); // Trả về 204 No Content
    }

    // =============================
    // View Rendering Methods
    // =============================

    // GET: /SalesOrder - Render View
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // GET: /SalesOrder/Create - Render Create View
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // GET: /SalesOrder/Edit/{id} - Render Edit View
    [HttpGet("Edit/{id}")]
    public IActionResult Edit(string id)
    {
        var salesOrder = _salesOrderDal.GetSalesOrders().FirstOrDefault(o => o.SalesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }
        return View(salesOrder);
    }

    // GET: /SalesOrder/Delete/{id} - Render Delete View
    [HttpGet("Delete/{id}")]
    public IActionResult Delete(string id)
    {
        var salesOrder = _salesOrderDal.GetSalesOrders().FirstOrDefault(o => o.SalesOrderId == id);
        if (salesOrder == null)
        {
            return NotFound();
        }
        return View(salesOrder);
    }

}
