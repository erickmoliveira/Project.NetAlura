using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria> SaveCategoria(string nome);
    }
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task<Categoria> SaveCategoria(string nome)
        {
            Categoria categoria = new Categoria();

            var ExistCategoria = dbSet.FirstOrDefault(cat => cat.NomeCategoria==nome);
            if (ExistCategoria == null)
            {
               
                categoria.NomeCategoria = nome;

                dbSet.Add(categoria);

                await contexto.SaveChangesAsync();

                return categoria;
            }
            else
            {
                return null;
            }

             
           
        }
    }
}
