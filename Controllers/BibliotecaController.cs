using Api.Models;
using Api.Requests;
using BibliotecaDeLivros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibliotecaDeLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BibliotecaController : ControllerBase
    {
        private static List<BibliotecaModels> Biblioteca = new List<BibliotecaModels>
        {
            new BibliotecaModels
            {
                Id = 1,
                Titulo = "Dom Casmurro",
                Autor = "Machado de Assis",
                Ano = 1899,
                Quantidade = 2

            },
            new BibliotecaModels
            {
                Id = 2,
                Titulo = "Grande Sertão: Veredas",
                Autor = "João Guimarães Rosa",
                Ano = 1956,
                Quantidade = 3,
            },
            new BibliotecaModels
            {
                Id = 3,
                Titulo = "Grande Sertão: Veredas",
                Autor = "João Guimarães Rosa",
                Ano = 1956,
                Quantidade = 4
            },
            new BibliotecaModels
            {
                Id = 4,
                Titulo = "O Cortiço",
                Autor = "Aluísio Azevedo",
                Ano = 1890,
                Quantidade = 4
            },
            new BibliotecaModels
            {
                Id = 5,
                Titulo = "Iracema",
                Autor = "José de Alencar",
                Ano = 1865,
                Quantidade = 1
            },
            new BibliotecaModels
            {
                Id = 6,
                Titulo = "Macunaíma",
                Autor = "Mário de Andrade",
                Ano = 1928,
                Quantidade = 11
            },
            new BibliotecaModels
            {
                Id = 7,
                Titulo = "Capitães da Areia",
                Autor = "Jorge Amado",
                Ano = 1937,
                Quantidade = 2
            },
            new BibliotecaModels
            {
                Id = 8,
                Titulo = "Vidas Secas",
                Autor = "Graciliano Ramos",
                Ano = 1938,
                Quantidade = 9
            },
            new BibliotecaModels
            {
                Id = 9,
                Titulo = "A Moreninha",
                Autor = "Joaquim Manuel de Macedo",
                Ano = 1844,
                Quantidade = 2
            },
            new BibliotecaModels
            {
                Id = 10,
                Titulo = "O Tempo e o Vento",
                Autor = "Erico Verissimo 1949",
                Ano = 1949,
                Quantidade = 1
            },
            new BibliotecaModels
            {
                Id = 11,
                Titulo = "O Quinze",
                Autor = "Rachel de Queiroz",
                Ano = 1930,
                Quantidade = 1
            },
            new BibliotecaModels
            {
                Id = 12,
                Titulo = "Menino do Engenho",
                Autor = "José Lins do Rego",
                Ano = 1932,
                Quantidade = 5
            },
            new BibliotecaModels
            {
                Id = 13,
                Titulo = "Sagarana",
                Autor = "João Guimarães Rosa",
                Ano = 1946,
                Quantidade = 3
            },
            new BibliotecaModels
            {
                Id = 14,
                Titulo = "Fogo Morto",
                Autor = "José Lins do Rego",
                Ano = 1943,
                Quantidade = 1
            },
            new BibliotecaModels
            {
                Id = 15,
                Titulo = "Memórias Póstumas de Brás Cubas",
                Autor = "Machado de Assis",
                Ano = 1881,
                Quantidade = 3
            }
        };

        private static List<AluguelModel> alugueis = new List<AluguelModel> { };

        [HttpGet]
        public ActionResult<List<BibliotecaModels>> VerTodosLivros()
        {
            return Ok(Biblioteca);
        }

        [HttpPut("alugar/{id}")]
        public ActionResult<List<BibliotecaModels>> AlugarLivro(int id, [FromBody] AluguelRequests aluguel)
        {
            var livro = Biblioteca.Find(x => x.Id == id);

            if (livro == null)
            {
                return NotFound(new { message= $"Livro com ID {id} não encontrado!" });
            }

            if (livro.Quantidade <= 0)
            {
                return BadRequest(new { message = "Não há exemplares disponíveis para aluguel." });
            }

            if (aluguel.name == null)
            {
                return BadRequest(new { message = "Nome e/ou data de nascimento não preenchido." });
            }


            var data = new AluguelModel()
            {
                Id = alugueis.Count() + 1,
                anoNascimento = aluguel.anoNascimento,
                name = aluguel.name,
                livroId = id,
                emprestado_em = DateTime.Now,
                devolvido_em = null
            };

            alugueis.Add(data);

            livro.Quantidade--;

            return Ok(new { message = $"Livro {livro.Titulo} alugado com sucesso! Quantidade atual: {livro.Quantidade}" });
        }

        [HttpGet("devolucao/{id}")]
        public ActionResult<List<BibliotecaModels>> DevolverLivro(int id)
        {
            var devLivro = Biblioteca.Find(x => x.Id == id);

            if (devLivro == null)
            {
                return NotFound(new { message = $"Livro com ID {id} não encontrado!" });
            }

            devLivro.Quantidade++;

            return Ok(new { message = $"Livro {devLivro.Titulo} devolvido com sucesso! Quantidade atual: {devLivro.Quantidade}" });
        }


        [HttpGet("titulo/{titulo}")]
        public ActionResult<List<BibliotecaModels>> NomeLivro(string titulo)
        {
            var nomeLivro = Biblioteca.Find(x => x.Titulo == titulo);

            if (nomeLivro is null) return NotFound(new { message = "Livro não encontrado!" });  

            return Ok(nomeLivro);
        }

        [HttpGet("aluguel")]
        public ActionResult<List<BibliotecaModels>> VerTodosOsAlugueis()
        {
            return Ok(alugueis);
        }


    }
}