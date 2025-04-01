using GsStore.Models;
namespace GsStore.ViewModels;

public class ProdutoVM
{
    public Produto Produto { get; set; }
    public List<Produto> Semelhante { get; set; }
}
