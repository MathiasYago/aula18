using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProdutosController : ControllerBase
{
    // A static list to store produtos (for demonstration purposes).
    private static List<Produto> produtos = new List<Produto>();


    [HttpGet]
    public ActionResult<List<Produto>> GetAll()
    {
        return produtos;
    }


    [HttpGet("{id}")]
    public ActionResult<Produto> GetById(int id)
    {
        var produtoEncontrado = produtos.Find(p => p.Id == id);
        if (produtoEncontrado == null)
        {
            return NotFound();
        }
        return produtoEncontrado;
    }

    [HttpPost]
    public ActionResult Post([FromBody] Produto produto)
    {
        if (produto == null)
        {
            return BadRequest("Produto não pode ser nulo.");
        }

        produto.Id = produtos.Count + 1; // Assign a new ID
        produtos.Add(produto);
        return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Produto updatedProduto)
    {
        var produtoEncontrado = produtos.Find(p => p.Id == id);
        if (produtoEncontrado == null)
        {
            return NotFound();
        }

        produtoEncontrado.Nome = updatedProduto.Nome;
        produtoEncontrado.Preco = updatedProduto.Preco;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var produtoEncontrado = produtos.Find(p => p.Id == id);
        if (produtoEncontrado == null)
        {
            return NotFound();
        }

        produtos.Remove(produtoEncontrado);
        return NoContent();
    }
}