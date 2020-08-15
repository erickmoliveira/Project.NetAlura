using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository _ICategoriaRepository;
        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository ICategoriaRepository) : base(contexto)
        {
            _ICategoriaRepository = ICategoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return dbSet.Include(p => p.Categoria).ToList();
        }
        public async Task<IList<Produto>> GetProdutos(string nome)
        {
           
            var Produtos =  dbSet.Where(p => p.Nome.Contains(nome)).Include(c => c.Categoria).ToList();

            if (Produtos == null)
            {
                //tenta realizar a pesquisa pelo nome da categoria
                 Produtos = dbSet.Where(p => p.Categoria.NomeCategoria.Contains(nome)).Include(c => c.Categoria).ToList();
            }

            return Produtos;
        }

        public async Task SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {

                    var Categoria = contexto.Set<Categoria>()
                                .Where(i => i.NomeCategoria == livro.Categoria)
                                .SingleOrDefault();

                    if (Categoria==null)
                    {
                        Categoria = await _ICategoriaRepository.SaveCategoria(livro.Categoria);
                       
                    }

                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco, Categoria));
                }
            }
            await contexto.SaveChangesAsync();
        }
    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}
