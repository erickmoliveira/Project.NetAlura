using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutosViewModel
    {
        public BuscaDeProdutosViewModel(IList<Produto> produtos)
        {
            Produtos = produtos;
        }

        public IList<Produto> Produtos { get; }

        public string TextoPesquisa { get; set; }
      
    }
}
